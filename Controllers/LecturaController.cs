using AcquaWS.DTO;
using AcquaWS.Models;
using AcquaWS.Response;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AcquaWS.Controllers
{
    [ApiController]
    [Route("api")]
    public class LecturaController : ControllerBase
    {

        private readonly ApplicationDbContext _context = null;

        public LecturaController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        /*
        //Endpoint para calculo de consumo (SOLO PARA PRUEBAS)
        [HttpPost("consumo")]
        public async Task<IActionResult> CalcularConsumo([FromBody] LecturaRequest request)
        {
            try
            {
                var result = await _context
                    .ConsumoResponse
                    .FromSqlRaw("EXEC sp_CalcularConsumo @Lectura",
                        new SqlParameter("@Lectura", request.Lectura))
                    .ToListAsync();

                var data = result.FirstOrDefault();

                var response = new ConsumoResponse
                {  
                    Consumo = data.Consumo, 
                    LecturaAnterior = data.LecturaAnterior,
                    LecturaActual = data.LecturaActual
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        */

        //EndPoint para obtener información del periodo activo
        [HttpGet("periodo/{id}")]
        public async Task<ActionResult<IReadOnlyList<PeriodoDTO>>> obtenerPeriodo(int id)
        {
            var periodo = await _context.Servicios_recurrentes_lectura
                .AsNoTracking()
                .Where(p => p.Id == id && p.borrado == false)
                .Select(p => new PeriodoDTO
                {
                    Id = p.Id,
                    IdCatalogo = p.IdCatalogo,
                    Fecha = p.Fecha.ToString("dd-MM-yyyy"),
                    Periodo_inicio = p.Periodo_inicio.ToString("dd-MM-yyyy"),
                    Periodo_fin = p.Periodo_fin.ToString("dd-MM-yyyy"),
                    Fecha_vencimiento = p.Fecha_vencimiento.ToString("dd-MM-yyyy"),
                    Concepto = p.Concepto,
                    Estado = p.Estado.Trim()
                })
                .FirstOrDefaultAsync();
            if (periodo == null)
            {
                return NotFound();
            }
            return Ok(periodo);
        }


        //EndPoint para obtener los clientes a prefacturar en el periodo
        [HttpGet("lectura_prefactura/{idLectura}")]
        public async Task<ActionResult<IReadOnlyList<SRLecturaPrefacturaDTO>>> obtenerClientesPeriodo(int idLectura)
        {
            var lectura_prefactura = await _context.Servicios_recurrentes_lectura_prefactura
                .AsNoTracking()
                .Where(l => l.IdLectura == idLectura && l.Borrado == false)
                .Select(
                l => new SRLecturaPrefacturaDTO
                {
                    Id = l.Id,
                    IdLectura = l.IdLectura,
                    IdServicio_recuerrente_cliente = l.IdServicio_recuerrente_cliente,
                    Lectura_anterior = l.Lectura_anterior,
                    Lectura_actual = l.Lectura_actual,
                    Consumo = l.Consumo,
                    IdCatalogo = l.IdCatalogo,
                    IdTarifa = l.IdTarifa,
                    AppPrefacturado = l.AppPrefacturado,
                    AppFechaHoraPrefacturado = l.AppFechaHoraPrefacturado,
                    AppMovilPrefacturado = l.AppMovilPrefacturado == null ? "" : l.AppMovilPrefacturado,
                    AppUsuarioPrefacturado = l.AppUsuarioPrefacturado == null ? "" : l.AppUsuarioPrefacturado
                })
                .ToListAsync();
            if (lectura_prefactura == null)
            {
                return NotFound();
            }
            return Ok(lectura_prefactura);
        }


        //EndPoint para procesar lectura
        [HttpPost("procesarlectura")]
        public async Task<IActionResult> ProcesarLectura(LecturasDTO request)
        {
            return await procesarLecturaInterna(request, null);
        }


        //EndPoint para procesar lectura anterior
        [HttpPost("procesar_lectura_anterior")]
        public async Task<IActionResult> procesarLecturaAnterior(LecturaAnteriorDTO request)
        {
            try
            {
                var clienteCuenta = await _context.Servicios_recurrentes_clientes
                    .FirstOrDefaultAsync(c =>
                    c.Cuenta == request.Cuenta &&
                    c.Borrado == false
                    );
                if (clienteCuenta == null) { return NotFound("La cuenta no existe."); }

                var prefactura = await _context.Servicios_recurrentes_lectura_prefactura
                    .FirstOrDefaultAsync(p =>
                    p.IdLectura == request.IdLectura &&
                    p.IdServicio_recuerrente_cliente == clienteCuenta.id
                    );
                if (prefactura == null) { return NotFound(""); }

                clienteCuenta.Ultima_lectura = request.Lectura_anterior;
                prefactura.Lectura_anterior = request.Lectura_anterior;

                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "LECTURA ANTERIOR PROCESADA" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = ex.Message,
                    inner = ex.InnerException?.Message,
                    tipo = ex.GetType().FullName
                });
            }
        }


        //EndPoint para procesar lectura completa (actual y anterior)
        [HttpPost("procesarlectura_con_anterior")]
        public async Task<IActionResult> ProcesarLecturaConAnterior(LecturaConAnteriorDTO request)
        {
            if (request.IdLectura <= 0)
            {
                return BadRequest("El periodo es invalido.");
            }
            if(request.Lectura_anterior <= 0)
            {
                return BadRequest("La lectura anterior debe ser mayor que cero.");
            }
            if (request.Lectura_actual < request.Lectura_anterior)
            {
                return BadRequest("La lectura actual no puede ser menor que la lectura anterior.");
            }

            var lecturaRequest = new LecturasDTO
            {
                IdLectura = request.IdLectura,
                Cuenta = request.Cuenta,
                Lectura_actual = request.Lectura_actual,
                Empleado = request.Empleado
            };

            return await procesarLecturaInterna(lecturaRequest, request.Lectura_anterior);
        }


        //Metdodo que recibe los datos de la lectura, verifica datos y arma la respuesta
        private async Task<IActionResult> procesarLecturaInterna(LecturasDTO request, decimal? lecturaAnterior)
        {
            try
            {
                // Buscar cliente por cuenta
                var cliente = await _context.Servicios_recurrentes_clientes
                    .FirstOrDefaultAsync(c =>
                        c.Cuenta == request.Cuenta &&
                        c.Borrado == false);
                if (cliente == null)
                {
                    return NotFound("La cuenta no existe.");
                }
                if (lecturaAnterior.HasValue) { 
                    if (cliente.Ultima_lectura != 0)
                    {
                        return BadRequest("La cuenta ya tiene una lectura anterior");
                    }
                    cliente.Ultima_lectura = lecturaAnterior.Value;
                }
                else
                {
                    if (cliente.Ultima_lectura == 0)
                    {
                        return BadRequest("ULTIMA_LECTURA_REQUERIDA");
                    }
                }

                // Buscar registro del cliente en tabla Clientes
                var cliente_detalle = await _context.Clientes
                    .FirstOrDefaultAsync(
                        cd =>
                        cd.Id == cliente.IdCliente
                    );

                if (cliente_detalle == null)
                {
                    return NotFound("El cliente no existe");
                }

                //Buscar la tarifa del cliente
                var servicios_cliente = await _context.Servicios_recurrentes_clientes_servicios
                    .FirstOrDefaultAsync(
                    t =>
                    t.Id_Servicios_recurrentes_clientes == cliente.id
                    && t.Borrado == false
                    && t.IdTarifa != null
                    );
                if (servicios_cliente == null)
                {
                    return NotFound("No se encontró la tarifa del cliente");
                }

                //Buscar datos del pliego tarifario de la tarifa del cliente
                var tarifa_cliente = await _context.Servicios_recurrentes_tarifas
                    .FirstOrDefaultAsync(
                    t =>
                    t.Id == servicios_cliente.IdTarifa
                    );
                if (tarifa_cliente == null)
                {
                    return NotFound("No se encontró el pliego tarifario");
                }

                // Buscar colonia/barrio del cliente
                var colbar = await _context.Servicios_recurrentes_clientes_colbarr
                    .FirstOrDefaultAsync(
                        cb =>
                        cb.Id == cliente.IdColbarr &&
                        cb.Borrado == false
                    );

                // Buscar sector del cliente
                var sector_cliente = await _context.Servicios_recurrentes_clientes_sectores
                    .FirstOrDefaultAsync(
                        cs =>
                        cs.id == cliente.IdSector &&
                        cs.Borrado == false
                    );
                // Buscar zona del cliente
                var zona_cliente = await _context.Servicios_recurrentes_clientes_sectores_zonas
                    .FirstOrDefaultAsync(
                        zc =>
                        zc.id == cliente.IdSector_zona &&
                        zc.Borrado == false
                    );

                //Validar que IdLectura sea válido
                if (request.IdLectura <= 0)
                {
                    return BadRequest("El período es inválido.");
                }

                // Buscar el registro de prefactura
                var prefactura = await _context.Servicios_recurrentes_lectura_prefactura
                    .FirstOrDefaultAsync(p =>
                        p.IdLectura == request.IdLectura &&
                        p.IdServicio_recuerrente_cliente == cliente.id &&
                        p.Borrado == false);

                if (prefactura == null)
                {
                    return NotFound("No existe una prefactura para ese cliente y período.");
                }
                if (prefactura.AppPrefacturado)
                {
                    return BadRequest("La lectura ya fue procesada.");
                }
                if (lecturaAnterior.HasValue)
                {
                    prefactura.Lectura_anterior = lecturaAnterior.Value;
                }

                // Validar lectura
                if (request.Lectura_actual < prefactura.Lectura_anterior)
                {
                    return BadRequest("La lectura actual no puede ser menor que la lectura anterior.");
                }

                // Actualizar
                prefactura.Lectura_actual = request.Lectura_actual;
                prefactura.Consumo = Math.Round(
                    request.Lectura_actual - prefactura.Lectura_anterior, 0, MidpointRounding.AwayFromZero
                    );
                prefactura.AppPrefacturado = true;
                prefactura.AppFechaHoraPrefacturado = DateTime.Now;
                prefactura.AppUsuarioPrefacturado = request.Empleado;

                await _context.SaveChangesAsync();
                return Ok(new
                {
                    Cuenta = cliente.Cuenta,
                    Nombre = cliente_detalle.Cliente.Trim(),
                    Periodo = request.IdLectura,
                    LecturaAnterior = prefactura.Lectura_anterior,
                    LecturaActual = prefactura.Lectura_actual,
                    Consumo = prefactura.Consumo,
                    AppPrefacturado = prefactura.AppPrefacturado,
                    Usuario = prefactura.AppUsuarioPrefacturado,
                    Documento = cliente_detalle?.Dui == null ? "" : cliente_detalle.Dui.Trim(),
                    Direccion = cliente_detalle?.DTEDireccion == null ? "" : cliente_detalle.DTEDireccion.Trim(),
                    IdColbar = colbar?.Id == null ? 0 : colbar.Id,
                    Colbar = colbar?.Nombre == null ? "" : colbar.Nombre.Trim(),
                    IdSector = sector_cliente?.id == null ? 0 : sector_cliente.id,
                    Sector = sector_cliente?.Sector == null ? "" : sector_cliente?.Sector.Trim(),
                    IdZona = zona_cliente?.id == null ? 0 : zona_cliente.id,
                    Zona = zona_cliente?.Zona == null ? "" : zona_cliente.Zona.Trim(),
                    //---- CARGOS FIJOS DE LA TARIFA DEL CLIENTE ----//
                    Cf1_linea = tarifa_cliente?.CargoFijo1_Linea == null ? "" : tarifa_cliente.CargoFijo1_Linea.Trim(),
                    Cf1_descripcion = tarifa_cliente?.CargoFijo1_Descripcion == null ? "" : tarifa_cliente.CargoFijo1_Descripcion.Trim(),
                    Cf1_precio = tarifa_cliente?.CargoFijo1_precio == null ? 0 : tarifa_cliente.CargoFijo1_precio,

                    Cf2_linea = tarifa_cliente?.CargoFijo2_Linea == null ? "" : tarifa_cliente.CargoFijo2_Linea.Trim(),
                    Cf2_descripcion = tarifa_cliente?.CargoFijo2_Descripcion == null ? "" : tarifa_cliente.CargoFijo2_Descripcion.Trim(),
                    Cf2_precio = tarifa_cliente?.CargoFijo2_precio == null ? 0 : tarifa_cliente.CargoFijo2_precio,

                    Cf3_linea = tarifa_cliente?.CargoFijo3_Linea == null ? "" : tarifa_cliente.CargoFijo3_Linea.Trim(),
                    Cf3_descripcion = tarifa_cliente?.CargoFijo3_Descripcion == null ? "" : tarifa_cliente.CargoFijo3_Descripcion.Trim(),
                    Cf3_precio = tarifa_cliente?.CargoFijo3_precio == null ? 0 : tarifa_cliente.CargoFijo3_precio,

                    Cf4_linea = tarifa_cliente?.CargoFijo4_Linea == null ? "" : tarifa_cliente.CargoFijo4_Linea.Trim(),
                    Cf4_descripcion = tarifa_cliente?.CargoFijo4_Descripcion == null ? "" : tarifa_cliente.CargoFijo4_Descripcion.Trim(),
                    Cf4_precio = tarifa_cliente?.CargoFijo4_precio == null ? 0 : tarifa_cliente.CargoFijo4_precio,

                    Cf5_linea = tarifa_cliente?.CargoFijo5_Linea == null ? "" : tarifa_cliente.CargoFijo5_Linea.Trim(),
                    Cf5_descripcion = tarifa_cliente?.CargoFijo5_Descripcion == null ? "" : tarifa_cliente.CargoFijo5_Descripcion.Trim(),
                    Cf5_precio = tarifa_cliente?.CargoFijo5_precio == null ? 0 : tarifa_cliente.CargoFijo5_precio,

                    //---- PLIEGO TARIFARIO DE LA TARIFA DEL CLIENTE ----//

                    Minimo_m3 = 0,
                    Primeros_m3 = tarifa_cliente.Primeros_m3 == null ? 0 : tarifa_cliente.Primeros_m3,
                    Primeros_m3_valor = tarifa_cliente.Primeros_m3_valor == null ? 0 : tarifa_cliente.Primeros_m3_valor,

                    E1_minimo_m3 = tarifa_cliente.Escala1_minimo_m3 == null ? 0 : tarifa_cliente.Escala1_minimo_m3,
                    E1_maximo_m3 = tarifa_cliente.Escala1_maximo_m3 == null ? 0 : tarifa_cliente.Escala1_maximo_m3,
                    E1_valor_m3 = tarifa_cliente.Escala1_valor_m3 == null ? 0 : tarifa_cliente.Escala1_valor_m3,

                    E2_minimo_m3 = tarifa_cliente.Escala2_minimo_m3 == null ? 0 : tarifa_cliente.Escala2_minimo_m3,
                    E2_maximo_m3 = tarifa_cliente.Escala2_maximo_m3 == null ? 0 : tarifa_cliente.Escala2_maximo_m3,
                    E2_valor_m3 = tarifa_cliente.Escala2_valor_m3 == null ? 0 : tarifa_cliente.Escala2_valor_m3,

                    E3_minimo_m3 = tarifa_cliente.Escala3_minimo_m3 == null ? 0 : tarifa_cliente.Escala3_minimo_m3,
                    E3_maximo_m3 = tarifa_cliente.Escala3_maximo_m3 == null ? 0 : tarifa_cliente.Escala3_maximo_m3,
                    E3_valor_m3 = tarifa_cliente.Escala3_valor_m3 == null ? 0 : tarifa_cliente.Escala3_valor_m3,

                    E4_minimo_m3 = tarifa_cliente.Escala4_minimo_m3 == null ? 0 : tarifa_cliente.Escala4_minimo_m3,
                    E4_maximo_m3 = tarifa_cliente.Escala4_maximo_m3 == null ? 0 : tarifa_cliente.Escala4_maximo_m3,
                    E4_valor_m3 = tarifa_cliente.Escala4_valor_m3 == null ? 0 : tarifa_cliente.Escala4_valor_m3,

                    E5_minimo_m3 = tarifa_cliente.Escala5_minimo_m3 == null ? 0 : tarifa_cliente.Escala5_minimo_m3,
                    E5_maximo_m3 = tarifa_cliente.Escala5_maximo_m3 == null ? 0 : tarifa_cliente.Escala5_maximo_m3,
                    E5_valor_m3 = tarifa_cliente.Escala5_valor_m3 == null ? 0 : tarifa_cliente.Escala5_valor_m3,

                    E6_minimo_m3 = tarifa_cliente.Escala6_minimo_m3 == null ? 0 : tarifa_cliente.Escala6_minimo_m3,
                    E6_maximo_m3 = tarifa_cliente.Escala6_maximo_m3 == null ? 0 : tarifa_cliente.Escala6_maximo_m3,
                    E6_valor_m3 = tarifa_cliente.Escala6_valor_m3 == null ? 0 : tarifa_cliente.Escala6_valor_m3,

                    E7_minimo_m3 = tarifa_cliente.Escala7_minimo_m3 == null ? 0 : tarifa_cliente.Escala7_minimo_m3,
                    E7_maximo_m3 = tarifa_cliente.Escala7_maximo_m3 == null ? 0 : tarifa_cliente.Escala7_maximo_m3,
                    E7_valor_m3 = tarifa_cliente.Escala7_valor_m3 == null ? 0 : tarifa_cliente.Escala7_valor_m3,

                    E8_minimo_m3 = tarifa_cliente.Escala8_minimo_m3 == null ? 0 : tarifa_cliente.Escala8_minimo_m3,
                    E8_maximo_m3 = tarifa_cliente.Escala8_maximo_m3 == null ? 0 : tarifa_cliente.Escala8_maximo_m3,
                    E8_valor_m3 = tarifa_cliente.Escala8_valor_m3 == null ? 0 : tarifa_cliente.Escala8_valor_m3,

                    E9_minimo_m3 = tarifa_cliente.Escala9_minimo_m3 == null ? 0 : tarifa_cliente.Escala9_minimo_m3,
                    E9_maximo_m3 = tarifa_cliente.Escala9_maximo_m3 == null ? 0 : tarifa_cliente.Escala9_maximo_m3,
                    E9_valor_m3 = tarifa_cliente.Escala9_valor_m3 == null ? 0 : tarifa_cliente.Escala9_valor_m3,

                    E10_minimo_m3 = tarifa_cliente.Escala10_minimo_m3 == null ? 0 : tarifa_cliente.Escala10_minimo_m3,
                    E10_maximo_m3 = tarifa_cliente.Escala10_maximo_m3 == null ? 0 : tarifa_cliente.Escala10_maximo_m3,
                    E10_valor_m3 = tarifa_cliente.Escala10_valor_m3 == null ? 0 : tarifa_cliente.Escala10_valor_m3,

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = ex.Message,
                    inner = ex.InnerException?.Message,
                    tipo = ex.GetType().FullName
                });
            }

        }
    
    }
}


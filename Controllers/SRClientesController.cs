using AcquaWS.Models;
using AcquaWS.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using AcquaWS.Response;

namespace AcquaWS.Controllers
{
    [Route("api")]
    public class SRClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SRClientesController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        //EndPoint para obtener sectores
        [HttpGet("sectores")]
        public async Task<ActionResult<IReadOnlyList<SRClientesSectoresDTO>>> ObenerSectores()
        {
            var sector = await _context.Servicios_recurrentes_clientes_sectores
                .AsNoTracking()
                .OrderBy(s => s.Sector)
                .Select(s => new SRClientesSectoresDTO
                {
                    Sector = s.Sector == null ? "" : s.Sector.Trim()
                })
            .ToListAsync();
            return Ok(sector);
        }

        [HttpGet("clientessr")]
        public async Task<ActionResult<IReadOnlyList<SRClientesDTO>>> ObtenerSRClientes()
        {

            var srCliente = await _context.Servicios_recurrentes_clientes
                .AsNoTracking()
                .Where(c => c.Borrado != true)
                .OrderBy(c => c.Cuenta)
                .Select(
                    c => new SRClientesDTO
                    {
                        id = c.id,
                        IdCliente = c.IdCliente,
                        Ultimo_mes_prefacturado = c.Ultimo_mes_prefacturado.ToString("dd-MM-yyyy"),
                        Ultimo_mes_cancelado = c.Ultimo_mes_cancelado.ToString("dd-MM-yyyy"),
                        Ultima_factura_cancelada = c.Ultima_factura_cancelada == null ? "" : c.Ultima_factura_cancelada,
                        Status = c.Status,
                        Cuenta = c.Cuenta,

                        IdCatalogo = _context.Servicios_recurrentes_clientes_servicios
                        .Where(s => s.Id_Servicios_recurrentes_clientes == c.id
                        && s.Borrado == false && s.IdTarifa != null)
                        .Select(s => s.IdCatalogo)
                        .FirstOrDefault(),

                        Balance = c.Balance,
                        IdSector = c.IdSector,
                        Tipo_factura = c.Tipo_factura == null ? "" : c.Tipo_factura,
                        Ult_lectura = c.Ultima_lectura,
                        Ult_factura_prefacturada = c.Ultima_factura_prefacturada == null ? "" : c.Ultima_factura_prefacturada ,

                        IdTarifa = _context.Servicios_recurrentes_clientes_servicios
                        .Where(s => s.Id_Servicios_recurrentes_clientes == c.id
                        && s.Borrado == false && s.IdTarifa != null)
                        .Select(s => s.IdTarifa)
                        .FirstOrDefault(),

                        CodCliente = c.CodCliente == null ? "" : c.CodCliente.Trim()
                    })
                .ToListAsync();
                return Ok(srCliente);
        }


        [HttpGet("tarifas")]
        public async Task<ActionResult<IReadOnlyList<SRTarifasDTO>>> ObtenerSRTarifas()
        {
            var srTarifas = await _context.Servicios_recurrentes_tarifas
                .AsNoTracking()
                .Select(
                    t => new SRTarifasDTO
                    {
                        Id = t.Id,
                        idCatalogo = t.idCatalogo,
                        Tarifa = t.Tarifa.Trim(),

                        //ESCALAS
                        Primeros_m3 = t.Primeros_m3,
                        Primeros_m3_valor = t.Primeros_m3_valor,
                        Primeros_m3_descuento = t.Primeros_m3_descuento,
                        Primeros_m3_alcantarillado = t.Primeros_m3_alcantarillado,

                        E1_minimo_m3 = t.Escala1_minimo_m3,
                        E1_maximo_m3 = t.Escala1_maximo_m3,
                        E1_valor_m3 = t.Escala1_valor_m3,
                        Escala1_alcantarillado = t.Escala1_alcantarillado,

                        E2_minimo_m3 = t.Escala2_minimo_m3,
                        E2_maximo_m3 = t.Escala2_maximo_m3,
                        E2_valor_m3 = t.Escala2_valor_m3,
                        Escala2_alcantarillado = t.Escala2_alcantarillado,

                        E3_minimo_m3 = t.Escala3_minimo_m3,
                        E3_maximo_m3 = t.Escala3_maximo_m3,
                        E3_valor_m3 = t.Escala3_valor_m3,
                        Escala3_alcantarillado = t.Escala3_alcantarillado,

                        E4_minimo_m3 = t.Escala4_minimo_m3,
                        E4_maximo_m3 = t.Escala4_maximo_m3,
                        E4_valor_m3 = t.Escala4_valor_m3,
                        Escala4_alcantarillado = t.Escala4_alcantarillado,

                        E5_minimo_m3 = t.Escala5_minimo_m3,
                        E5_maximo_m3 = t.Escala5_maximo_m3,
                        E5_valor_m3 = t.Escala5_valor_m3,
                        Escala5_alcantarillado = t.Escala5_alcantarillado,

                        E6_minimo_m3 = t.Escala6_minimo_m3,
                        E6_maximo_m3 = t.Escala6_maximo_m3,
                        E6_valor_m3 = t.Escala6_valor_m3,
                        Escala6_alcantarillado = t.Escala6_alcantarillado,

                        E7_minimo_m3 = t.Escala7_minimo_m3,
                        E7_maximo_m3 = t.Escala7_maximo_m3,
                        E7_valor_m3 = t.Escala7_valor_m3,
                        Escala7_alcantarillado = t.Escala7_alcantarillado,

                        E8_minimo_m3 = t.Escala8_minimo_m3,
                        E8_maximo_m3 = t.Escala8_maximo_m3,
                        E8_valor_m3 = t.Escala8_valor_m3,
                        Escala8_alcantarillado = t.Escala8_alcantarillado,

                        E9_minimo_m3 = t.Escala9_minimo_m3,
                        E9_maximo_m3 = t.Escala9_maximo_m3,
                        E9_valor_m3 = t.Escala9_valor_m3,
                        Escala9_alcantarillado = t.Escala9_alcantarillado,

                        E10_minimo_m3 = t.Escala10_minimo_m3,
                        E10_maximo_m3 = t.Escala10_maximo_m3,
                        E10_valor_m3 = t.Escala10_valor_m3,
                        Escala10_alcantarillado = t.Escala10_alcantarillado,

                        //CANON
                        Canon = t.Canon,
                        Canon_TipoFiscal = t.Canon_TipoFiscal.Trim(),


                        //CARGOS FIJOS
                        Cf1_Codigo = t.CargoFijo1_Codigo == null ?"" : t.CargoFijo1_Codigo,
                        Cf1_Descripcion = t.CargoFijo1_Descripcion == null ? "" : t.CargoFijo1_Descripcion,
                        Cf1_Linea = t.CargoFijo1_Linea == null ? "" : t.CargoFijo1_Linea,
                        Cf1_precio = t.CargoFijo1_precio,
                        Cf1_TipoFiscal = t.CargoFijo1_TipoFiscal.Trim(),

                        Cf2_Codigo = t.CargoFijo2_Codigo == null ?"": t.CargoFijo2_Codigo,
                        Cf2_Descripcion = t.CargoFijo2_Descripcion == null ?"" : t.CargoFijo2_Descripcion,
                        Cf2_Linea = t.CargoFijo2_Linea == null ? "" : t.CargoFijo2_Linea,
                        Cf2_precio = t.CargoFijo2_precio,
                        Cf2_TipoFiscal = t.CargoFijo2_TipoFiscal.Trim(),

                        Cf3_Codigo = t.CargoFijo3_Codigo == null ? "" : t.CargoFijo3_Codigo,
                        Cf3_Descripcion = t.CargoFijo3_Descripcion == null ? "" : t.CargoFijo3_Descripcion,
                        Cf3_Linea = t.CargoFijo3_Linea == null ? "" : t.CargoFijo3_Linea,
                        Cf3_precio = t.CargoFijo3_precio,
                        Cf3_TipoFiscal = t.CargoFijo3_TipoFiscal.Trim(),

                        Cf4_Codigo = t.CargoFijo4_Codigo == null ? "" : t.CargoFijo4_Codigo,
                        Cf4_Descripcion = t.CargoFijo4_Descripcion == null ? "" : t.CargoFijo4_Descripcion,
                        Cf4_Linea = t.CargoFijo4_Linea == null ? "" : t.CargoFijo4_Linea,
                        Cf4_precio = t.CargoFijo4_precio,
                        Cf4_TipoFiscal = t.CargoFijo4_TipoFiscal.Trim(),

                        Cf5_Codigo = t.CargoFijo5_Codigo == null ? "" : t.CargoFijo5_Codigo,
                        Cf5_Descripcion = t.CargoFijo5_Descripcion == null ? "" : t.CargoFijo5_Descripcion,
                        Cf5_Linea = t.CargoFijo5_Linea == null ? "" : t.CargoFijo5_Linea,
                        Cf5_precio = t.CargoFijo5_precio,
                        Cf5_TipoFiscal = t.CargoFijo5_TipoFiscal.Trim(),

                        PliegoTarifario_TipoFiscal = t.PliegoTarifario_TipoFiscal.Trim(),
                        

                    })
                .ToListAsync();
            return(Ok(srTarifas));
        }


    }
}
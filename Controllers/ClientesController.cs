using AcquaWS.Models;
using AcquaWS.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AcquaWS.Controllers
{
    [Route("api")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClientesController(ApplicationDbContext ctx) {
            _context = ctx;
        }

        //EndPoint para obtener listado de clientes
        [HttpGet("clientes")]
        public async Task<ActionResult<IReadOnlyList<ClientesDTO>>> ObtenerClientes()
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .Where(c => c.Id != 1)
                .Select(c => new ClientesDTO
                {
                    Id = c.Id,
                    Codigo = c.Codigo.Trim(),
                    Cliente = c.Cliente.Trim(),
                    Casa = c.Contacto == null ? "" : c.Contacto.Trim(),
                    Direccion = c.DTEDireccion == null ? "" : c.DTEDireccion.Trim(),
                    Poligono = c.Ruta == null ? "" : c.Ruta.Trim(),
                    Id_ruta = c.Id_ruta,
                    Codigo_casa = c.ref_personal_nombre == null ? "" : c.ref_personal_nombre.Trim()
                })
                .ToListAsync();
            return Ok(cliente);
        }
    }
}
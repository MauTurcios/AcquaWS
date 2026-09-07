using AcquaWS.Models;
using AcquaWS.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AcquaWS.Controllers
{
    [Route("api")]
    public class EmpresaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmpresaController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        //Endpoint para obtener configuración
        [HttpGet("config")]
        public async Task<ActionResult<IReadOnlyList<ConfigDTO>>> obtenerConfig()
        {
            var config = await _context.Config
                .AsNoTracking()
                .Select(c => new ConfigDTO
                {
                    DTEPais = c.DTEPais == null ? "" : c.DTEPais.Trim(),
                    DTEDepto = c.DTEDepto == null ? "" : c.DTEPais.Trim(),
                    DTEMunicipio = c.DTEMunicipio == null ? "" : c.DTEMunicipio.Trim(),
                    DTEDistrito = c.DTEDistrito == null ? "" : c.DTEDistrito.Trim(),
                    DTENit = c.DTENit == null ? "" : c.DTENit.Trim(),  
                    DTENrc = c.DTENrc == null ? "": c.DTENrc.Trim(),
                    DTENombreEmisor = c.DTENombreEmisor == null ? "" : c.DTENombreEmisor.Trim(),
                    DTENombreComercial = c.DTENombreComercial == null ? "" : c.DTENombreComercial.Trim(),
                    DTETelefono = c.DTETelefonoEmisor == null ? "" : c.DTETelefonoEmisor.Trim(),
                    DTECorreo = c.DTECorreoEmisor == null ? "" : c.DTECorreoEmisor.Trim(),
                    DTEDireccion = c.DTEDireccionEmisor == null ? "" : c.DTEDireccionEmisor.Trim(),
                    DTEGiro = c.DTEGiro == null ? "" : c.DTEGiro.Trim()

                }).ToListAsync();

            return Ok(config);
        }

        //Endpoint para obtener datos de actualizacion de la app
        [HttpGet("updateapp")]
        public async Task<ActionResult<IReadOnlyList<UpdateAppDTO>>> updateApp()
        {
            var update = await _context.updateVersionApp
                .AsNoTracking()
                .Select(u => new UpdateApp
                {
                    id = u.id,
                    version = u.version,
                    url = u.url,

                }).ToListAsync();

            return Ok(update);
        }
    }
}

using AcquaWS.Models;
using AcquaWS.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AcquaWS.Controllers
{
    [Route("api")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmpleadosController(ApplicationDbContext ctx){
            _context = ctx;
        }
        //EndPoint para obtener listado de empleados
        [HttpGet("empleados")] 
        public async Task<ActionResult<IEnumerable<EmpleadosDTO>>> ObtenerEmpleados()
        {
            var emp = await _context.Empleados
                .Select(e => new EmpleadosDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo.Trim(),
                    Empleado = e.Empleado.Trim(),
                    Status = e.Status.Trim(),
                    Usuario_App = e.Usuario_App.Trim(),
                    Clave_App = e.Clave_App.Trim(),
                    Identidad_App = e.Identidad_App.Trim(),
                    Estado_App = e.Estado_App.Trim(),
                    Ultima_Conexion_App = e.Ultima_Conexion_App,
                    Todos_clientes_App = e.Todos_clientes_App.Trim()
                })
                .ToListAsync();
            return Ok(emp);
        }
    }
}

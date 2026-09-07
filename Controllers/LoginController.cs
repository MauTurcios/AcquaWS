using Microsoft.AspNetCore.Mvc;
using AcquaWS.Models;
using Microsoft.EntityFrameworkCore;

namespace AcquaWS.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {

        private readonly ApplicationDbContext _context = null;

        public LoginController(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        //Endpoint para el inicio de Sesión en la App
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try {
                var empleado = await _context.Empleados
                    .FirstOrDefaultAsync(e => e.Usuario_App == loginModel.Usuario && e.Clave_App == loginModel.Password);
                if (empleado != null)
                {

                    var _fechaActual = DateTime.Now;
                    empleado.Estado_App = "ACTIVO";
                    empleado.Ultima_Conexion_App = _fechaActual;
                    empleado.Identidad_App = "USUARIO_ACQUA_" + loginModel.Usuario?.Trim();
                    await _context.SaveChangesAsync();

                    var response = new Response.LoginResponse
                    {
                        IdVendedor = empleado.Id,
                        NombreVendedor = empleado.Empleado?.Trim(),
                        Respuesta = "CREDENCIALES_VALIDAS"
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new Response.LoginResponse
                    {
                        IdVendedor = 0,
                        NombreVendedor = null,
                        Respuesta = "CREDENCIALES_INCORRECTAS"
                    };
                    return Unauthorized(response);
                }
            }
            catch (Exception ex) { 
                var response = new Response.LoginResponse
                {
                    IdVendedor = 0,
                    NombreVendedor = null,
                    Respuesta = "Error en el servidor: " + ex.Message
                };
                return StatusCode(500, response);
            }
        }


        //Endpoint para cerrar Sesion en la app y el servidor
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutModel obj) {
            try
            {
                var empleado = await _context.Empleados
                    .FirstOrDefaultAsync(e => e.Id == obj.IdVendedor);
                if (empleado != null)
                {
                    var _fechaActual = DateTime.Now;
                    empleado.Ultima_Conexion_App = _fechaActual;
                    empleado.Estado_App = "INACTIVO";
                    empleado.Identidad_App = null;
                    await _context.SaveChangesAsync();

                    return Ok(new { Respuesta = "LOGOUT_EXITOSO" });
                }
                else
                {
                    return NotFound(new { Respuesta = "VENDEDOR_NO_ENCONTRADO" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Respuesta = "Error en el servidor: " + ex.Message });
            }
        }


    }
}

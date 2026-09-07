using AcquaWS.Response;
using Microsoft.AspNetCore.Mvc;

namespace AcquaWS.Controllers
{
    [ApiController]
    public class ConexionController : ControllerBase
    {

        [HttpGet("api/conexion")]
        public IActionResult obtenerConexion() {

            return StatusCode(StatusCodes.Status200OK,
                new ConexionRespuesta { Respuesta = "CONEXION_EXITOSA" });

        }

    }
}

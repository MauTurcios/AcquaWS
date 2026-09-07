using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace AcquaWS.DTO
{
    public class LecturasDTO
    {
        public int IdLectura { get; set; }
        public string Cuenta { get; set; } = string.Empty;
        public decimal Lectura_actual { get; set; }
        public string Empleado { get; set; } = string.Empty;
    }
}

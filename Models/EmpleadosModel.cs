namespace AcquaWS.Models
{
    public class EmpleadosModel
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Empleado { get; set; }
        public string? Status { get; set; }
        public string? Usuario_App { get; set; }
        public string? Clave_App { get; set; }
        public string? Identidad_App { get; set; }
        public string? Estado_App { get; set; }
        public DateTime? Ultima_Conexion_App { get; set; }
        public string? Todos_clientes_App { get; set; }
    }
}

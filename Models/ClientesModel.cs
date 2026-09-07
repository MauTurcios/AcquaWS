namespace AcquaWS.Models
{
    public class ClientesModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string? Direccion { get; set; }
        public string? Municipio { get; set; }
        public string? Departamento { get; set; }
        public string? Contacto { get; set; }
        public string? Ruta { get; set; }
        public int Id_ruta { get; set; }
        public string? Info_adicional { get; set; }
        public string? ref_personal_nombre { get; set; }
        public string? Status { get; set; }
        public string? DTEDireccion { get; set; }
        public string? DTECorreo { get; set; }
        public string? Nit { get; set; }
        public string Dui { get; set; } = "";
    }
}

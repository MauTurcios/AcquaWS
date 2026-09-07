namespace AcquaWS.Models
{
    public class ConfigModel
    {
        public int Id { get; set; }
        public string Empresa { get; set; } = "";
        public string? Giro { get; set; }
        public string? Propietario { get; set; }
        public string? Nrc { get; set; }
        public string? Nit { get; set; } 
        public string? Direccion { get; set; }
        public string? DTEPais { get; set; }
        public string? DTEDepto { get; set; }
        public string? DTEMunicipio { get; set; }
        public string? DTEDistrito { get; set; }
        public string? DTENit { get; set; }
        public string? DTENrc { get; set; }
        public string? DTENombreEmisor { get; set; }
        public string? DTEGiro {  get; set; }
        public string? DTEGiroCodigo { get; set; }
        public string? DTENombreComercial { get; set; }
        public string? DTEDireccionEmisor { get; set; }
        public string? DTETelefonoEmisor { get; set; }
        public string? DTECorreoEmisor { get; set ; }
    }
}

namespace AcquaWS.DTO
{
    public class ClientesDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string? Casa { get; set; }
        public string? Poligono { get; set; }
        public int Id_ruta { get; set; }
        public string? Codigo_casa { get; set; }
        public string? Direccion { get; set; }
    }

}

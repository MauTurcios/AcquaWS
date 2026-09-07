namespace AcquaWS.DTO
{
    public class LecturaAnteriorDTO
    {
        public int IdLectura { get; set; }
        public string Cuenta { get; set; } = "";
        public decimal Lectura_anterior { get; set; }
    }
}

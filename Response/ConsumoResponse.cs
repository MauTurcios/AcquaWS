namespace AcquaWS.Response
{
    public class ConsumoResponse
    {
        public long Consumo { get; set; }
        public string LecturaAnterior { get; set; } = "";
        public string LecturaActual { get; set; } = "";
    }
}

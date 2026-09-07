namespace AcquaWS.DTO
{
    public class SRLecturaPrefacturaDTO
    {
        public int Id { get; set; }
        public int IdLectura { get; set; }
        public int IdServicio_recuerrente_cliente { get; set; }
        public decimal Lectura_anterior { get; set; }
        public decimal Lectura_actual { get; set; }
        public decimal Consumo { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTarifa { get; set; }
        public Boolean AppPrefacturado { get; set; }
        public DateTime? AppFechaHoraPrefacturado { get; set; }
        public string? AppMovilPrefacturado { get; set; }
        public string? AppUsuarioPrefacturado { get; set; }
    }
}

namespace AcquaWS.Models
{
    public class SRLecturaPrefacturaModel
    {
        public int Id { get; set; }
        public int IdLectura { get; set; }
        public int IdServicio_recuerrente_cliente { get; set; }
        public Boolean Exonerado { get; set; }
        public decimal Descuento { get; set; }
        public decimal Balance { get; set; }
        public decimal Lectura_anterior { get; set; }
        public decimal Lectura_actual {  get; set; }
        public decimal Consumo { get; set; }
        public int IdCliente { get; set; }
        public string CodCliente { get; set; }
        public int IdCatalogo { get; set; }
        public int IdTarifa { get; set; }
        public Boolean AppPrefacturado { get; set; }
        public DateTime? AppFechaHoraPrefacturado { get; set; }
        public string? AppMovilPrefacturado { get; set;}
        public string? AppUsuarioPrefacturado { get; set;}
        public Boolean Borrado {  get; set; }

    }
}

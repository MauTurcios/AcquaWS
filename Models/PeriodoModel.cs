namespace AcquaWS.Models
{
    public class PeriodoModel
    {
        public int Id { get; set; }
        public int IdCatalogo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Periodo_inicio { get; set; }
        public DateTime Periodo_fin { get; set; }
        public DateTime Fecha_vencimiento { get; set; }
        public string Concepto {  get; set; }
        public string? Estado { get; set; }
        public Boolean borrado { get; set; }
    }
}

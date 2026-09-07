namespace AcquaWS.DTO
{
    public class PeriodoDTO
    {
        public int Id { get; set; }

        public int IdCatalogo { get; set; }

        public string Fecha { get; set; } = "";

        public string Periodo_inicio { get; set; } = "";

        public string Periodo_fin { get; set; } = "";

        public string Fecha_vencimiento { get; set; } = "";

        public string Concepto { get; set; } = "";

        public string Estado { get; set; } = "";
    }
}

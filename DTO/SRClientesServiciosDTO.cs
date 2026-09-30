namespace AcquaWS.DTO
{
    public class SRClientesServiciosDTO
    {
        public int id { get; set; }
        public int Id_Servicios_recurrentes_clientes { get; set; }
        public int IdCatalogo { get; set; }
        public int? IdPlan { get; set; }
        public int? IdTarifa { get; set; }
        public string Recurrencia { get; set; } = "";
        public decimal Precio_personalizado { get; set; }
        public decimal Precio_personalizado_iva { get; set; }
    }
}

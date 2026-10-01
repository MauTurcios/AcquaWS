namespace AcquaWS.Models
{
    public class SRClientesServicios
    {
        public int id { get; set; }
        public int Id_Servicios_recurrentes_clientes { get; set; }
        public int IdCatalogo { get; set; }
        public int? IdPlan { get; set; }
        public int? IdTarifa { get; set; }
        public string Recurrencia { get; set; } = "";
        public decimal Precio_personalizado { get; set; }
        public decimal Precio_iva_personalizado { get; set; }
        public DateTime Creado_Fecha_hora { get; set; }
        public Boolean Borrado { get; set; }
    }
}

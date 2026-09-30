namespace AcquaWS.Models
{
    public class SRClientesModel
    {
        public int id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha_alta { get; set; }
        public DateTime? Fecha_baja { get; set; }
        public DateTime Ultimo_mes_prefacturado { get; set; }
        public DateTime Ultimo_mes_cancelado { get; set; }
        public string? Ultima_factura_cancelada { get; set; }
        public string Status { get; set; }
        public Boolean Borrado { get; set; }
        public string Cuenta { get; set; }
        public int IdCatalogo { get; set; }
        public decimal Balance { get; set; }
        public int IdColbarr { get; set; }
        public int?  IdSector { get; set; } = 0;
        public int? IdSector_zona { get; set; } = 0;
        public string? Medidor { get; set; }
        public string Tipo_factura { get; set; }
        public decimal Ultima_lectura { get; set; }
        public string? Ultima_factura_prefacturada { get; set; }
        public int? IdTarifa { get; set; } = 0;
        public string CodCliente { get; set; }

    }
}

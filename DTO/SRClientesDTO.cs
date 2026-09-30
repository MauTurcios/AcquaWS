namespace AcquaWS.DTO
{
    public class SRClientesDTO
    {
        public int id {  get; set; }
        public int IdCliente { get; set; }
        public string Ultimo_mes_prefacturado { get; set; }
        public string Ultimo_mes_cancelado { get; set; }
        public string? Ultima_factura_cancelada { get; set; }
        public string Status { get; set; }
        public string Cuenta { get; set; }
        public int IdCatalogo { get; set; }
        public decimal Balance { get; set; }
        public int? IdSector { get; set; }
        public string? Tipo_factura { get; set; }
        public decimal Ult_lectura { get; set; }
        public string? Ult_factura_prefacturada { get; set; }
        public int? IdTarifa { get; set; } = 0; 
        public string CodCliente { get; set; }
    }
}

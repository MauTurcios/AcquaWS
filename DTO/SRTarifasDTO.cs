namespace AcquaWS.DTO
{
    public class SRTarifasDTO
    {
        public int Id { get; set; }
        public int idCatalogo { get; set; }
        public string Tarifa { get; set; }

        //Escala inicial m3
        public decimal Primeros_m3 { get; set; }
        public decimal Primeros_m3_valor { get; set; }
        public decimal Primeros_m3_descuento { get; set; }
        //ESCALA primeros m3
        public decimal E1_minimo_m3 { get; set; }
        public decimal E1_maximo_m3 { get; set; }
        public decimal E1_valor_m3 { get; set; }
        //ESCALA egundos m3
        public decimal E2_minimo_m3 { get; set; }
        public decimal E2_maximo_m3 { get; set; }
        public decimal E2_valor_m3 { get; set; }
        //ESCALA terceros m3
        public decimal E3_minimo_m3 { get; set; }
        public decimal E3_maximo_m3 { get; set; }
        public decimal E3_valor_m3 { get; set; }
        //ESCALA cuartos m3
        public decimal E4_minimo_m3 { get; set; }
        public decimal E4_maximo_m3 { get; set; }
        public decimal E4_valor_m3 { get; set; }
        //ESCALA quintos m3
        public decimal E5_minimo_m3 { get; set; }
        public decimal E5_maximo_m3 { get; set; }
        public decimal E5_valor_m3 { get; set; }
        //ESCALA sextos m3
        public decimal E6_minimo_m3 { get; set; }
        public decimal E6_maximo_m3 { get; set; }
        public decimal E6_valor_m3 { get; set; }
        //ESCALA septimos m3
        public decimal E7_minimo_m3 { get; set; }
        public decimal E7_maximo_m3 { get; set; }
        public decimal E7_valor_m3 { get; set; }
        //ESCALA octavos m3
        public decimal E8_minimo_m3 { get; set; }
        public decimal E8_maximo_m3 { get; set; }
        public decimal E8_valor_m3 { get; set; }
        //ESCALA novenos m3
        public decimal E9_minimo_m3 { get; set; }
        public decimal E9_maximo_m3 { get; set; }
        public decimal E9_valor_m3 { get; set; }
        //ESCALA decimos m3
        public decimal E10_minimo_m3 { get; set; }
        public decimal E10_maximo_m3 { get; set; }
        public decimal E10_valor_m3 { get; set; }
        public decimal Canon { get; set; }
        public string Canon_TipoFiscal { get; set; }

        //CARGOS FIJOS 1
        public string? Cf1_Codigo { get; set; }
        public string? Cf1_Descripcion { get; set; }
        public string? Cf1_Linea { get; set; }
        public decimal Cf1_precio { get; set; }
        public string Cf1_TipoFiscal { get; set; }

        //CARGOS FIJOS 2
        public string? Cf2_Codigo { get; set; }
        public string? Cf2_Descripcion { get; set; }
        public string? Cf2_Linea { get; set; }
        public decimal Cf2_precio { get; set; }
        public string Cf2_TipoFiscal { get; set; }

        //CARGOS FIJOS 3
        public string? Cf3_Codigo { get; set; }
        public string? Cf3_Descripcion { get; set; }
        public string? Cf3_Linea { get; set; }
        public decimal Cf3_precio { get; set; }
        public string Cf3_TipoFiscal { get; set; }

        //CARGOS FIJOS 4
        public string? Cf4_Codigo { get; set; }
        public string? Cf4_Descripcion { get; set; }
        public string? Cf4_Linea { get; set; }
        public decimal Cf4_precio { get; set; }
        public string Cf4_TipoFiscal { get; set; }

        //CARGOS FIJOS 5
        public string? Cf5_Codigo { get; set; }
        public string? Cf5_Descripcion { get; set; }
        public string? Cf5_Linea { get; set; }
        public decimal Cf5_precio { get; set; }
        public string Cf5_TipoFiscal { get; set; }

        //ALCANTARILLADOS ESCALA 1
        public decimal Escala1_alcantarillado { get; set; }
        public decimal Escala2_alcantarillado { get; set; }
        public decimal Escala3_alcantarillado { get; set; }
        public decimal Escala4_alcantarillado { get; set; }
        public decimal Escala5_alcantarillado { get; set; }
        public decimal Escala6_alcantarillado { get; set; }
        public decimal Escala7_alcantarillado { get; set; }
        public decimal Escala8_alcantarillado { get; set; }
        public decimal Escala9_alcantarillado { get; set; }
        public decimal Escala10_alcantarillado { get; set; }

        public string PliegoTarifario_TipoFiscal { get; set; }
        public decimal Primeros_m3_alcantarillado { get; set; }
    }
}

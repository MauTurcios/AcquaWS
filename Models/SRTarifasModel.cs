namespace AcquaWS.Models
{
    public class SRTarifasModel
    {
        public int Id { get; set; }
        public int idCatalogo { get; set; }
        public string Tarifa { get; set; }

        //Escala inicial m3
        public decimal Primeros_m3 { get; set; }
        public decimal Primeros_m3_valor { get; set; }
        public decimal Primeros_m3_descuento { get; set; }
        //ESCALA primeros m3
        public decimal Escala1_minimo_m3 { get; set; }
        public decimal Escala1_maximo_m3 { get; set; }
        public decimal Escala1_valor_m3 { get; set; }
        //ESCALA segundos m3
        public decimal Escala2_minimo_m3 { get; set; }
        public decimal Escala2_maximo_m3 { get; set; }
        public decimal Escala2_valor_m3 { get; set; }
        //ESCALA terceros m3
        public decimal Escala3_minimo_m3 { get; set; }
        public decimal Escala3_maximo_m3 { get; set; }
        public decimal Escala3_valor_m3 { get; set; }
        //ESCALA cuartos m3
        public decimal Escala4_minimo_m3 { get; set; }
        public decimal Escala4_maximo_m3 { get; set; }
        public decimal Escala4_valor_m3 { get; set; }
        //ESCALA quintos m3
        public decimal Escala5_minimo_m3 { get; set; }
        public decimal Escala5_maximo_m3 { get; set; }
        public decimal Escala5_valor_m3 { get; set; }
        //ESCALA sextos m3
        public decimal Escala6_minimo_m3 { get; set; }
        public decimal Escala6_maximo_m3 { get; set; }
        public decimal Escala6_valor_m3 { get; set; }
        //ESCALA septimos m3
        public decimal Escala7_minimo_m3 { get; set; }
        public decimal Escala7_maximo_m3 { get; set; }
        public decimal Escala7_valor_m3 { get; set; }
        //ESCALA octavos m3
        public decimal Escala8_minimo_m3 { get; set; }
        public decimal Escala8_maximo_m3 { get; set; }
        public decimal Escala8_valor_m3 { get; set; }
        //ESCALA novenos m3
        public decimal Escala9_minimo_m3 { get; set; }
        public decimal Escala9_maximo_m3 { get; set; }
        public decimal Escala9_valor_m3 { get; set; }
        //ESCALA decimos m3
        public decimal Escala10_minimo_m3 { get; set; }
        public decimal Escala10_maximo_m3 { get; set; }
        public decimal Escala10_valor_m3 { get; set; }
        public decimal Canon {  get; set; }
        public string Canon_TipoFiscal { get; set; }

        //CARGOS FIJOS 1
        public string? CargoFijo1_Codigo { get; set; }
        public string? CargoFijo1_Descripcion { get; set; }
        public string? CargoFijo1_Linea { get; set; }
        public decimal CargoFijo1_precio { get; set; }
        public string CargoFijo1_TipoFiscal { get; set; }

        //CARGOS FIJOS 2
        public string? CargoFijo2_Codigo { get; set; }
        public string? CargoFijo2_Descripcion { get; set; }
        public string? CargoFijo2_Linea { get; set; }
        public decimal CargoFijo2_precio { get; set; }
        public string CargoFijo2_TipoFiscal { get; set; }

        //CARGOS FIJOS 3
        public string? CargoFijo3_Codigo { get; set; }
        public string? CargoFijo3_Descripcion { get; set; }
        public string? CargoFijo3_Linea { get; set; }
        public decimal CargoFijo3_precio { get; set; }
        public string CargoFijo3_TipoFiscal { get; set; }

        //CARGOS FIJOS 4
        public string? CargoFijo4_Codigo { get; set; }
        public string? CargoFijo4_Descripcion { get; set; }
        public string? CargoFijo4_Linea { get; set; }
        public decimal CargoFijo4_precio { get; set; }
        public string CargoFijo4_TipoFiscal { get; set; }

        //CARGOS FIJOS 5
        public string? CargoFijo5_Codigo { get; set; }
        public string? CargoFijo5_Descripcion { get; set; }
        public string? CargoFijo5_Linea { get; set; }
        public decimal CargoFijo5_precio { get; set; }
        public string CargoFijo5_TipoFiscal { get; set; }

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

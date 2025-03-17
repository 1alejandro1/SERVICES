namespace BCP.CROSS.MODELS.Cobro
{
    public class CobroRegistro
    {
        public string DescripcionCobro { get; set; }
        public int IdPeriodo { get; set; }
        //public string DescripcionPeriodo { get; set; }
        public int IdViaEnvio { get; set; }
        //public string DescripcionViaEnvio { get; set; }
        public int IdTarifario { get; set; }//
        //public string DescripcionTarifario { get; set; }
        public string IdProducto { get; set; }
        public string DescripcionProducto { get; set; }//
        public string IdServicio { get; set; }
        public string DescripcionServicio { get; set; }//
        public bool esVisiblePR { get; set; }
        public bool esVisibleSwamp { get; set; }
       // public string VisiblePR { get; set; }
        //public string VisibleSwamp { get; set; }
        public bool esCobroPlataforma { get; set; }
       // public string CobroPlataforma { get; set; }
        public bool esCobroPR { get; set; }
        //public string CobroPR { get; set; }
        public bool esCierraSwamp { get; set; }
       // public string CierraSwamp { get; set; }
        public string Glosa { get; set; }
        public bool esFacturable { get; set; }
       // public string Facturable { get; set; }
        public int IdServicioFacturacion { get; set; }
        //public string DescripcionServicioFacturacion { get; set; }
        public int IdServiciosCanales { get; set; }
       // public string DescripcionServiciosCanales { get; set; }
        public string Funcionario { get; set; }
    }
}

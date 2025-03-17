namespace BCP.CROSS.MODELS.Devolucion
{
    public class DevolucionRegistro
    {
        //public int IdDevolucion { get; set; }
        public string DescripcionDevolucion { get; set; }//
        public int IdTarifario { get; set; }//
        //public string DescripcionTarifario { get; set; }
        public string IdProducto { get; set; }//
        public string DescripcionProducto { get; set; }//
        public string IdServicio { get; set; }//
        public string DescripcionServicio { get; set; }//
        public bool esVisiblePR { get; set; }//
        public bool esVisibleSwamp { get; set; }//
        //public string VisiblePR { get; set; }
        //public string VisibleSwamp { get; set; }
        public int IdServiciosCanales { get; set; }//
       // public string DescripcionServiciosCanales { get; set; }
        public string Glosa { get; set; }//
        public string Funcionario { get; set; }
    }
}

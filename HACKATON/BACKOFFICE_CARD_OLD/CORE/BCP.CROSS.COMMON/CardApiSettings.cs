using System.Collections.Generic;

namespace BCP.CROSS.COMMON
{
    public class CardApiSettings
    {
        public Lexico Lexico { get; set; }
        public string BaseUrl { get; set; }
        public string CardTopList { get; set; }
        public string CardGenerated { get; set; }
        public string CategoryList { get; set; }
        public string CommerceList { get; set; }
        public string ServiceList { get; set; }
        public string ServiceEnroll { get; set; }                      
    }
    public class Lexico
    {
        public List<TipoIdc> TipoIdc { get; set; }       
        public string TipoError { get; set; }
        public string ListCommerceResponse { get; set; }
    }
    
    
    public class TipoIdc
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}

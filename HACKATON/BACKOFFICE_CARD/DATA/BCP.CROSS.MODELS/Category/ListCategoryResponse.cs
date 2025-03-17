using System.Collections.Generic;

namespace BCP.CROSS.MODELS.Category
{
    public class ListCategoryResponse
    {
        public List<Categories> categories { get; set; }

    }   
    
    public class Categories
    {
        public int id { get; set; }
        public string name { get; set; }
        public string categoryCode { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
    }

}

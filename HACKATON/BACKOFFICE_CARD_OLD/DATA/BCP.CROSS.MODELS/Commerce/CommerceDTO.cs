using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Commerce
{
    public class CommerceDTO
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public string commerceCode { get; set; }
        public string businessName { get; set; }
        public string nit { get; set; }
        public string phoneNumber { get; set; }
        public string city { get; set; }
        public string logoUrl { get; set; }
        public string socialNetworkLink { get; set; }
    }
}

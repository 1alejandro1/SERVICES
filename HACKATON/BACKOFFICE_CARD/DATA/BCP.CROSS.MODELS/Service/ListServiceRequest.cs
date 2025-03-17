using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Service
{
    public class ListServiceRequest
    {
        public string publicToken { get; set; }
        public string appUserId { get; set; }
        public int idCommerce { get; set; }
    }
}

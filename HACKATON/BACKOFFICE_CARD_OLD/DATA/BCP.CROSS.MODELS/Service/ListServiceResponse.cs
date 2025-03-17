using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Service
{
    public class ListServiceResponse
    {
        public List<Services> services { get; set; }
    }
    public class Services
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string serviceCode { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string discountText { get; set; }
        public string disclaimer { get; set; }
        public float discountAmount { get; set; }
        public float discountRate { get; set; }
        public string maximumAmount { get; set; }
        public string type { get; set; }
        public string whileSuppliesLast { get; set; }
        public string largeImageUrl { get; set; }
        public string smallImageUrl { get; set; }
        public string city { get; set; }
        public string cityDescription { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
    }
}

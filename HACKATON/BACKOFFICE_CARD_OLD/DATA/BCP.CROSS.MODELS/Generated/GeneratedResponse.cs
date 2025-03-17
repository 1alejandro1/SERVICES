using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Generated
{
    public class GeneratedResponse
    {
        public Data data { get; set; }
        public int state { get; set; }
        public string message { get; set; }
        public class Data
        {
            public string serviceCode { get; set; }
            public Card card { get; set; }           
            
            public class Card
            {
                public long id { get; set; }
                public string templateUrl { get; set; }
                public string name { get; set; }
                public string cardCommerceId { get; set; }
                public string cardCommerceCode { get; set; }
                public ImageCode imageCode { get; set; }
                public string activationDate { get; set; }
                public string expirationDate { get; set; }
                public string status { get; set; }
                public string statusDescription { get; set; }
                public class ImageCode
                {
                    public string type { get; set; }
                    public int axisX { get; set; }
                    public int axisY { get; set; }
                    public string image { get; set; }
                    public string code { get; set; }
                    public int width { get; set; }
                    public int height { get; set; }

                }
            }
        }
    }
}

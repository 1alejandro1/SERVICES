using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMarketingCRM
{
    public partial class VisorImagen1 : System.Web.UI.Page
    {
        public string ImagePath { get; set; }
        public string ImageUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImagePath = @"D:\Imagenes\BCP.jpg";
            ImageUrl = "http://www.google.com";
        }
        
    }
}
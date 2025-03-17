using MICREDITO.Model.Service;
using System.Collections.Generic;

namespace MICREDITO.WebSite.Models
{
    public class HomeModel
    {
        public BusquedaModel busqueda { get; set; }
        public List<BandejaResponseModel> bandeja { get; set; }
    }
}
using BCP.CROSS.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sarc.WebApp.Models.Supervisor
{
    public class ProccessRechazoCasoViewModel
    {

        public ServiceResponse<bool?> CasoRechazado { get; set; }
        public string NroCaso { get; set; }
    }
}

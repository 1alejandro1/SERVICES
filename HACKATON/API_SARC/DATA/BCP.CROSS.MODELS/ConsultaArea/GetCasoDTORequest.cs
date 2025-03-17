using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.ConsultaArea
{
    public class GetCasoDTORequest
    {
        [Required]
        public string NroCaso { get; set; }
    }
}

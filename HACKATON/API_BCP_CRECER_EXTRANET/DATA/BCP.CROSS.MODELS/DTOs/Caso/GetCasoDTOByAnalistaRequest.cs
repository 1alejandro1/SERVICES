using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class GetCasoDTOByAnalistaRequest
    {
        public string Usuario { get; set; }
        public string Estado { get; set; }
    }
}
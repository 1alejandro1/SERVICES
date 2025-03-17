using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCP.CROSS.MODELS.Caso
{

    public class UpdateOrigenCasoRequest
    {
        public string NroCarta { get; set; }
        public string Area { get; set; }

        [MaxLength(35)]
        public string Nombre { get; set; }

        [MaxLength(25)]
        public string Paterno { get; set; }

        [MaxLength(25)]
        public string Materno { get; set; }

        [MaxLength(35)]
        public string Matricula { get; set; }
    }
}
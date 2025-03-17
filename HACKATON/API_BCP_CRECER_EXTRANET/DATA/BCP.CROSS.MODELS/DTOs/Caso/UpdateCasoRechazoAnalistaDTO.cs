using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateCasoRechazoAnalistaDTO
    {
        public string FechaRegistro { get; set; }
        public string FuncionarioRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string NroCaso { get; set; }
        public string Estado { get; set; }
        public string AntServ { get; set; }
        public string ProductoId { get; set; }
        public string ServicioId { get; set; }
        public string SWErrorReg { get; set; }
        public string IdRegistroError { get; set; }
        public string DescripcionRegistroError { get; set; }
    }
}

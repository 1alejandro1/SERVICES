using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Tarifario
{
    public class TarifarioModificacion
    {
        public int TarifarioId { get; set; }
        public string Descripcion { get; set; }
        public string TipoCodigo { get; set; }
        public string FuncionarioRegistro { get; set; }
        public decimal Importe { get; set; }
    }
}

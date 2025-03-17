using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class UpdateCasoSolucionDTO
    {
        public string FuncionarioModificacion {get; set;}
        public string NroCarta{ get; set; }
        public string Estado {get; set;}
        public string TipoSolucion {get; set;}
        public string DescripcionSolucion {get; set;}
        public string SucursalSolucion {get; set;}
        public string DocumentoAdjuntoOut {get; set;}
        public string TipoCarta {get; set;}
        public decimal ImporteDevolucion {get; set;}
        public string MonedaDevolucion {get; set;}

        public UpdateCasoSolucionDTO()
        {
            FuncionarioModificacion = string.Empty;
            NroCarta = string.Empty;
            Estado = string.Empty;
            TipoSolucion = string.Empty;
            DescripcionSolucion = string.Empty;
            SucursalSolucion = string.Empty;
            DocumentoAdjuntoOut = string.Empty;
            TipoCarta = string.Empty;
            MonedaDevolucion = string.Empty;
        }
    }
}

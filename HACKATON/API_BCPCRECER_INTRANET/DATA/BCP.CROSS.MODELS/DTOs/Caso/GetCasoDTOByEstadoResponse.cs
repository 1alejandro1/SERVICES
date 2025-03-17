using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class GetCasoDTOByEstadoResponse
    {
        public string FechaRegistro { get; set; }
        public string NroCaso { get; set; }
        public string IdcCliente { get; set; }
        public string FuncionarioAtencion { get; set; }
        public string FuncionarioNombre { get; set; }
        public string Estado { get; set; }
        public string InformacionAdicional { get; set; }
        public int Dias { get; set; }
        public int DiasAtencion { get; set; }
        public int DiasDiferencia { get; set; }
        public string Descripcion { get; set; }
        public string NombreEmpresa { get; set; }
    }
}

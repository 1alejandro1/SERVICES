using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class GetCasoDTOByAnalistaResponse
    {
        public string NroCaso { get; set; }
        public string IdcCliente { get; set; }
        public string IdcNumero { get; set; }
        public string IdcTipo { get; set; }
        public string IdcExtension { get; set; }

        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public string NombreEmpresa { get; set; }
        public string Estado { get; set; }
        public string EstadoDescripcion { get; set; }

        public string ProductoId { get; set; }
        public string Producto { get; set; }
        public string ServicioId { get; set; }
        public string Servicio { get; set; }

        public string FechaRegistro { get; set; }
        public string FuncionarioRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string FuncionarioAtencion { get; set; }

        public string InformacionAdicional { get; set; }

        public int DiasAtencion { get; set; }
        public int DiasLimite { get; set; }
        public int DiasDiferencia { get; set; }

        public string Sucursal { get; set; }
        public string DescripcionErrorRegistro { get; set; }
    }
}

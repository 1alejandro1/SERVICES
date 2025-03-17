using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Infocliente
{
    public class ClientePjByIdcResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string Codigo { get; set; }
        public string Error { get; set; }
        public List<DatosInfoClienteIDCJuridico> Data { get; set; }
    }

    public class DatosInfoClienteIDCJuridico
    {
        public int cjCliJurId { get; set; }

        public string cjCic { get; set; }

        public string cjNroNit { get; set; }

        public string cjNroNit_actual { get; set; }

        public string cjExtNit { get; set; }

        public string cjExtNit_actual { get; set; }

        public string cjTipoNit { get; set; }
        public string cjTipoNit_actual { get; set; }
        public string cjRazonSocial { get; set; }
        public string cjRazonSocial_actual { get; set; }
        public string cjNombreComercial { get; set; }
        public string cjEstado { get; set; }
        public string cjUsuario { get; set; }
        public string cjCanal { get; set; }

        public string cjFechaUltimaModificacion { get; set; }

        public string cjFechaCreacion { get; set; }

        public string cjIdcCompleto { get; set; }

        public string cjIdcCompletoSinTipo { get; set; }

        public bool bolivia { get; set; }

        public bool peru { get; set; }

    }
}

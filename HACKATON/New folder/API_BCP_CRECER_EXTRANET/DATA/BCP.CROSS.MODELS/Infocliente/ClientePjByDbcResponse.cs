using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Infocliente
{
    public class ClientePjByDbcResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string Codigo { get; set; }
        public string Error { get; set; }
        public List<DatosInfoClienteDBCJuridico> data { get; set; }
    }
}
public class DatosInfoClienteDBCJuridico
{
    public string nit { get; set; }

    public string tipoNit { get; set; }

    public string extensionNit { get; set; }

    public string cic { get; set; }

    public string razonSocial { get; set; }

    public string nombreComercial { get; set; }
    public string telefono { get; set; }
    public string direccion { get; set; }

}


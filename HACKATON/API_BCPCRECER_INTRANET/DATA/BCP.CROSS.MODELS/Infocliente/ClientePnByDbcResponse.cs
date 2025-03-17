using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Infocliente
{
    public class ClientePnByDbcResponse
    {
        public List<DatosInfoClienteAlfabetico> Data { get; set; }
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public bool Exito { get; set; }
        public string Operation { get; set; }
    }
}
public class DatosInfoClienteAlfabetico
{
    public string IdcNumero { get; set; }

    public string IdcTipo { get; set; }

    public string IdcExtension { get; set; }

    public string IdcComplemento { get; set; }

    public string Nombres { get; set; }
    public string Paterno { get; set; }
    public string Materno { get; set; }
    public string Celular { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Cic { get; set; }

}


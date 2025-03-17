using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public  class Reclamos
    {
        public string NRO_CARTA { get; set; }
        public string IDC_CLIE { get; set; }
        public string IDC_TIPO { get; set; }
        public string IDC_EXT { get; set; }
        public string IDC_CLIENTE { get; set; }
        public string PATERNO { get; set; }
        public string MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string EMPRESA { get; set; }
        public string ESTADO { get; set; }
        public string DESCRIPCION_ESTADO { get; set; }
        public string PRODUCTOID { get; set; }
        public string PRODUCTO_DESCRIPCION { get; set; }
        public string SERVICIOID { get; set; }
        public string SERVICIO_DESCRIPCION { get; set; }
        public string FUNC_ATN { get; set; }
        public string FUNC_REG { get; set; }
        public string FEC_REG { get; set; }
        public string HORA_REG { get; set; }
        public string INF_ADICIONAL { get; set; }
        public int NRO_DIAS_ATN { get; set; }
        public int NRO_DIAS_LIMITE { get; set; }
        public int NRO_DIAS_RETRASO { get; set; }
        public string SUCURSAL { get; set; }
        public string DESC_ERROR_REG { get; set; }
    }
}

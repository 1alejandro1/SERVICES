using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes
{
    //exec DEVOLICIONES_COBROS_LOG_list @FEC_INI=N'20211128',@FEC_FIN=N'20211215',@FUNC=N'',@CANAL=0,@ESTADO=-1,@TIPO=0
    public class CobrosDevolucionesModel
    {
        public DateTime Fecha { get; set; }
        public string IDC { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public string DescTipo { get; set; } //DESC_TIPO
        public string Cuenta { get; set; }
        public decimal Importe { get; set; }
        public string Moneda { get; set; }
        public string Descmoneda { get; set; } //DESC_MONEDA
        public int IdCanal { get; set; } //ID_CANAL
        public string DescCanal { get; set; } //DESC_CANAL
        public string Funcionario { get; set; }
        public string Supervisor { get; set; }
        public string NroCaso { get; set; } //NRO_CASO
        public bool Completado { get; set; }
        public string DescCompletado { get; set; } //DESC_COMPLETADO
        public string Agencia { get; set; }
        public string NroAgencia { get; set; } //NRO_AGENCIA
        public string NombreAgencia { get; set; } //NOMBRE_AGENCIA
        public string Sucursal { get; set; }
    }
}

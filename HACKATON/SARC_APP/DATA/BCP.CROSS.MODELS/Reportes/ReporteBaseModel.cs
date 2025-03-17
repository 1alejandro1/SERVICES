using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Reportes
{
    //SP=>exec SARC.SP_REP_BASE @FECHAINI=N'20211128',@FECHAFIN=N'20211222'
    public class ReporteBaseModel
    {
        public string Carta { get; set; }
        public string Estado { get; set; }
        public string TipoServicio { get; set; }
        public string DiasServicio { get; set; }
        public string Servicio { get; set; }
        public string Producto { get; set; }
        public string Agencia { get; set; }
        public string DescAgencia { get; set; }
        public string FunReg { get; set; }
        public string FunAte { get; set; }
        public string NomFunCate { get; set; }
        public string Sucsol { get; set; }
        public string FecReg { get; set; }
        public string FecIni { get; set; }
        public string FecFin { get; set; }
        public string IDC { get; set; }
        public string IDCTipo { get; set; }
        public string IDCExt { get; set; }
        public string NroCuenta { get; set; }
        public string NroTarjeta { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string DirCli { get; set; }
        public string DirEnvio { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string AreaRor { get; set; }
        public string UsrOr { get; set; }
        public string NomOr { get; set; }
        public string DesReclamo { get; set; }
        public string WsDesc { get; set; }
        public string AreaResp { get; set; }
        public string AreaDesc { get; set; }
        public string TipoSol { get; set; }
        public string DescSol { get; set; }
        public string ATMSuc { get; set; }
        public string ATMUbi { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string FecTXN { get; set; }
        public string ViaEnvio { get; set; }
        public decimal ImporteDev { get; set; }
        public string MonedaDev { get; set; }
        public string SwReg { get; set; }
        public string DescErrorReg { get; set; }
        public int T { get; set; }
        public string Tarea { get; set; }
        public string Clasificacion { get; set; }
        public string AreaExt { get; set; }
    }
}

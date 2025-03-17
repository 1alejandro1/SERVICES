using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios;
using Bitacoras;
using System.Configuration;

namespace WebMarketingCRM
{
    public partial class VisorLog : System.Web.UI.Page
    {
        Negocios neg = new Negocios();
        #region Parametros de Bitacoras
        //INICIALIZA BITACORAS POR PARAMETROS
        public enum TiposLogger
        { EV, TextLog }
        private static readonly IBitacora Bitacora = BitacoraAdmin.ObtenerLogger("BitacoraEventLog");
        private static readonly IBitacora BitacoraText = BitacoraAdmin.ObtenerLogger(TiposLogger.TextLog);
        private static readonly string Destinatario = ConfigurationManager.AppSettings.Get("Destinatario");
        private static readonly string Asunto = ConfigurationManager.AppSettings.Get("Asunto");

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string codigocampania = string.Empty;
                    codigocampania = Convert.ToString(Session["Proceso"] == null ? string.Empty : Session["Proceso"]);
                    if (codigocampania != string.Empty)
                    {
                        GvwLog.DataSource = neg.LogCampanias(codigocampania);
                        GvwLog.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Bitacora.Error("Page_Load()", "VisorLog.aspx", ex);
                BitacoraText.Error("Page_Load()", "Visorlog.aspx", ex);
            }
        }
    }
}
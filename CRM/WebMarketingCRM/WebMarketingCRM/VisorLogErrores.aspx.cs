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
    public partial class VisorLogErrores : System.Web.UI.Page
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
                    lblCodigoCampania.Text = string.Empty;
                    codigocampania = Convert.ToString(Session["CodCampania"] == null ? string.Empty : Session["CodCampania"]);
                    if (codigocampania != string.Empty)
                    {
                        lblCodigoCampania.Text = codigocampania;
                        GvwLog.DataSource = neg.LogCampaniasCarga(codigocampania);
                        GvwLog.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Bitacora.Error("Page_Load()", "VisorLogErrores.aspx", ex);
                BitacoraText.Error("Page_Load()", "VisorlogErrores.aspx", ex);
            }
        }
    }
}
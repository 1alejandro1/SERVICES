using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDeNegocios;
using Bitacoras;
using System.Configuration;
using System.Data;

namespace WebMarketingCRM
{
    public partial class MonitorCargasArchivosPlanos : System.Web.UI.Page
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


        protected void btnVerLog_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string selectedValue = rdbOpciones.SelectedValue;
            string mensaje;
            lblMensaje.Text = "";
            if (selectedValue == "1")
            {
                if (txtCodigoCampania.Text.Length == 0)
                {
                    lblMensaje.Text = "Debe introducir el codigo de campaña.";
                    return;
                }
                dt = neg._DatosLogEjecucion(3, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();
                //dt1 = neg._ObtenerDetalle(txtCodigoCampania.Text);
                dt1 = neg._ObtenerCabecera(txtCodigoCampania.Text);
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
            if (selectedValue == "2")
            {
                dt = neg._DatosLogEjecucion(1, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();

                dt1 = neg._ActualizacionPropietarios();
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
            if (selectedValue == "3")
            {
                dt = neg._DatosLogEjecucion(2, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();
                dt1 = neg._ActualizacionDatosCliente();
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
            if (selectedValue == "4")
            {
                if (txtCodigoCampania.Text.Length == 0)
                {
                    lblMensaje.Text = "Debe introducir el codigo de campaña.";
                    return;
                }
                mensaje = neg._EjecucionProceso(txtCodigoCampania.Text);
                dt = neg._DatosLogEjecucion(3, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
            if (selectedValue == "5")
            {
                dt = neg._DatosLogEjecucion(4, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();
                dt1 = neg._CierreActividadesMarketing();
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
            if (selectedValue == "6")
            {
                dt = neg._DatosLogEjecucion(6, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
            if (selectedValue == "7")
            {
                dt = neg._DatosLogEjecucion(7, txtCodigoCampania.Text);
                grvDatos1.DataSource = dt;
                grvDatos1.DataBind();
                grvDatos2.DataSource = dt1;
                grvDatos2.DataBind();
            }
        }

        protected void rdbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = rdbOpciones.SelectedValue;
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            grvDatos1.DataSource = dt;
            grvDatos1.DataBind();
            grvDatos2.DataSource = dt1;
            grvDatos2.DataBind();
            lblMensaje.Text = "";
            if (selectedValue == "1")
            {
                lblCodigo.Visible = true;
                txtCodigoCampania.Visible = true;
                btnVerLog.Text = "Ver Logs";
            }
            if (selectedValue == "2")
            {
                lblCodigo.Visible = false;
                txtCodigoCampania.Visible = false;
                btnVerLog.Text = "Ver Logs";
            }
            if (selectedValue == "3")
            {
                lblCodigo.Visible = false;
                txtCodigoCampania.Visible = false;
                btnVerLog.Text = "Ver Logs";
            }
            if (selectedValue == "4")
            {
                lblCodigo.Visible = true;
                txtCodigoCampania.Visible = true;
                btnVerLog.Text = "Ejecuta Proceso";
            }
            if (selectedValue == "5")
            {
                lblCodigo.Visible = false;
                txtCodigoCampania.Visible = false;
                btnVerLog.Text = "Ver Logs";
            }
            if (selectedValue == "6")
            {
                lblCodigo.Visible = false;
                txtCodigoCampania.Visible = false;
                btnVerLog.Text = "Ver Logs";
            }
            if (selectedValue == "7")
            {
                lblCodigo.Visible = false;
                txtCodigoCampania.Visible = false;
                btnVerLog.Text = "Ver Logs";
            }
        }
    }
}
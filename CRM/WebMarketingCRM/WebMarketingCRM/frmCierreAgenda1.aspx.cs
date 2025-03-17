using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bitacoras;
using CapaDeNegocios;
using System.Configuration;
using System.Data;
using System.Globalization;

namespace WebCRM
{
    public partial class frmCierreAgenda1 : System.Web.UI.Page
    {
       
        Negocios neg = new Negocios();
        #region Parametros de Bitacoras
        //INICIALIZA BITACORAS POR PARAMETROS
        public enum TiposLogger
        { EV, TextLog }
        private static readonly IBitacora Bitacora = BitacoraAdmin.ObtenerLogger("BitacoraEventLog");
        private static readonly IBitacora BitacoraText = BitacoraAdmin.ObtenerLogger(TiposLogger.TextLog);
        private static readonly string Destinatario = ConfigurationManager.AppSettings.Get("CRM.Destinatario");
        private static readonly string Asunto = ConfigurationManager.AppSettings.Get("CRM.Asunto");

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                this.txtUsuarioConectado.Text = HttpContext.Current.User.Identity.Name.ToUpper();// @"BTBNET\BC2885";//
                //Captura datos pasados por el formulario llamador
                this.txtEntidadId.Text = Request.QueryString["id"]; //"6CF58514-E53B-EF11-A0EE-005056BB8F77"; //

                this.txtEntidadTipo.Text = Request.QueryString["type"];
                //"3"; 
                this.txtEntidadNombre.Text =  Request.QueryString["TypeName"];
                //"opportunity";
                //Valida que la actividad esta basada en una campaña y que no este cerrada
                ExisteCierreOportunidad(this.txtEntidadId.Text);
               
                //Sugiere valores
                //if ((this.txtFechaCierre.Text.Length) == 0)
                //{
                //    this.txtFechaCierre.Text = (System.DateTime.Today.Day.ToString("00") ) + "/" + (System.DateTime.Today.Month.ToString( "00")) + "/" + (System.DateTime.Today.Year.ToString( "0000"));
                //}
                //if ((this.txtIngresosReales.Text.Length) == 0)
                //{
                //    this.txtIngresosReales.Text = "0";
                //}

                //Funciones.TextBox_CargarValidadorNumeros(txtIngresosReales);
                //Funciones.TextBox_CargarValidadorNumerosSlash(txtFechaCierre);

            }
            else
            {
                if (this.ddlEstado.SelectedItem == null)
                {
                    Response.Write("El combo estado no tiene seleccionado ningun elemento");
                }
            }
        }

        private void HabilitaCampos()
        {
            
            this.txtFechaCierre.Enabled = true;
            this.txtIngresosReales.Enabled = true;
            this.ddlEstado.Enabled = true;
            this.txtProbabilidadCierre.Enabled=true;
            this.btnProcesar.Enabled = true;
        }

        private void DeshabilitaCampos()
        {
            
            this.txtFechaCierre.Enabled = false;
            this.txtIngresosReales.Enabled = false;
            this.ddlEstado.Enabled = false;
            this.txtProbabilidadCierre.Enabled = false;
            this.btnProcesar.Enabled = false;
        }

        private bool ExisteCierreOportunidad(string EntidadId)
        {
            bool booEncontroEnTblRespuesta = false;
            string strFechaFin;
            DateTime hoy = DateTime.Today;
            try
            {
                DataTable dt = neg._ObtenerAgenda(EntidadId, this.txtUsuarioConectado.Text);
                //Primero busca datos en tblCierreOportunidad, si existe retorna verdadero

                if (dt == null)
                {
                    return false;
                }

                foreach(DataRow dr in dt.Rows)
                {
                    object temp;
                    booEncontroEnTblRespuesta = true;
                    //Me.ddlEstado.SelectedItem.Value = CStr(dr("copEstado"))
                    this.ddlEstado.ClearSelection();
                    if (DBNull.Value != (temp = dr["new_Respuesta"]))
                        this.ddlEstado.Items.FindByValue(Convert.ToString(dr["new_Respuesta"])).Selected = true;
                    
                    

                    this.txtFechaCierre.Text = DBNull.Value != (temp = dr["new_Fechadeoportunidad"]) ? dr["new_Fechadeoportunidad"].ToString() : "";  // Convert.ToString((  (dr("copFechaCierre")) ? "" : dr("copFechaCierre")));
                    
                    this.txtIngresosReales.Text = dr["new_MontoCierre"].ToString();
                    this.txtProbabilidadCierre.Text = dr["new_ProbabilidadCierre"].ToString();
                    this.txtFechaFin.Text = DBNull.Value != (temp = dr["new_FechaFIn"]) ? dr["new_FechaFIn"].ToString() : "";  // Convert.ToString((  (dr("copFechaCierre")) ? "" : dr("copFechaCierre")));
                    this.txtFuncionario.Text = dr["new_FuncionarioAsignado"].ToString();
                    this.txtPadre.Text = dr["new_AgendaId"].ToString();
                    booEncontroEnTblRespuesta = Convert.ToBoolean(dr["valida"].ToString());
                    this.txtUsuarioGestiona.Text = dr["FuncionarioGestiono"].ToString();
                    this.txtMatriculaConectado.Text = dr["MatriculaConectado"].ToString();
                    this.txtDescripcion.Text = dr["new_Descripcion"].ToString();
                }

                TextBox_CargarValidadorNumeros(txtIngresosReales);
                TextBox_CargarValidadorNumerosSlash(txtFechaCierre);
                strFechaFin = this.txtFechaFin.Text;
                //strFechaFin = FormateFecha_char10(strFechaFin);
                if (booEncontroEnTblRespuesta && this.txtUsuarioGestiona.Text != this.txtMatriculaConectado.Text)
                {
                    this.lblMensaje.Text = "Otro usuario gestiono el cliente y solo el puede modificar el registro";
                    DeshabilitaCampos();
                }
                //validamos si la fecha de fin es igual o mayor a hoy para poder gestionar
                if (DateTime.ParseExact(strFechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.Today)
                {
                    this.lblMensaje.Text = "La fecha de fin de la actividad es menor a la fecha de hoy no se puede gestionar ";
                    DeshabilitaCampos();
                }
            }

            catch (Exception ex)
            {
                this.lblMensaje.Text = "Error: " + ex.Message + " - " + ex.Source;
            }

            return false;
            
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Convert.ToInt32(this.ddlEstado.SelectedItem.Value) == 0)
                {
                    this.lblMensaje.Text = "Debe seleccionar una respuesta..";
                    return;
                }
                this.btnProcesar.Enabled = false;
                //Limpia mensaje
                this.lblMensaje.Text = "";

                //Declara Variables
                int intEstado = 0;
                int intProbabilidad = 0;
                double dblIngresosReales = 0;
                System.DateTime fchFechaCierre = default(System.DateTime);
                string strFEC = null;
                Guid guiCompetidorId = default(Guid);
                string strEntidadId = null;
                int intEntidadTipo = 0;
                string strEntidadNombre = null;
                
                string strSQL = null;
                string strSubject = null;
                string strFechaVencimiento = null;
                string strIngresos = this.txtIngresosReales.Text;
                string strProbablidad = this.txtProbabilidadCierre.Text;
                string strDescripcion = this.txtDescripcion.Text;
                strFEC = this.txtFechaCierre.Text;
                if (string.IsNullOrEmpty(strIngresos))
                    strIngresos = "0";
                if (string.IsNullOrEmpty(strProbablidad))
                    strProbablidad = "0";
                if (string.IsNullOrEmpty(strFEC))
                { 
                    strFEC = "";
                }
                else
                    strFEC = FormateFecha_char10(strFEC);
               
                //Inicializa Variables
                intEstado = Convert.ToInt32(this.ddlEstado.SelectedItem.Value);
                strEntidadId = this.txtEntidadId.Text;
                dblIngresosReales = Convert.ToDouble(strIngresos);
                intProbabilidad = Convert.ToInt32(strProbablidad);
                
                
                try
                {
                    neg._RegistraGestion(strEntidadId, intEstado, dblIngresosReales, intProbabilidad, strFEC, this.txtFuncionario.Text, this.txtUsuarioConectado.Text, this.txtPadre.Text, strDescripcion);


                    //MENSAJE DE EXITO
                    this.lblMensaje.Text = "Proceso realizado exitosamente...";
                    DeshabilitaCampos();
                }
                catch (Exception ex)
                {
                    this.lblMensaje.Text = "Error: " + ex.Message + " - " + ex.Source;
                }


                

            }
        }

        public static string FormateFecha_char10(string strFEC)
        {
            DateTime dt = DateTime.ParseExact(strFEC, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            return dt.Month.ToString("00") + "/" + dt.Day.ToString("00") + "/" + dt.Year.ToString("0000");
        }
        public static void TextBox_CargarValidadorNumerosSlash(TextBox TextBoxIn)
        {
            TextBoxIn.Attributes.Add("onKeypress", "javascript:if ((event.keyCode<47 || event.keyCode>47) && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false;");
        }


        //private string FechaVencimiento(string EntidadID)
        //{
        //    return neg.FechaVencimientoOportunidad(EntidadID);
        //}
        public static void TextBox_CargarValidadorNumeros(TextBox TextBoxIn)
        {
            TextBoxIn.Attributes.Add("onKeypress", "javascript:if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;");
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtFechaCierre.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
            Calendar1.Visible = false;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = true;
        }
    }
}
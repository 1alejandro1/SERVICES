using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bitacoras;
using CapaDeNegocios;
namespace WebMarketingCRM
{
    public partial class VisorImagen : System.Web.UI.Page
    {
        Negocios neg = new Negocios();
        int entidadTipo = 0;
        string entidadNombre = "";
        string activityid;
        string nombreImagen = "";
        #region Parametros de Bitacoras
        //INICIALIZA BITACORAS POR PARAMETROS
        public enum TiposLogger
        { EV, TextLog }
        private static readonly IBitacora Bitacora = BitacoraAdmin.ObtenerLogger("BitacoraEventLog");
        private static readonly IBitacora BitacoraText = BitacoraAdmin.ObtenerLogger(TiposLogger.TextLog);
        private static readonly string Destinatario = ConfigurationManager.AppSettings.Get("Destinatario");
        private static readonly string Asunto = ConfigurationManager.AppSettings.Get("Asunto");

        #endregion
        public string getPathName()
        {
            return @"D:\Imagenes\BCP.jpg";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //String path = Server.MapPath("~\\Imagenes\\");
            
            try
            {

                activityid = Request.QueryString["id"];  //Id de la actividad
                entidadTipo = Convert.ToInt32(Request.QueryString["type"]); //Tipo de Actividad
                entidadNombre = Request.QueryString["TypeName"];   //Nombre de la actividad
                                                                  //Se obtiene el usuario de dominio del usuario conectado
                //activityid = "857A817D-0C1A-EF11-A0EC-005056BB8F77";
                //entidadTipo = 4210;
                //entidadNombre = "phonecall";

                Guid ActivityId = new Guid(activityid);

                //metodos
                nombreImagen = neg.NombreImagen(ActivityId);

                //Session["activityid"] = activityid;
                //Session["entidadTipo"] = entidadTipo;
                //Session["entidadNombre"] = entidadNombre;
                Image1.ImageUrl = @"~/Imagenes/"+ nombreImagen;


            }

            catch (Exception ex)
            {
                Response.Write("<scrip>alert('" + activityid + "');</script>");
                Response.Write("<scrip>alert('" + entidadTipo + "');</script>");
                Response.Write("<scrip>alert('" + entidadNombre + "');</script>");

                BitacoraText.Error("Page_Load", "error en carga", ex);
            }
        }
    }
}
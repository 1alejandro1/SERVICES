using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bitacoras;
using CapaDeNegocios;
using System.Text.Json;
namespace WebMarketingCRM
{
    public partial class VisorIASpeech : System.Web.UI.Page
    {
        Negocios neg = new Negocios();
        int entidadTipo = 0;
        string entidadNombre = "";
        string id;
        string datoCliente = "";
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
            //String path = Server.MapPath("~\\Imagenes\\");
            
            try
            {

                id = Request.QueryString["id"];  //Id de la actividad
                entidadTipo = Convert.ToInt32(Request.QueryString["type"]); //Tipo de Actividad
                entidadNombre = Request.QueryString["TypeName"];   //Nombre de la actividad
                //Se obtiene el usuario de dominio del usuario conectado
                //id = "D0C269D0-F68B-EF11-A0FE-005056BB8F77";
                //entidadTipo = 2;
                //entidadNombre = "contact";
                //txtIASpeech.Text = id + '/' + entidadTipo + '/' + entidadNombre;

                Session["id"] = id;
                Session["entidadTipo"] = entidadTipo;
                Session["entidadNombre"] = entidadNombre;
                


            }

            catch (Exception ex)
            {
                Response.Write("<scrip>alert('" + id + "');</script>");
                Response.Write("<scrip>alert('" + entidadTipo + "');</script>");
                Response.Write("<scrip>alert('" + entidadNombre + "');</script>");

                BitacoraText.Error("Page_Load", "error en carga", ex);
            }
        }

        protected void btnConsultaIA_Click(object sender, EventArgs e)
        {
            id = Session["id"].ToString();
            Guid Id = new Guid(id);
            string Nombre = "";
            string CIC = "";
           
            //metodos
            datoCliente = neg.DatosCliente(Id);
            Nombre = datoCliente.Substring(0, datoCliente.IndexOf(","));
            CIC = datoCliente.Substring(datoCliente.IndexOf(",") + 1);
            txtIASpeech.Text = "Por favor espere se esta consultando....";

            txtIASpeech.Text = PostItem(Nombre,CIC);
        }

        public string PostItem(string nombre, string CIC)
        {
            var url = ConfigurationSettings.AppSettings["URLAPIIA"]; 
  
            var request = (HttpWebRequest)WebRequest.Create(url);
            IARequest datos = new IARequest();
            IAResponse respuesta = new IAResponse();
            datos.cic = CIC;
            datos.name= nombre;
            respuesta.data = new Data();
            //respuesta.data.text = "PRUEBA";
            //respuesta.message = "OK";
            //respuesta.statusCode = "00";       
            string responseBody = null;
            //string json = JsonSerializer.Serialize(respuesta);
            string json = JsonSerializer.Serialize(datos);
            //string json = $"{{\"cic\":\"{CIC}\",\"name\":\"{nombre}\"}}";

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return "";
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                            respuesta = JsonSerializer.Deserialize<IAResponse>(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            if (respuesta.message == "OK" && respuesta.statusCode == "00")
                return respuesta.data.text;
            else
                return "ERROR EN EL API";
            
        }
    }
}
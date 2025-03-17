using BCP.CROSS.SECRYPT;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using srvSharepointDws;
using System.Net;
using System.ServiceModel;
using srvSharepointCopy;
using System.IO;
using BCP.CROSS.MODELS.DTOs.Caso;

namespace BCP.CROSS.SHAREPOINT
{
    public class Sharepoint : ISharepoint
    {
        private readonly SharepointSettings _sharepointSettings;
        private readonly IManagerSecrypt _managerSecrypt;
        public Sharepoint(IManagerSecrypt managerSecrypt, IOptions<SharepointSettings> sharepointSettings)
        {
            this._managerSecrypt = managerSecrypt;
            this._sharepointSettings = sharepointSettings.Value;
        }
        public HealthCheckResult Check()
        {
            DwsSoapClient.EndpointConfiguration endpoint = new DwsSoapClient.EndpointConfiguration();
            try
            {
                try
                {
                    string mensajeRespuesta = string.Empty;
                    if (this._sharepointSettings.Simulacion)
                    {
                        if (Directory.Exists(this._sharepointSettings.PathSimulacion))
                        {
                            return HealthCheckResult.Healthy($"Connect to Simulation Path: {this._sharepointSettings.PathSimulacion}");
                        }
                        return HealthCheckResult.Unhealthy($"Log Directory not exist: {this._sharepointSettings.PathSimulacion}");
                    }
                    else
                    {
                        BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                        basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                        basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                        var enpointAddress = new EndpointAddress(this._sharepointSettings.UriSharepoint + this._sharepointSettings.MethodDws);
                        DwsSoapClient sharepointClient = new DwsSoapClient(basicHttpBinding, new EndpointAddress(this._sharepointSettings.UriSharepoint + this._sharepointSettings.MethodDws));
                        string username = this._managerSecrypt.Desencriptar(this._sharepointSettings.Usuario);
                        string password = this._managerSecrypt.Desencriptar(this._sharepointSettings.Password);
                        sharepointClient.ClientCredentials.Windows.ClientCredential = new NetworkCredential(username, password, this._sharepointSettings.Dominio);
                        sharepointClient.FindDwsDocAsync(null).Wait();
                        sharepointClient.Close();
                        return HealthCheckResult.Healthy($"Connect to Sharepoint Service URL: {this._sharepointSettings.UriSharepoint}");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Could not connect to URL: {this._sharepointSettings.UriSharepoint}; Exception: {ex.Message.ToUpper()}");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Config parameter Url: {this._sharepointSettings.UriSharepoint}; Exception: {ex.Message.ToUpper()}");
            }
        }

        private async Task<bool> CreateFolderAsync(string nombreCarpeta)
        {
            if (this._sharepointSettings.Simulacion)
            {
                string path = Path.Combine(this._sharepointSettings.PathSimulacion, nombreCarpeta);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            else
            {
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                var enpointAddress = new EndpointAddress(this._sharepointSettings.UriSharepoint + this._sharepointSettings.MethodDws);
                DwsSoapClient sharepointClient = new DwsSoapClient(basicHttpBinding, new EndpointAddress(this._sharepointSettings.UriSharepoint + this._sharepointSettings.MethodDws));
                string username = this._managerSecrypt.Desencriptar(this._sharepointSettings.Usuario);
                string password = this._managerSecrypt.Desencriptar(this._sharepointSettings.Password);
                sharepointClient.ClientCredentials.Windows.ClientCredential = new NetworkCredential(username, password, this._sharepointSettings.Dominio);
                string rutaCarpeta = this._sharepointSettings.RutaCarpeta.Substring(1);
                string[] directorios = nombreCarpeta.Split('/');
                bool[] creados = new bool[directorios.Length];
                for (int i = 0; i < directorios.Length; i++)
                {
                    rutaCarpeta = rutaCarpeta + "/" + directorios[i];
                    var result=await sharepointClient.CreateFolderAsync(rutaCarpeta);
                    creados[i] = true;
                }
                sharepointClient.Close();
                return !creados.Contains(false);
            }
        }

        private async Task<bool> SubirArchivo(string nombreArchivo,string rutaCarpeta,byte[] archivo)
        {
            if (this._sharepointSettings.Simulacion)
            {
                rutaCarpeta = Path.Combine(new string[]{ GetRutaSharePoint(),rutaCarpeta,nombreArchivo});
                using (FileStream fs = File.Create(rutaCarpeta))
                {
                   fs.Write(archivo, 0, archivo.Length);
                }
                return true;
            }
            else
            {
                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
                var enpointAddress = new EndpointAddress(this._sharepointSettings.UriSharepoint + this._sharepointSettings.MethodDws);
                CopySoapClient sharepointClient = new CopySoapClient(basicHttpBinding, new EndpointAddress(this._sharepointSettings.UriSharepoint + this._sharepointSettings.MethodCopy));
                string username = this._managerSecrypt.Desencriptar(this._sharepointSettings.Usuario);
                string password = this._managerSecrypt.Desencriptar(this._sharepointSettings.Password);
                sharepointClient.ClientCredentials.Windows.ClientCredential = new NetworkCredential(username, password, this._sharepointSettings.Dominio);
                CopyIntoItemsRequest request = new CopyIntoItemsRequest
                {
                    DestinationUrls = new string[] { $"{this._sharepointSettings.UriSharepoint}{this._sharepointSettings.RutaCarpeta}/{rutaCarpeta}/{nombreArchivo}" },
                    SourceUrl = @"c:\" + nombreArchivo,
                    Stream = archivo,
                    Fields = new FieldInformation[] {
                    new FieldInformation
                    {
                        DisplayName=nombreArchivo,
                        Type=FieldType.Attachments,
                        Value=nombreArchivo
                    }
                }
                };
                var response = await sharepointClient.CopyIntoItemsAsync(request);
                return response.CopyIntoItemsResult == 0;
            }
        }

        public string GetRutaSharePoint()
        {
            if (!this._sharepointSettings.Simulacion)
            {
                return this._sharepointSettings.UriSharepoint + this._sharepointSettings.RutaCarpeta+"/";
            }
            else
            {
                return this._sharepointSettings.PathSimulacion.Replace('\\','/');
            }
        }

        private string GetCarpetaProceso(string proceso)
        {
            string response = string.Empty;
            switch (proceso.ToLower())
            {
                case "registro":
                    response = this._sharepointSettings.FolderRegistro;
                    break;
                case "solucion":
                    response =  this._sharepointSettings.FolderSolucion;
                    break;
                case "respuesta":
                    response =  this._sharepointSettings.FolderRespuesta;
                    break;
                case "movimiento":
                    response = this._sharepointSettings.FolderLogMovimiento;
                    break;
            }
            return response;
        }

        public async Task<bool> GuardarArchivosAdjuntosAsync(string nombreCarpeta, List<ArchivoSharepoint> archivosAdjuntos, string nombreProceso,bool base64)
        {
            string carpeta = nombreCarpeta.Replace(GetRutaSharePoint(), "").Trim() + "/" + GetCarpetaProceso(nombreProceso);
            var responseCreacionCarpeta = await CreateFolderAsync(carpeta);
            if (!responseCreacionCarpeta)
            {
                return false;
            }
            byte[] datos;
            bool[] archivosSubidos = new bool[archivosAdjuntos.Count];
            for (int i = 0; i < archivosAdjuntos.Count; i++)
            {
                if (base64)
                {
                    datos=System.Convert.FromBase64String(archivosAdjuntos[i].Archivo);
                }
                else
                {
                    datos=Encoding.ASCII.GetBytes(archivosAdjuntos[i].Archivo);
                }
                archivosSubidos[i] = await SubirArchivo(archivosAdjuntos[i].Nombre, carpeta,datos );
            }
            return !archivosSubidos.Contains(false);
        }
    }
}

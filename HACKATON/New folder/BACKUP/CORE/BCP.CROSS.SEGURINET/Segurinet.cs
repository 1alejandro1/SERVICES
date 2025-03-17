using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SrvSegurinet;
using BCP.CROSS.MODELS;
using BCP.CROSS.SECRYPT;
using BCP.CROSS.SEGURINET.Services;

namespace BCP.CROSS.SEGURINET
{
    public class Segurinet:ISegurinet
    {
        private readonly SegurinetSettings _segurinetSettings;
        private readonly IManagerSecrypt _managerSecrypt;
        private readonly ISegurinetServices _segurinetServices;
        public Segurinet(IManagerSecrypt managerSecrypt, IOptions<SegurinetSettings> segurinetSettings, ISegurinetServices segurinetServices)
        {
            this._managerSecrypt = managerSecrypt;
            this._segurinetSettings = segurinetSettings.Value;
            this._segurinetServices = segurinetServices;
        }

        public HealthCheckResult Check()
        {
            SegurinetClient.EndpointConfiguration endpoint = new SegurinetClient.EndpointConfiguration();
            SegurinetClient segurinetClient = new SegurinetClient(endpoint);
            try
            {
                try
                {                    
                    segurinetClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this._segurinetSettings.UriSegurinet);
                    segurinetClient.LoginAsync(string.Empty,string.Empty,string.Empty,null).Wait();
                    
                    var response = HealthCheckResult.Healthy($"Connect to Segurinet Service URL: {this._segurinetSettings.UriSegurinet}");
                    return response;
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Could not connect to URL: {this._segurinetSettings.UriSegurinet}; Exception: {ex.Message.ToUpper()}");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Config parameter Url: {this._segurinetSettings.UriSegurinet}; Exception: {ex.Message.ToUpper()}");
            }
            finally
            {
                segurinetClient.Close();
            }
        }

        private string[] cargarPoliticas()
        {
            string[]response=new string[this._segurinetSettings.politicas.Count];
            for(int i = 0; i < response.Length; i++)
            {
                response[i] = this._segurinetSettings.politicas[i].nombre;
            }
            return response;
        }
        private List<string> descargarRolesPoliticas(Dictionary<string,bool> politicas)
        {
            List<string> response = new List<string>();
            foreach (var politica in politicas)
            {
                if (politica.Value)
                {
                    response.Add(politica.Key);
                }
            }
            return response;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (this._segurinetSettings.Segurinet2)
            {
                return await this._segurinetServices.Login(request);
            }
            else
            {
                SegurinetClient.EndpointConfiguration endpoint = new SegurinetClient.EndpointConfiguration();
                SegurinetClient segurinetClient = new SegurinetClient(endpoint);
                segurinetClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this._segurinetSettings.UriSegurinet);
                string usuario = this._managerSecrypt.Desencriptar(request.usuario);
                string password = this._managerSecrypt.Desencriptar(request.password);
                string aplicativo = this._managerSecrypt.Desencriptar(this._segurinetSettings.AplicationName);
                var result = await segurinetClient.LoginAsync(aplicativo, usuario, password, cargarPoliticas());
                segurinetClient.Close();
                //state = result.CodigoErrorSegurinet;
                //message = result.ErrorSegurinet;
                LoginResponse response = new LoginResponse();
                if (result.CodigoErrorSegurinet.Equals("00"))
                {
                    response.Matricula = result.CodigoErrorSegurinet + result.Matricula;
                    response.Nombre = result.NombreUsuario;
                    response.Politicas = descargarRolesPoliticas(result.PoliticasUsuario);
                    response.Token = new UserToken();
                    response.Token.token = string.Join(",", result.Role);
                }
                else
                {
                    response.Matricula = result.CodigoErrorSegurinet;
                    response.Nombre = result.ErrorSegurinet;
                }
                return response;
            }
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            if (this._segurinetSettings.Segurinet2)
            {
                return await this._segurinetServices.ChangePassword(request);
            }
            else
            {
                SegurinetClient.EndpointConfiguration endpoint = new SegurinetClient.EndpointConfiguration();
                SegurinetClient segurinetClient = new SegurinetClient(endpoint);
                segurinetClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this._segurinetSettings.UriSegurinet);
                string usuario = this._managerSecrypt.Desencriptar(request.usuario);
                string password = this._managerSecrypt.Desencriptar(request.password);
                string newpassword = this._managerSecrypt.Desencriptar(request.newPassword);
                string aplicativo = this._managerSecrypt.Desencriptar(this._segurinetSettings.AplicationName);
                var result = await segurinetClient.ChangePasswordAsync(aplicativo, usuario, password, newpassword, new string[0]);
                segurinetClient.Close();
                //state = result.CodigoErrorSegurinet;
                //message = result.ErrorSegurinet;
                ChangePasswordResponse response = new ChangePasswordResponse();
                response.state = result.CodigoErrorSegurinet;
                response.message = result.ErrorSegurinet;
                return response;
            }
        }
    }
}

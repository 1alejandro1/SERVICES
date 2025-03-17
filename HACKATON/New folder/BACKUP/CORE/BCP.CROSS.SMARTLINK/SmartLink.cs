using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using srvSLK;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BCP.CROSS.SMARTLINK
{
    public class SmartLink : ISmartLink
    {
        private readonly SmartLinkSettings _slkSettings;
        public SmartLink(IOptions<SmartLinkSettings> slkSettings )
        {
            this._slkSettings=slkSettings.Value;
        }
        public HealthCheckResult Check()
        {        
            SrvSLKClient.EndpointConfiguration endpoint = new SrvSLKClient.EndpointConfiguration();
            SrvSLKClient slkClient = new SrvSLKClient(endpoint);
            try
            {
                try
                {
                    slkClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this._slkSettings.UriSmartLink);
                    TipoCambio_PARAMETROS parametros = new TipoCambio_PARAMETROS
                    {
                        CodigoIso = this._slkSettings.CodigoIso,
                        Fecha = DateTime.Now.ToString("yyyMMdd")
                    };
                    slkClient.ConsultaTCAsync(parametros).Wait();

                    var response = HealthCheckResult.Healthy($"Connect to Service SmartLink URL: {this._slkSettings.UriSmartLink}");
                    return response;
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Could not connect to URL: {this._slkSettings.UriSmartLink}; Exception: {ex.Message.ToUpper()}");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Config parameter Url: {this._slkSettings.UriSmartLink}; Exception: {ex.Message.ToUpper()}");
            }
            finally
            {
                slkClient.Close();
            }


        }

        public async Task<MODELS.TipoCambio> GetTipoCambioAsync()
        {
            SrvSLKClient.EndpointConfiguration endpoint = new SrvSLKClient.EndpointConfiguration();
            SrvSLKClient slkClient = new SrvSLKClient(endpoint);
            slkClient.Endpoint.Address = new System.ServiceModel.EndpointAddress(this._slkSettings.UriSmartLink);
            TipoCambio_PARAMETROS parametros = new TipoCambio_PARAMETROS
            {
                CodigoIso = this._slkSettings.CodigoIso,
                Fecha = DateTime.Now.ToString("yyyMMdd")
            };
            var response=await slkClient.ConsultaTCAsync(parametros);
            if (response.Error.Equals(string.Empty))
            {
                return new MODELS.TipoCambio
                {
                    Compra = response.TasaCompra,
                    Venta = response.TasaVenta,
                };
            }
            return null;
        }
    }
}

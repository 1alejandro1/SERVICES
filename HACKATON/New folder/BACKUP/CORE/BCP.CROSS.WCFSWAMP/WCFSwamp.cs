using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_Swamp;

namespace BCP.CROSS.WCFSWAMP
{
    public class WCFSwamp : IWCFSwamp
    {
        private readonly WCFSwampSettings _wcfSwampSettings;
        public WCFSwamp(IOptions<WCFSwampSettings> wCFSwampSettings)
        {
            this._wcfSwampSettings = wCFSwampSettings.Value;
        }

        public HealthCheckResult Check()
        {
            try
            {
                try
                {
                    BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                    EndpointAddress endPointAddress = new EndpointAddress(new Uri(this._wcfSwampSettings.UriWCFSwamp));
                    Service1Client swampClient = new  Service1Client(basicHttpBinding, endPointAddress);
                    var respuesta=swampClient.GetDataAsync(9).Result;
                    if (respuesta.Contains("9"))
                    {
                        return HealthCheckResult.Healthy($"Connect to WCF_SWAMP Service URL: {this._wcfSwampSettings.UriWCFSwamp}");
                    }
                    return HealthCheckResult.Unhealthy($"Could not connect to URL: {this._wcfSwampSettings.UriWCFSwamp}; Exception: Metodo no arrojo el resultado esperado");
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Could not connect to URL: {this._wcfSwampSettings.UriWCFSwamp}; Exception: {ex.Message.ToUpper()}");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Config parameter Url: {this._wcfSwampSettings.UriWCFSwamp}; Exception: {ex.Message.ToUpper()}");
            }
        }

        public async Task<DevolucionPRResponse> AbonosSwamp(AbonosSwamp request)
        {
            DevolucionPRResponse respuesta = new DevolucionPRResponse();

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            EndpointAddress endPointAddress = new EndpointAddress(new Uri(this._wcfSwampSettings.UriWCFSwamp));
            Service1Client swampClient = new Service1Client(basicHttpBinding, endPointAddress);
            List<Operacion> operaciones = new List<Operacion>();
            Operacion operacion = new Operacion();
            operacion.Sucursal = "204";
            operacion.Agencia = "201";
            operacion.Banca = request.Banca;
            operacion.Centro = request.Centro;
            operacion.Cerrar = true;
            operacion.CIC = request.CIC;
            operacion.Funcionario = request.Funcionario;
            operacion.Supervisor = request.Supervisor;
            operacion.NombreCompleto = request.NombreCompleto;
            operacion.IDC = request.ClienteIdc;
            operacion.NroCaso = request.NroCaso;
            operacion.Cuenta = request.NroCuenta;
            operacion.NIT = request.ClienteNit;
            operacion.Moneda = request.Moneda;
            operacion.Factura = request.DatosFactura;
            operacion.Origen = CanalOrigen.PR;
            operacion.Direccion = request.Direccion;
            operacion.DescripcionTramite = request.DescripcionTramite;
            operacion.Importe= request.Monto;
            operacion.Canal = new ServCanales
            {
                Id=request.ServiciosCanalesId,
                clave=request.CanalClave,
                Cuenta=request.CanalCuenta,
                Producto=request.CanalProducto
            };
            operacion.Glosa = request.Glosa;
            operacion.TipAccion = Accion.Devolucion;
            operacion.devolucion = new Devolucion
            {
                ID_DEVOLUCION = request.ID_DEVOLUCION,
                PRODUCTO_PR = request.ProductoId,
                SERVICIO_PR = request.ServicioId,
                DESCRIPCION = string.Empty
            };
            operaciones.Add(operacion);
            var response = await swampClient.AbonosSWAMPAsync(operaciones.ToArray());
            swampClient.Close();
            if (!response.Error)
            {

                respuesta.NroOperacion = response.operacion[0].NroOperacion;
                respuesta.Secuencia = response.operacion[0].Secuencia;
            }                
            else
            {
                respuesta.StatusCode = 426;
                respuesta.NroOperacion = "OCURRIO UN ERROR AL REALIZAR LA DEVOLUCION";
            }
            return respuesta;
        }

        public async Task<CobroPRResponse> CobroSwamp(CobroSwamp request)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            EndpointAddress endPointAddress = new EndpointAddress(new Uri(this._wcfSwampSettings.UriWCFSwamp));
            Service1Client swampClient = new Service1Client(basicHttpBinding, endPointAddress);
            List<Operacion> operaciones = new List<Operacion>();
            Operacion operacion = new Operacion();
            operacion.Sucursal = "204";
            operacion.Agencia = "201";
            operacion.Banca = request.Banca;
            operacion.Centro = request.Centro;
            operacion.Cerrar = true;
            operacion.CIC = request.CIC;
            operacion.Funcionario = request.Funcionario;
            operacion.Supervisor = request.Supervisor;
            operacion.NombreCompleto = request.NombreCompleto;
            operacion.IDC = request.ClienteIdc;
            operacion.NroCaso = request.NroCaso;
            operacion.Cuenta = request.NroCuenta;
            operacion.NIT = request.ClienteNit;
            operacion.Moneda = request.Moneda;
            operacion.Factura = request.DatosFactura;
            operacion.Origen = CanalOrigen.PR;
            operacion.Direccion = request.Direccion;
            operacion.DescripcionTramite = request.DescripcionTramite;
            operacion.Importe = request.Monto;
            operacion.Canal = new ServCanales
            {
                Id = request.ServiciosCanalesId,
                clave = request.CanalClave,
                Cuenta = request.CanalCuenta,
                Producto = request.CanalProducto,
            };
            operacion.Glosa = request.Glosa;
            operacion.TipAccion = Accion.Cobro;
            operacion.Correo = request.Correo;
            operacion.Telefono = request.Telefono;
            operacion.TipoFacturacion = request.FacturacionOnline?Facturacion.Online_SIN:Facturacion.Online_Transactor;
            operacion.ServFacturacion = new ServFacturacion
            {
                clave=request.FacturacionClave,
                Descripcion=request.FacturacionDescripcion
            };
            operacion.cobro = new Cobro
            {
                ID_COBRO= request.ID_COBRO,
                DESCRIPCION=request.DescripcionCobro,
                PRODUCTO_PR=request.ProductoId,
                SERV_PR=request.ServicioId,
                CIERRA_SWAMP=1
            };
            operacion.UsuDominio = request.Funcionario;
            operaciones.Add(operacion);
            var responseSwamp = await swampClient.CobroSWAMP2019Async(operaciones.ToArray(),"PR");
            swampClient.Close();
            var response = new CobroPRResponse();
            if (responseSwamp.exito)
            {

                response.CUF = responseSwamp.cuf;
                // Message=responseSwamp.mensajeCliente,
                response.NroOperacion = responseSwamp.operacion[0].NroOperacion;
                response.Secuencia = responseSwamp.operacion[0].Secuencia;
            }
            else
            {
                response.StatusCode = 426;
                response.NroOperacion = "OCURRIO UN ERROR AL TRATAR DE REALIZAR EL COBRO";
            }
            return response;
        }

        public async Task<string> GeneraFactura(string ClienteIdc,string ClienteTipo,string ClienteExtension, string cuf)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            EndpointAddress endPointAddress = new EndpointAddress(new Uri(this._wcfSwampSettings.UriWCFSwamp));
            Service1Client swampClient = new Service1Client(basicHttpBinding, endPointAddress);
            ConsultaFactura2019Request facturaRequest = new ConsultaFactura2019Request
            {
                cuf=cuf,
                aplicacion="PR",
                formatoFactura=2,
                idcNumero=ClienteIdc,
                idcTipo=ClienteTipo,
                idcExtension=ClienteExtension
            };
            var responseSwamp = await swampClient.ConsultaFactura2019Async(facturaRequest);
            swampClient.Close();
            if (responseSwamp.exito) 
            {
                return responseSwamp.pdf;
            }
            return null;
        }
    }
}

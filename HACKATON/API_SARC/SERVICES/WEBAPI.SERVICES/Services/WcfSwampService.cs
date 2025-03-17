using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.MODELS.WcfSwamp;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    public class WcfSwampService : IWcfSwampService
    {
        private readonly IWcfSwampRepository _wcfSwamp;
        private readonly ISharepointService _sharepointService;
        public WcfSwampService(IWcfSwampRepository wcfSwamp, ISharepointService sharepointService)
        {
            _wcfSwamp = wcfSwamp;
            _sharepointService = sharepointService;
        }
        public async Task<ServiceResponse<bool>> RealizarAbonoAsync(DevolucionPR request, string requestId)
        {
            var abonoResponse = await _wcfSwamp.RealizarAbonoAsync(request);
            if (abonoResponse.StatusCode==0)
            {
                #region SHAREPOINT
                string Auxiliar = "Detalle Transacciones Realizadas, Caso: " +request.NroCaso+"\nNro de Operacion: " + abonoResponse.NroOperacion + " Cuenta: " + request.NroCuenta + " Moneda: " + request.Moneda + " Importe: " + request.Monto.ToString(CultureInfo.InvariantCulture) + " Abono";
                List<ArchivoSharepoint> archivosLogs = new List<ArchivoSharepoint>();
                archivosLogs.Add(new ArchivoSharepoint
                {
                    Nombre = "DetalleOperacion" + abonoResponse.Secuencia.ToString() + ".txt",
                    Archivo = Auxiliar
                });
                var guardarArchivosAdjuntosRequest = new ArchivosAdjuntos
                {
                    nombreCarpeta = request.RutaSharePoint,
                    archivosAdjuntos = archivosLogs,
                    nombreProceso = "movimiento"
                };
                var guardarArchivosAdjuntosResponse = await _sharepointService.GuardarArchivosAdjuntosAsync(guardarArchivosAdjuntosRequest, requestId);
                if(guardarArchivosAdjuntosResponse.Meta.StatusCode == 200)
                {
                    abonoResponse.StatusCode = 200;
                    abonoResponse.NroOperacion = "ABONO REALIZADO CORRECTAMENTE";
                }
                else
                {
                    abonoResponse.StatusCode = 428;
                    abonoResponse.NroOperacion = "OCURRIO UN ERROR AL TRATAR DE GUARDAR EL REGISTRO DE LA DEVOLUCION";
                }
                #endregion
            }
            var response = new ServiceResponse<bool>
            {
                Data = abonoResponse.StatusCode==200,
                Meta = {
                    Msj = abonoResponse.NroOperacion,
                    ResponseId = requestId,
                    StatusCode = abonoResponse.StatusCode
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> RealizarCobroAsync(CobroPR request, string requestId)
        {
            var cobroResponse = await _wcfSwamp.RealizarCobroAsync(request);
            if (cobroResponse.StatusCode==0)
            {
                #region SHAREPOINT
                List<ArchivoSharepoint> archivosLogs = new List<ArchivoSharepoint>();
                var guardarArchivosAdjuntosRequest = new ArchivosAdjuntos
                {
                    nombreCarpeta = request.RutaSharePoint,
                    nombreProceso = "movimiento"
                };
                if (!request.FacturacionOnline && !string.IsNullOrEmpty(cobroResponse.Factura))
                {
                    archivosLogs.Add(new ArchivoSharepoint
                    {
                        Nombre = "Factura_" + cobroResponse.CUF + ".pdf",
                        Archivo = cobroResponse.Factura
                    });
                    guardarArchivosAdjuntosRequest.base64 = true;
                    guardarArchivosAdjuntosRequest.archivosAdjuntos = archivosLogs;
                    var guardarFactura = await _sharepointService.GuardarArchivosAdjuntosAsync(guardarArchivosAdjuntosRequest, requestId);
                    archivosLogs.Clear();
                    if(guardarFactura.Meta.StatusCode != 200)
                    {
                        cobroResponse.StatusCode = 428;
                        cobroResponse.NroOperacion = "OCURRIO UN ERROR AL TRATAR DE GUARDAR LA FACTURA. ";
                    }                                    
                }
                else
                {
                    //cobroResponse.StatusCode = 427;
                    //cobroResponse.NroOperacion += "NO SE OBTUVO LA FACTURA. ";
                }

                string Auxiliar = "Detalle Transacciones Realizadas, Caso: " + request.NroCaso + "\nNro de Operacion: " + cobroResponse.NroOperacion + " Cuenta: " + request.NroCuenta + " Moneda: " + request.Moneda + " Importe: " + request.Monto.ToString(CultureInfo.InvariantCulture) + " Cobro";
                archivosLogs.Add(new ArchivoSharepoint
                {
                    Nombre = "DetalleOperacion" + cobroResponse.Secuencia.ToString() + ".txt",
                    Archivo = Auxiliar
                });                    
                guardarArchivosAdjuntosRequest.archivosAdjuntos = archivosLogs;
                guardarArchivosAdjuntosRequest.base64 = false;
                var guardarArchivosAdjuntosResponse = await _sharepointService.GuardarArchivosAdjuntosAsync(guardarArchivosAdjuntosRequest, requestId);
                if(guardarArchivosAdjuntosResponse.Meta.StatusCode == 200)
                {
                    cobroResponse.StatusCode = 200;
                    cobroResponse.NroOperacion += "COBRO REALIZADO EXITOSAMENTE. ";
                }
                else
                {
                    cobroResponse.StatusCode = 428;
                    cobroResponse.NroOperacion += "OCURRIO UN ERROR AL TRATAR DE GUARDAR LOS LOGS. ";
                }
                #endregion
            }
            var response = new ServiceResponse<bool>
            {
                Data = cobroResponse.StatusCode==200,
                Meta = {
                    Msj = cobroResponse.NroOperacion,
                    ResponseId = requestId,
                    StatusCode = cobroResponse.StatusCode
                }
            };
            return response;
        }
    }
}

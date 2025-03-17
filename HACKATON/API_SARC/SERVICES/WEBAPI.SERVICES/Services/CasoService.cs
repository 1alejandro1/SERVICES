using AutoMapper;
using BCP.CROSS.COMMON;
using BCP.CROSS.COMMON.Enums;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Asignacion;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.MODELS.DTOs.PushNotification;
using BCP.CROSS.MODELS.Sarc;
using BCP.CROSS.MODELS.WcfSwamp;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES
{
    public class CasoService : ICasoService
    {
        private readonly ICasoRepository _casoRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ISarcService _parametrosSarc;
        private readonly IWcfSwampService _wcfSwampService;
        private readonly ISharepointService _sharepointService;
        private readonly IServiciosSwampRepository _serviciosSwampRepository;
        private readonly IRepExtService _repExtService;        

        public CasoService(ICasoRepository casoRepository, IMapper mapper, INotificationService notificationService, ISarcService parammetrosSarc, IWcfSwampService wcfSwampService, ISharepointService sharepointService, IServiciosSwampRepository serviciosSwampRepository, IRepExtService repExtService)
        {
            _casoRepository = casoRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _parametrosSarc = parammetrosSarc;
            _wcfSwampService = wcfSwampService;
            _sharepointService = sharepointService;  
            _serviciosSwampRepository = serviciosSwampRepository;
            _repExtService = repExtService;
        }

        public async Task<ServiceResponse<InsertCasoResponse>> AddCasoAsync(CreateCasoDTO casoRequest, string requestId)
        {
            var caso = _mapper.Map<Caso>(casoRequest);
            var casoResponse = await _casoRepository.InsertCaso(caso).ConfigureAwait(false);

            var response = new ServiceResponse<InsertCasoResponse>
            {
                Data = casoResponse,
                Meta = {
                    Msj = casoResponse != null ? "Caso Registrado" : "Caso no registrado",
                    ResponseId = requestId,
                    StatusCode = casoResponse != null ? 200 : 204
                }
            };

            if (casoResponse is not null && casoRequest.ViaEnvioCodigo != (int)NotifcationChannels.GeneradoEnLinea)
            {
                var client = new List<Client>{
                        new Client
                        {
                            Cic = string.Empty,
                            Idc = casoRequest.ClienteIdc+casoRequest.ClienteIdcTipo+casoRequest.ClienteIdcExtension,
                            Email = new string[]{ casoRequest.EmailRespuesta },
                            PhoneNumber = casoRequest.SmsRespuesta
                        }
                    };
                var responseNotification = await _notificationService.SendNotificationAsync(client, casoRequest.ViaEnvioCodigo, requestId, casoResponse);
                response.Meta.Msj += $", {responseNotification.Data}, code response notification: {responseNotification.Meta.StatusCode}, {responseNotification.Meta.Msj}";
            }

            return response;
        }
        public async Task<ServiceResponse<InsertCasoResponseV2>> AddCasoAsyncV2(CreateCasoDTOV2 casoRequest, string requestId,bool debAbo=false)
        {
            //var casoExtra = System.Text.Json.JsonSerializer.Deserialize<CreateCasoDTO>(System.Text.Json.JsonSerializer.Serialize(casoRequest));
            var caso = _mapper.Map<Caso>(casoRequest); 
            caso.FuncinarioAtencion = caso.FuncionarioRegistro;
            caso.SwComunicacionEnOficina = caso.ViaEnvioRespuesta.Equals("O") ? "1" : "0";
            caso.SwComunicacionEmail = caso.ViaEnvioRespuesta.Equals("E") ? "1" : "0";
            caso.SwComunicacionWhatsapp = caso.ViaEnvioRespuesta.Equals("W") ? "1" : "0";
            if (debAbo)
            {
                caso.ImporteDevolucion = caso.Monto;
                caso.MonedaDevolucion = caso.Moneda;
            }
            var casoResponse = await _casoRepository.InsertCaso(caso);
            var response = new ServiceResponse<InsertCasoResponseV2>
            {
                Data = casoResponse == null ? null : new InsertCasoResponseV2
                {
                    NroCarta = casoResponse.NroCarta,
                    RutaSharePoint = casoResponse.RutaSharePoint
                },
                Meta = {
                    Msj = casoResponse != null ? "Caso Registrado" : "Caso no registrado",
                    ResponseId = requestId,
                    StatusCode = casoResponse != null ? 200 : 204
                }
            };
            if (casoResponse is not null) { 
                if (casoRequest.ViaEnvioCodigo != (int)NotifcationChannels.GeneradoEnLinea)
                {
                    var client = new List<Client>{
                            new Client
                            {
                                Cic = string.Empty,
                                Idc = casoRequest.ClienteIdc+casoRequest.ClienteIdcTipo+casoRequest.ClienteIdcExtension,
                                Email = new string[]{ casoRequest.EmailRespuesta },
                                PhoneNumber = casoRequest.SmsRespuesta
                            }
                        };
                    var responseNotification = await _notificationService.SendNotificationAsync(client, casoRequest.ViaEnvioCodigo, requestId, casoResponse);
                    response.Meta.Msj += $", {responseNotification.Data}, code response notification: {responseNotification.Meta.StatusCode}, {responseNotification.Meta.Msj}";
                }
                #region LOGS
                var registroLogsResponse = await _casoRepository.InsertLogsCasoAsync(response.Data.NroCarta, casoRequest.FuncionarioRegistro);
                if (!registroLogsResponse)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REGISTRAR LOS LOGS DE SARC";
                    response.Meta.StatusCode = 503;
                    return response;
                }
                #endregion
                #region PDF
                DatosReporteRequest pdfRequest = new DatosReporteRequest
                {
                    NombreUsuario = casoRequest.NombreUsuario,
                    NroCarta = response.Data.NroCarta,
                    TipoReporte = casoRequest.TipoRegistro
                };
                var pdfResponse = await GetReporteRegistroAsync(pdfRequest, requestId);
                if (pdfResponse.Meta.StatusCode != 200)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE CREAR EL ARCHIVO PDF";
                    response.Meta.StatusCode = 601;
                }
                else
                {
                    response.Data.PDF = pdfResponse.Data;
                }
                #endregion
                if (casoRequest.ArchivosAdjuntos.Count > 0)
                {
                    #region SHAREPOINT
                    var guardarArchivosAdjuntosRequest = new ArchivosAdjuntos
                    {
                        nombreCarpeta = response.Data.RutaSharePoint,
                        archivosAdjuntos = casoRequest.ArchivosAdjuntos,
                        nombreProceso = "registro",
                        base64 = true
                    };
                    var guardarArchivosAdjuntosResponse = await _sharepointService.GuardarArchivosAdjuntosAsync(guardarArchivosAdjuntosRequest, requestId);
                    if (guardarArchivosAdjuntosResponse.Meta.StatusCode != 200)
                    {
                        response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE GUARDAR LOS ARCHIVOS ADJUNTOS";
                        response.Meta.StatusCode = 502;
                        return response;
                    }
                    #endregion
                }
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateSolucionCasoAsync(UpdateCasoSolucionDTO casoRequest, string requestId,bool escribirLogs=true)
        {
            var responseCaso = await _casoRepository.UpdateSolucionCasoAsync(casoRequest).ConfigureAwait(false);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Caso Actualizado" : "Caso no Actualizado, asegurese de que exista el caso",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };
            if (escribirLogs)
            {
                #region LOGS
                var registroLogsResponse = await _casoRepository.InsertLogsCasoAsync(casoRequest.NroCarta, casoRequest.FuncionarioModificacion);
                if (!registroLogsResponse)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REGISTRAR LOS LOGS DE SARC";
                    response.Meta.StatusCode = 503;
                    return response;
                }
                #endregion
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateAsignacionCasoExpressAsync(UpdateAsignacionCasoDTO casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.UpdateAsignacionCasoExpressAsync(casoRequest).ConfigureAwait(false);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Caso Asignado" : "Caso no Asignado, asegurese de que exista el caso",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };

            return response;
        }

        public async Task<ServiceResponse<CasoDTO>> GetCasoAsync(GetCasoDTO casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetCasoAsync(casoRequest.NroCaso);
            var response = new ServiceResponse<CasoDTO> { 
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> CerrarCasoAsync(CerrarCasoRequest casoRequest, string requestId,bool escribirLogs=true)
        {
            var responseCaso = await _casoRepository.CerrarCasoAsync(casoRequest);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Caso Cerrado" : "Caso no cerrado, verifique e inetente nuevamente",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };
            if (escribirLogs)
            {
                #region LOGS
                var registroLogsResponse = await _casoRepository.InsertLogsCasoAsync(casoRequest.NroCarta, casoRequest.FuncionarioSupervisor);
                if (!registroLogsResponse)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REGISTRAR LOS LOGS DE SARC";
                    response.Meta.StatusCode = 503;
                    return response;
                }
                #endregion
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateDevolucionCobroAsync(UpdateDevolucionCobroRequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.UpdateDevolucionCobroAsync(casoRequest.NroCarta,casoRequest.NroCuentaPR,casoRequest.IdServiciosCanales,casoRequest.ParametroCentroPR,casoRequest.Monto,casoRequest.Moneda,casoRequest.TipoFacturacion,casoRequest.IndDevolucionCobro);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Registro Modificados" : "No se pudo guardar el registro de cobro/devolucion, verifique e inetente nuevamente",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateSolucionCasoOrigenAsync(UpdateSolucionCasoDTOOrigenRequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.UpdateSolucionCasoOrigenAsync(casoRequest);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Registro Modificados" : "No se pudo cambiar el registro origen, verifique e inetente nuevamente",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };

            return response;
        }

        public async Task<ServiceResponse<string>> GetReporteRegistroAsync(DatosReporteRequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetReporteRegistroAsync(casoRequest.NroCarta,casoRequest.NombreUsuario,casoRequest.TipoReporte);

            var response = new ServiceResponse<string>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateCasoGrabarOrigenAsync(UpdateOrigenCasoDTORequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.UpdateCasoGrabarOrigenAsync(casoRequest);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Registro Modificados" : "No se pudo guardar el registro de origen, verifique e inetente nuevamente",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };

            return response;
        }

        public async Task<ServiceResponse<CasoDTOCliente>> GetCasoAllAsync(GetCasoDTO casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetCasoAllAsync(casoRequest.NroCaso);
            var response = new ServiceResponse<CasoDTOCliente>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<CasoDTOCompleto>> GetCasoCompletoAsync(GetCasoDTO casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetCasoCompletoAsync(casoRequest.NroCaso);
            var response = new ServiceResponse<CasoDTOCompleto>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<GetCasoDTOByAnalistaResponse>>> GetCasoByAnalistaAsync(GetCasoDTOByAnalistaRequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetCasoByAnalistaAsync(casoRequest.Usuario,casoRequest.Estado);
            var response = new ServiceResponse<List<GetCasoDTOByAnalistaResponse>>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<GetCasoDTOByEstadoResponse>>> GetCasoByEstadoAsync(GetCasoDTOByEstadoRequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetCasoByEstadoAsync(casoRequest.Estado);
            var response = new ServiceResponse<List<GetCasoDTOByEstadoResponse>>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<GetCasoResponse>>> GetListCasoAsync(GetCasoRequest casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.GetListCasoAsync(casoRequest);
            var response = new ServiceResponse<List<GetCasoResponse>>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso != null ? "Success" : "Caso no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseCaso != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateSolucionCasoDateAsync(UpdateCasoSolucionDateDTO casoRequest, string requestId)
        {
            var responseCaso = await _casoRepository.UpdateSolucionCasoDateAsync(casoRequest);

            var response = new ServiceResponse<bool>
            {
                Data = responseCaso,
                Meta = {
                    Msj = responseCaso ? "Registro Modificado" : "No se pudo guardar la modificacion del caso, verifique e inetente nuevamente",
                    ResponseId = requestId,
                    StatusCode = responseCaso ? 200 : 400
                }
            };

            return response;
        }
        #region FLUJO CASO EXPRESS
        public async Task<ServiceResponse<InsertCasoResponseV2>> CierraCasoExpress(FlujoCasoExpressDTORequest casoRequest, string requestId)
        {
            #region validar
            if (casoRequest.esCobro ||casoRequest.esDevolucion)
            {
                if (casoRequest.Caso.Monto <= 0M)
                {
                    return new ServiceResponse<InsertCasoResponseV2>
                    {
                        Data = null,
                        Meta = {
                        ResponseId= requestId,
                        Msj="MONTO DEBE SER MAYOR A CERO",
                        StatusCode=291
                    }
                    };
                }
                var validarMonto = await _serviciosSwampRepository.ValidarImporte(casoRequest.Caso.Monto, casoRequest.Caso.ProductoId, casoRequest.Caso.ServicioId, casoRequest.Caso.Moneda, casoRequest.esCobro);
                if (validarMonto!=0M)
                {
                    return new ServiceResponse<InsertCasoResponseV2>
                    {
                        Data = null,
                        Meta = {
                        ResponseId= requestId,
                        Msj=validarMonto<0M?validarMonto==-1?"SERVICIO NO APTO PARA REALIZAR COBRO/DEVOLUCIONES":"OCURRIO UN ERROR AL MOMENTO DE VALIDAR EL IMPORTE":"MONTO NO DEBE SUPERAR LOS "+validarMonto.ToString(CultureInfo.InvariantCulture),
                        StatusCode=292
                    }
                    };
                }
                var validarCuentaRequest = new BCP.CROSS.MODELS.RepExt.ValidaCuentaRequest
                {
                    IDC = casoRequest.Caso.ClienteIdc,
                    IdcTipo = casoRequest.Caso.ClienteIdcTipo,
                    IdcExtenion = casoRequest.Caso.ClienteIdcExtension,
                    NroCuenta = casoRequest.Caso.NroCuenta.Replace("-", "")
                };
                var validarcuenta = await _repExtService.ValidarCuentaAsync(validarCuentaRequest, requestId);
                if (validarcuenta.Meta.StatusCode!=200)
                {
                    return new ServiceResponse<InsertCasoResponseV2>
                    {
                        Data = null,
                        Meta = {
                            ResponseId= requestId,
                            Msj="CUENTA NO ASOCIADA AL CLIENTE",
                            StatusCode=293
                        }
                    };
                }
                if (casoRequest.esDevolucion)
                {
                    if (casoRequest.Devolucion.ParametroCentro.Length != 3) 
                    {
                        return new ServiceResponse<InsertCasoResponseV2>
                        {
                            Data = null,
                            Meta = {
                                ResponseId= requestId,
                                Msj="CENTRO DE COSTO INVALIDO",
                                StatusCode=294
                            }
                        };
                    }
                    var limiteDevolucion = await _parametrosSarc.validarLimitesCuenta(casoRequest.Caso.NroCuenta, casoRequest.Caso.FuncionarioRegistro);
                    if (limiteDevolucion != 0)
                    {
                         return new ServiceResponse<InsertCasoResponseV2>
                        {
                            Data = null,
                            Meta = {
                                ResponseId= requestId,
                                Msj=limiteDevolucion==-99?"OCURRIO UN ERROR AL VALIDAR EL LIMITE DE DEVOLUCIONES":limiteDevolucion<0?"CUENTA ALCANZO LA MAXIMA CANTIDAD DE DEVOLUCIONES":"ANALISTA ALCANZO LA MAXIMA CANTIDAD DE DEVOLUCIONES",
                                StatusCode=295
                            }
                        };
                    } 
                }
            }
            #endregion

            #region CREAR CASO             
            ServiceResponse<InsertCasoResponseV2> response;
            if (casoRequest.StatusCode < 422)
            {
                response = await AddCasoAsyncV2(casoRequest.Caso, requestId, true);
                if (response.Meta.StatusCode != 200)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REGISTRAR EL CASO";
                    response.Meta.StatusCode = 421;
                    return response;
                }
            }
            else
            {
                response = new ServiceResponse<InsertCasoResponseV2>
                {
                    Data = new InsertCasoResponseV2 
                    {
                        NroCarta=casoRequest.NroCaso,
                        RutaSharePoint=casoRequest.RutaSharepoint
                    },
                    Meta = {
                        ResponseId= requestId,
                        Msj="CASO FINALIZADO EXITOSAMENTE",
                        StatusCode=200
                    }
                };  
            }
            #endregion

            #region ASIGNAR CASO
            if (casoRequest.StatusCode < 423)
            {
                var asignacionRequest = new UpdateAsignacionCasoDTO
                {
                    FuncionarioAtension = casoRequest.Caso.FuncionarioRegistro,
                    Estado = "A",
                    FuncionarioModificacion = casoRequest.Caso.FuncionarioRegistro,
                    NroCarta = response.Data.NroCarta,
                    TiempoResolucion = 1,
                    Complejidad = "000028"
                };
                var asignacionResponse = await UpdateAsignacionCasoExpressAsync(asignacionRequest, requestId);
                if (asignacionResponse.Meta.StatusCode != 200)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REALIZAR LA ASIGNACION DE CASO";
                    response.Meta.StatusCode = 422;
                    return response;
                }
            }
            #endregion

            #region SOLUCION CASO
            if (casoRequest.StatusCode < 424)
            {
                var solucionCasoRequest = new UpdateCasoSolucionDTO
                {
                    FuncionarioModificacion = casoRequest.Caso.FuncionarioRegistro,
                    NroCarta = response.Data.NroCarta,
                    DescripcionSolucion = casoRequest.Caso.DescripcionSolucion,
                    TipoCarta = casoRequest.Caso.TipoCarta,
                    ImporteDevolucion = casoRequest.Caso.Monto,
                    MonedaDevolucion = casoRequest.Caso.Moneda,
                    TipoSolucion = casoRequest.Caso.TipoSolucion,
                };
                var solucionCasoResponse = await UpdateSolucionCasoAsync(solucionCasoRequest, requestId);
                if (solucionCasoResponse.Meta.StatusCode != 200)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REALIZAR LA ASIGNACION DE CASO";
                    response.Meta.StatusCode = 423;
                    return response;
                }
            }
            #endregion

            #region Actualiza Origen Solucion(no area origen)
            if (casoRequest.StatusCode < 425)
            {
                var origenSolucionRequest = new UpdateSolucionCasoDTOOrigenRequest();
                string[] parametrosOrigen = { "TIEMPO", "RIESGO", "PROCESO" };
                int[] indiceOrigen = { 0, 2, 0 };
                int[] parametrosRescatado = new int[indiceOrigen.Length];
                ServiceResponse<List<ParametrosResponse>> lexicos = new ServiceResponse<List<ParametrosResponse>>();
                for (int i = 0; i < parametrosOrigen.Length; i++)
                {
                    lexicos = await _parametrosSarc.GetParametrosSarcAsync(parametrosOrigen[i], requestId);
                    if (lexicos.Meta.StatusCode == 200)
                    {
                        parametrosRescatado[i] = lexicos.Data[indiceOrigen[i]].IdParametro;
                    }
                }
                origenSolucionRequest.NroCarta = response.Data.NroCarta;
                origenSolucionRequest.Tiempo = parametrosRescatado[0];
                origenSolucionRequest.Riesgo = parametrosRescatado[1];
                origenSolucionRequest.Proceso = parametrosRescatado[2];
                var responseEtapa4 = await UpdateSolucionCasoOrigenAsync(origenSolucionRequest, requestId);
                if (responseEtapa4.Meta.StatusCode != 200)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REALIZAR EL REGISTRO DEL ORIGEN SOLUCION";
                    response.Meta.StatusCode = 424;
                    return response;
                }
            }
            #endregion

            if ((casoRequest.esCobro || casoRequest.esDevolucion) && casoRequest.StatusCode<426)
            {
                #region DEVOLUCION COBRO
                int serviciosCanalesID = casoRequest.esCobro ? casoRequest.Cobro.ServiciosCanalesId : casoRequest.Devolucion.ServiciosCanalesId;
                string facturacion=casoRequest.esCobro?casoRequest.Cobro.TipoFacturacion:string.Empty;
                string parametroCentro = casoRequest.esCobro ? string.Empty : casoRequest.Devolucion.ParametroCentro;
                var responseRegistroDebAbo = await _casoRepository.UpdateDevolucionCobroAsync(response.Data.NroCarta,casoRequest.Caso.NroCuenta, serviciosCanalesID, parametroCentro,casoRequest.Caso.Monto, casoRequest.Caso.Moneda, facturacion, casoRequest.esCobro?1:2);
                if (!responseRegistroDebAbo)
                {
                    response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE REALIZAR EL REGISTRO DEL COBRO/DEVOLUCION";
                    response.Meta.StatusCode = 425;
                    return response;
                }
                if (casoRequest.esCobro)
                {
                    var cobroRequest = System.Text.Json.JsonSerializer.Deserialize<CobroPR>(System.Text.Json.JsonSerializer.Serialize(casoRequest.Caso));
                    cobroRequest.FuncionarioAtencion = casoRequest.Caso.FuncionarioRegistro;
                    cobroRequest.Supervisor = casoRequest.Caso.FuncionarioRegistro;
                    cobroRequest.NroCaso = response.Data.NroCarta;
                    cobroRequest.DescripcionServicio = casoRequest.Cobro.DescripcionServicio;
                    cobroRequest.ServiciosCanalesId = casoRequest.Cobro.ServiciosCanalesId;
                    cobroRequest.FacturacionOnline = casoRequest.Cobro.TipoFacturacion.Equals("O");
                    cobroRequest.RutaSharePoint = response.Data.RutaSharePoint;
                    var cobroResponse = await _wcfSwampService.RealizarCobroAsync(cobroRequest, requestId);
                    if (cobroResponse.Meta.StatusCode != 200)
                    {
                        response.Meta.Msj = cobroResponse.Meta.Msj;
                        response.Meta.StatusCode = cobroResponse.Meta.StatusCode;
                        return response;
                    }
                }
                else
                {
                    #region ABONO
                    var devolucionRequest = System.Text.Json.JsonSerializer.Deserialize<DevolucionPR>(System.Text.Json.JsonSerializer.Serialize(casoRequest.Caso));
                    devolucionRequest.FuncionarioAtencion = casoRequest.Caso.FuncionarioRegistro;
                    devolucionRequest.Supervisor = casoRequest.Caso.FuncionarioRegistro;
                    devolucionRequest.NroCaso = response.Data.NroCarta;
                    devolucionRequest.DescripcionServicio = casoRequest.Devolucion.DescripcionServicio;
                    devolucionRequest.ServiciosCanalesId = casoRequest.Devolucion.ServiciosCanalesId;
                    devolucionRequest.ParametroCentro = casoRequest.Devolucion.ParametroCentro;
                    devolucionRequest.RutaSharePoint = response.Data.RutaSharePoint;
                    var devolucionResponse = await _wcfSwampService.RealizarAbonoAsync(devolucionRequest, requestId);
                    if (devolucionResponse.Meta.StatusCode!=200)
                    {
                        response.Meta.Msj = devolucionResponse.Meta.Msj;
                        response.Meta.StatusCode = devolucionResponse.Meta.StatusCode;
                        return response;
                    }
                    #endregion
                }
                #endregion
            }
            #region CIERRE DE CASO
            var requestCierre = new CerrarCasoRequest {
                NroCarta = response.Data.NroCarta,
                Producto = casoRequest.Caso.ProductoId,
                Servicio = casoRequest.Caso.ServicioId,
                FuncionarioSupervisor=casoRequest.Caso.FuncionarioRegistro
                };
            var reponseCierreCaso = await CerrarCasoAsync(requestCierre,requestId);
            if (reponseCierreCaso.Meta.StatusCode!=200)
            {
                response.Meta.Msj = "OCURRIO UN ERROR AL TRATAR DE CERRAR EL CASO";
                response.Meta.StatusCode = 430;
                return response;
            }
            #endregion
            return response;
        }
        #endregion
        
       

        public async Task<ServiceResponse<List<CasoResumen>>> GetCasoResumenByEstadoAsync(EstadoCasoRequest casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.GetCasoResumenByEstadoAsync(casoRequest.Estado);
            var response = new ServiceResponse<List<CasoResumen>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos != null ? "Success" : "Registros no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos != null ? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateCasoEstadoComplejidadAsync(CasoComplejidad casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.UpdateCasoEstadoComplejidadAsync(casoRequest);
            var response = new ServiceResponse<bool>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos ? "Success" : "Registros no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateCasoRechazarAsync(UpdateCasoRechazarDTO casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.UpdateCasoRechazarAsync(casoRequest);
            var response = new ServiceResponse<bool>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos ? "Success" : "Caso no Rechazado",
                    ResponseId = requestId,
                    StatusCode = responseCasos? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateCasoReaperturaAsync(UpdateCasoDTOEstado casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.UpdateCasoReaperturaAsync(casoRequest);
            var response = new ServiceResponse<bool>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos ? "Success" : "Caso no Reasignado",
                    ResponseId = requestId,
                    StatusCode = responseCasos? 200 : 404
                }
            };

            return response;
        }

        

        public async Task<ServiceResponse<List<CasoLog>>> GetCasoLogsAllAsync(GetCasoDTO casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.GetCasoLogsAllAsync(casoRequest.NroCaso);
            var response = new ServiceResponse<List<CasoLog>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Logs de Casos no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<CasoDTOBaseAtencion>>> GetCasoAtencionByEstadoAsync(EstadoCasoRequest casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.GetCasoAtencionByEstadoAsync(casoRequest.Estado);
            var response = new ServiceResponse<List<CasoDTOBaseAtencion>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Casos no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<CasoDTOBaseAtencion>>> GetCasoAtencionByEstadoUsuarioAsync(EstadoCasoFuncionarioRequest casoRequest, string requestId)
        {
            var responseCasos = await _casoRepository.GetCasoAtencionByEstadoUsuarioAsync(casoRequest.Estado,casoRequest.Funcionario);
            var response = new ServiceResponse<List<CasoDTOBaseAtencion>>
            {
                Data = responseCasos,
                Meta = {
                    Msj = responseCasos!=null ? "Success" : "Casos no encontrados",
                    ResponseId = requestId,
                    StatusCode = responseCasos!=null? 200 : 404
                }
            };

            return response;
        }
        public async Task<ServiceResponse<List<Reclamos>>> GetReclamoFuncionarioTipoAsync(AlertaAnalistaRequest request, string responseId)
        {
            var alerta = await _casoRepository.GetReclamoFuncionarioTipoAsync(request.Funcionario, request.Estado);
            var response = new ServiceResponse<List<Reclamos>>()
            {
                Data = alerta,
                Meta =
                {
                    Msj=alerta!= null ? "Success" : "El funcionario no presenta casos en el estado indicado",
                    ResponseId=responseId,
                    StatusCode=alerta!= null?200:404
                }
            };
            return response;
        }
        public async Task<ServiceResponse<List<string>>> GetReclamoFuncionarioTipoAlertaAsync(MatriculaFuncionario request, string responseId)
        {
            var alerta = await _casoRepository.GetReclamoFuncionarioTipoAlertaAsync(request.Funcionario, "A");
            var response = new ServiceResponse<List<string>>()
            {
                Data = alerta,
                Meta =
                {
                    Msj=alerta!= null ? "Success" : "El funcionario no presenta casos pendientes",
                    ResponseId=responseId,
                    StatusCode=alerta!= null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateCasoRechazoAnalistaAsync(UpdateCasoRechazoAnalistaDTO request, string requestId)
        {
            var caso = await _casoRepository.UpdateCasoRechazoAnalistaAsync(request);
            var response = new ServiceResponse<bool>
            {
                Data = caso,
                Meta = {
                    Msj = caso ? "Success" : "Caso no se rechazado",
                    ResponseId = requestId,
                    StatusCode = caso? 200 : 404
                }
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateViaEnvioAsync(UpdateCasoDTOViaEnvio request, string requestId)
        {
            var casoResponse = await _casoRepository.UpdateViaEnvioAsync(request).ConfigureAwait(false);

            var response = new ServiceResponse<bool>
            {
                Data = casoResponse,
                Meta = {
                    Msj = casoResponse? "Caso Actualizado" : "Caso no Actualizado",
                    ResponseId = requestId,
                    StatusCode = casoResponse? 200 : 204
                }
            };
            return response;
        }
    }
}

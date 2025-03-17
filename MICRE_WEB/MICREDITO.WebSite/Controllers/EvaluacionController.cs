using BCP.CROSS.LOGGER;
using MICREDITO.BusinessLogic;
using MICREDITO.Common;
using MICREDITO.Model;
using MICREDITO.Model.Common;
using MICREDITO.Model.Evaluacion;
using MICREDITO.Model.Inventario;
using MICREDITO.Model.Request;
using MICREDITO.Model.Response;
using MICREDITO.Model.Service;
using MICREDITO.Model.Service.RefinanciamientoCompraModel;
using MICREDITO.Model.Solicitud;
using MICREDITO.WebSite.Controllers.Base;
using MICREDITO.WebSite.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebGrease;

namespace MICREDITO.WebSite.Controllers
{
    [CustomAuthorization("Login", "Index")]
    public class EvaluacionController : BaseController
    {
        // GET: Evaluacion
        //log instancia de errores
        MICREDITO.Common.ManejoErrores log = new MICREDITO.Common.ManejoErrores(Parametros.NombreAplicacion());
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileBase)
        {
            if (fileBase != null)
            {
                string path = Path.Combine(Path.GetTempPath(), Path.GetFileName(fileBase.FileName));
                fileBase.SaveAs(path);
                byte[] file = System.IO.File.ReadAllBytes(path);
                EvaluacionService service = new EvaluacionService();
                EvaluacionCuantitativa model = service.Load(file);
                model.garantia = model.garante.cabecera != null;
                model.nuevo = true;
                model.json = JsonConvert.SerializeObject(model);
                return View("View", model);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetEvaluacion(string nroEvaluacion)
        {
            string FLAG_SCOCRE_MICROCREDITO = ConfigurationManager.AppSettings["FLAG_SCORE_MICROCREDITO"].ToString();

            log.RegistroLogInformacion($"[GetEvaluacion] nroEvaluacion:{nroEvaluacion}");
            EvaluacionService service = new EvaluacionService();
            EvaluacionCuantitativa model = service.GetEvaluacion(nroEvaluacion);
            model.nroEvaluacion = nroEvaluacion;
            model.nuevo = false;
            SolicitudService solicitudService = new SolicitudService();
            RefinanciamientoCompraService refinanciamientoCompraService = new RefinanciamientoCompraService();

            #region SCORE MICROCREDITO 
            model.FLAG_SCORE_MICROCREDITO = FLAG_SCOCRE_MICROCREDITO;
            if (FLAG_SCOCRE_MICROCREDITO == "S")
            {
                //agrego consulta inventario
                model.inventario = solicitudService.ObtenerInventario(Convert.ToString(model.codSolicitud), nroEvaluacion);
                model.desembolso = new DatosDesembolso();
                model.desembolso.numSolicitud = nroEvaluacion;
                model.desembolso.codSolicitud = Convert.ToString(model.codSolicitud);
                model.desembolso.matricula = user.matricula.ToString();
                model.desembolso.contratoCronograma.finalidad = model.inventario.dataInventario.inventario10;
                if (model.codSolicitud != 0)
                {
                    //consulta de excepciones
                    ExceptionByIdcRequest exceptionByIdcRequest = new ExceptionByIdcRequest
                    {
                        IdcNumero = solicitudService.IdcTG(model.resumen.nroDocumento.PadLeft(8, '0')),
                        IdcTipo ="Q",
                        IdcExtension = model.resumen.extensionDocumento,
                        IdcComplemento = solicitudService.ComplementoTG(model.resumen.nroDocumento.PadLeft(8, '0'))
                    };
                    model.excepcionResponseIdc = new ExcepcionResponseByIdc();
                    model.excepcionResponseIdc =  await solicitudService.GetExceptionByIdc(exceptionByIdcRequest);
                    model.excepcionResponseIdc.matricula = user.matricula.ToString();
                    model.excepcionResponseIdc.codigoSolicitud = Convert.ToString(model.codSolicitud);
                    model.desembolso.ConsultaSegurosResponse = solicitudService.ConsultaSegurosMICROCREDITO(Convert.ToString(model.codSolicitud), nroEvaluacion);
                }
                model.desembolso.FlagFogagre =await solicitudService.ValidacionFogagre(model.resumen.tipoCredito, model.resumen.ciiuDestinoCredito);
                //fin consulta inventario
            }
            #endregion

            //#region SEGURO DESGRAVAMENT TRADICIONAL/PLUS
            //model.LstSeguroDesgravamen = solicitudService.ObtenerTipoSeguroPLUS();
            //#endregion



            string mensajeTitular = string.Empty;
            string mensajeGarante = string.Empty;

            if (model.codSolicitud == 0)
            {
                //agrego validacion para asignar el desgravamen en operaciones antiguas -> NORMAL 1.6
                string tiposDesgravamen = ConfigurationManager.AppSettings["TIPO_DESGRAVAMEN_PLANTILLA"].ToString();
                List<string> tiposDesgravamenList = tiposDesgravamen.Split(',').ToList();

                if (string.IsNullOrEmpty(model.credito.CapitalConstante) || !tiposDesgravamenList.Contains(model.credito.CapitalConstante.ToString()))
                {
                    model.credito = new Credito();
                    model.credito.CapitalConstante = "NORMAL";
                }

                log.RegistroLogInformacion($"[GetEvaluacion] [BUSQUEDA INFOCLIENTE TITULAR]");
                model.datosTitular = solicitudService.BusquedaInfocliente(solicitudService.IdcTG(model.resumen.nroDocumento.PadLeft(8, '0')), "Q", model.resumen.extensionDocumento, solicitudService.ComplementoTG(model.resumen.nroDocumento.PadLeft(8, '0')), ref mensajeTitular, model.resumen.tipoVivienda, model.resumen.matriculaOficial);
                model.garantia = !string.IsNullOrEmpty(model.resumen.nroDocumentoGarantia);
                model.titular.tipo = "T";
                if (model.garantia)
                {
                    log.RegistroLogInformacion($"[GetEvaluacion] [BUSQUEDA INFOCLIENTE GARANTE/CONYUGUE]");
                    model.datosGarante = solicitudService.BusquedaInfocliente(solicitudService.IdcTG(model.resumen.nroDocumentoGarantia.PadLeft(8, '0')), "Q", model.resumen.extensionDocumentoGarantia, solicitudService.ComplementoTG(model.resumen.nroDocumentoGarantia.PadLeft(8, '0')), ref mensajeGarante, model.resumen.tipoVivienda, model.resumen.matriculaOficial);
                    model.garante.tipo = "G";
                }
                if (FLAG_SCOCRE_MICROCREDITO == "S")
                {
                    //agregar consulta score 
                    var scoreEvaluacionResonse = await solicitudService.GetScoreEvaluacion(model.datosTitular.fechaNacimiento, solicitudService.IdcTG(model.resumen.nroDocumento.PadLeft(8, '0')) + "Q" + model.resumen.extensionDocumento + solicitudService.ComplementoTG(model.resumen.nroDocumento.PadLeft(8, '0')), model.datosTitular.nombres, model.datosTitular.paterno, model.datosTitular.materno, model.datosTitular.sexo, model.datosTitular.cic, model.datosTitular.codCiiu, model.datosTitular.indicadorVivienda, model.datosTitular.estadoCivil, model.resumen.patrimonio);
                    model.titular.score = scoreEvaluacionResonse.data.mensaje.ToUpper();
                }
            }
            else
            {
                var lstPersonas = await solicitudService.GetSolicitudPersona(model.codSolicitud);
                model.datosTitular = lstPersonas.FirstOrDefault(x => x.tipoPersona == "T");
                model.titular.tipo = "T";
                #region agrego variable estatica  para guardar el valor del CIC del titular
                model.CIC_Consulta = model.datosTitular.cic;
                model.CIC_Consulta = model.CIC_Consulta.Length == 12 ? "    " + model.CIC_Consulta.Substring(4) : model.CIC_Consulta;
                model.desembolso.cuenta = await solicitudService.lstCuentas(model.CIC_Consulta);
                #endregion

                PersonaModel conyuge = lstPersonas.FirstOrDefault(x => x.tipoPersona == "C");
                if (conyuge != null)
                {
                    string nroIdc = conyuge.numIDC.PadLeft(8, '0').Substring(0, 8);
                    string extension = conyuge.numIDC.PadLeft(8, '0').Substring(8);
                    model.datosTitular.numIdcConyuge = nroIdc + conyuge.tipoIDC + extension;
                    model.datosTitular.paternoConyuge = conyuge.paterno;
                    model.datosTitular.maternoConyuge = conyuge.materno;
                    model.datosTitular.nombresConyuge = conyuge.nombres;
                }

                model.garantia = lstPersonas.Count(x => x.tipoPersona == "F") == 0 ? false : true;
                if (model.garantia)
                {
                    model.datosGarante = lstPersonas.FirstOrDefault(x => x.tipoPersona == "F");
                    model.garante.tipo = "G";
                }
                model.solicitud =await solicitudService.GetSolicitud(model.codSolicitud);
                model.solicitud.getDesgravamen =await solicitudService.GetTasaDesgravamen(model.codSolicitud);
                model.credito.CapitalConstante = model.solicitud.getDesgravamen.nemonico;
                model.solicitud.codCpop = model.solicitud.cpop ? "S" : "N";
                model.solicitud.codSolicitud = model.codSolicitud;
                model.solicitud.tipoOperacion = 0;

                var lstCpop = new List<SelectListItem>();
                lstCpop.Add(new SelectListItem { Text = "SI", Value = "S" });
                lstCpop.Add(new SelectListItem { Text = "NO", Value = "N" });
                ViewBag.lstCpop = lstCpop;

                var lstTipoOperacion = refinanciamientoCompraService.GetTipoOperacion().Select(x => new SelectListItem
                {
                    Text = x.Operacion,
                    Value = x.Codigo.ToString()
                }).ToList();
                ViewBag.lstTipoOperacion = lstTipoOperacion;

                var _lstProductos = await solicitudService.GetProductos();
                var lstProductos = _lstProductos.Select(x => new SelectListItem
                {
                    Text = x.descripcion,
                    Value = x.codProducto.ToString()
                }).ToList();
                var selectTipoProductos = lstProductos.FirstOrDefault(x => x.Text.Trim() == model.solicitud.productoMicrocredito);

                var filteredList = lstProductos.Where(p => p.Value == selectTipoProductos.Value).ToList();

                model.solicitud.codTipoCredito = selectTipoProductos.Value;
                ViewBag.lstProductos = filteredList;

                var _lstCiiu =await solicitudService.GetCiiu();
                var lstCiiu = _lstCiiu.Select(x => new SelectListItem
                {
                    Text = $"{x.codigo} - {x.descripcion}",
                    Value = x.codigo
                }).ToList();
                ViewBag.lstCIIU = lstCiiu;

                var _lstFinalidad =await solicitudService.GetFinalidad(model.solicitud.codTipoCredito);
                var lstFinalidad = _lstFinalidad.Select(x => new SelectListItem
                {
                    Text = x.finalidad,
                    Value = x.codFinalidad
                }).ToList();
                ViewBag.lstFinalidad = lstFinalidad;

                var tipoOperacion = refinanciamientoCompraService.GetCompraRefinanciamiento(model.codSolicitud);
                if (tipoOperacion.state)
                {
                    List<CompraRefinanciamiento> tipo = tipoOperacion.data;
                    if (tipo != null)
                    {
                        if (tipo.Exists(x => x.CodOperacion == 3))
                        {
                            model.solicitud.tipoOperacion = 3;
                            var response = refinanciamientoCompraService.GetRefinanciamiento(model.codSolicitud);
                            model.solicitud.refinanciamiento = response.data;
                            model.solicitud.compreDeuda = new Model.Common.CompraDeudaModel
                            {
                                moneda = tipo.FirstOrDefault(x => x.CodOperacion == 3).moneda,
                                monto = tipo.FirstOrDefault(x => x.CodOperacion == 3).Monto
                            };
                        }
                        if (tipo.Exists(x => x.CodOperacion == 1) || tipo.Exists(x => x.CodOperacion == 2))
                        {
                            model.solicitud.tipoOperacion = 1;
                            var compra = tipo.FirstOrDefault(x => x.CodOperacion == 1 || x.CodOperacion == 2);
                            model.solicitud.compreDeuda = new Model.Common.CompraDeudaModel
                            {
                                moneda = compra.moneda,
                                monto = compra.Monto
                            };
                        }
                        if (tipo.Exists(x => x.CodOperacion == 3) && (tipo.Exists(x => x.CodOperacion == 1) || tipo.Exists(x => x.CodOperacion == 2)))
                        {
                            model.solicitud.tipoOperacion = 5;

                            var response = refinanciamientoCompraService.GetRefinanciamiento(model.codSolicitud);
                            model.solicitud.refinanciamiento = response.data;

                            var compra = tipo.FirstOrDefault(x => x.CodOperacion == 1 || x.CodOperacion == 2);
                            model.solicitud.compreDeuda = new Model.Common.CompraDeudaModel
                            {
                                moneda = compra.moneda,
                                monto = compra.MontoCompra
                            };
                        }
                    }
                }
                if (FLAG_SCOCRE_MICROCREDITO == "S")
                {
                   
                    //agregar consulta score 
                    model.solicitud.descripcionScore = await solicitudService.GetScoreGeneradoByCod(model.codSolicitud.ToString());
                    
                    switch (model.solicitud.descripcionScore)
                    {
                        case "Muy Bueno":
                            ViewBag.lstProductos = filteredList.Where(p => p.Text.Contains(" A")).ToList();
                            break;
                        case "Bueno":
                            ViewBag.lstProductos = filteredList.Where(p => p.Text.Contains(" B")).ToList();
                            break;
                        case "Regular":
                            ViewBag.lstProductos = filteredList.Where(p => p.Text.Contains(" C")).ToList();
                            break;
                        case "Malo":
                            ViewBag.lstProductos = filteredList.Where(p => p.Text.Contains(" D")).ToList();
                            break;
                    }
                }
            }

            model.json = JsonConvert.SerializeObject(model);
            ViewBag.error = !string.IsNullOrEmpty(mensajeTitular) && !string.IsNullOrEmpty(mensajeGarante);
            ViewBag.mensajeTitular = mensajeTitular;
            ViewBag.mensajeGarante = mensajeGarante;
            return View("View", model);
        }


        [HttpPost]
        public ActionResult Save(EvaluacionCuantitativa model)
        {
            EvaluacionService service = new EvaluacionService();
            bool response = service.Save(user.matricula, model.json);
            if (response)
                return RedirectToAction("Index", "Home");
            else
            {
                EvaluacionCuantitativa _model = JsonConvert.DeserializeObject<EvaluacionCuantitativa>(model.json);
                _model.json = JsonConvert.SerializeObject(model);
                return View("View", _model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Rechazar(EvaluacionCuantitativa model)
        {
            EvaluacionService service = new EvaluacionService();
            bool response = service.Rechazar(model.nroEvaluacion, user.matricula);
            if (response)
                return RedirectToAction("Index", "Home");
            else
            {
                ViewBag.Error = "NO SE PUDO REALIZAR EL CAMBIO A RECHAZADO.";
                return await GetEvaluacion(model.nroEvaluacion);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Aprobar(EvaluacionCuantitativa model)
        {
            try
            {
                string NOMBRE_APLICATIVO = ConfigurationManager.AppSettings["CANAL"].ToString();
                string FLAG_SCOCRE_MICROCREDITO = ConfigurationManager.AppSettings["FLAG_SCORE_MICROCREDITO"].ToString();

                EvaluacionService service = new EvaluacionService();
                SolicitudService solicitudService = new SolicitudService();
                SolicitudRequest request = new SolicitudRequest();
                #region logica de asignacion para desgravament tradicional o plus
                string seguro_Desgravamen = model.credito.CapitalConstante.Trim() == "NORMAL" ? "I" : "F";
                #endregion
                model = JsonConvert.DeserializeObject<EvaluacionCuantitativa>(model.json);
                request.datosBasicos = new Datosbasicos();
                request.datosBasicos.complemento = solicitudService.ComplementoTG(model.resumen.nroDocumento.PadLeft(8, '0'));
                request.datosBasicos.extension = model.resumen.extensionDocumento;
                request.datosBasicos.idc = solicitudService.IdcTG(model.resumen.nroDocumento.PadLeft(8, '0'));
                request.datosBasicos.tipo = "Q";
                request.solicitud = new DatosSolicitud();
                request.solicitud.codOficina = $"{model.resumen.sucursal}-{model.resumen.agencia}";
                request.solicitud.correoElectronico = model.datosTitular.email;
                request.solicitud.indicadorDeVivienda = model.datosTitular.indicadorVivienda;
                request.solicitud.matriculaAuxuliar = model.resumen.matriculaOficial;
                request.solicitud.matriculaUsuario = model.resumen.matriculaOficial;
                request.solicitud.matriculaVendedor = model.resumen.matriculaOficial;
                request.solicitud.nombreAplicacion = NOMBRE_APLICATIVO;
                request.solicitud.nroDependientes = model.resumen.nroDependientes;
                request.solicitud.tipoRenta = 5;
                request.patrimonio = new DatosPatrimonio();
                request.patrimonio.Activo = model.resumen.activos;
                request.patrimonio.NroPersonasOcupadas = model.resumen.nroPersonasOcupadas;
                request.patrimonio.Patrimonio = model.resumen.patrimonio;
                request.patrimonio.TipoSector = model.resumen.sectorEconomico;
                request.patrimonio.VentaAnual = model.resumen.ventas;
                var solicitud = await solicitudService.CrearSolicitud(request);
                if (solicitud.state == 200)
                {

                    var condiciones =await solicitudService.Condiciones(new CalculoRequest
                    {
                        codSolicitud = solicitud.data,
                        cpop = !model.resumen.cpop.Contains("NO"),
                        destinoCredito = model.resumen.ciiuDestinoCredito,
                        diaPago = model.resumen.diaPago,
                        finalidad = model.resumen.finalidad,
                        garantia = new GarantiaRequest
                        {
                            descripcion = model.resumen.garantiaDescripcion,
                            montoGarantia = model.resumen.garantiaMonto,
                            nroGarantia = model.resumen.codigoGarantia
                        },
                        matricula = user.matricula,
                        monto = model.resumen.monto,
                        plazo = model.resumen.plazo,
                        productoMicrocredito = model.resumen.tipoCredito,
                        tasa = model.resumen.tasa,
                        tipoCredito = model.resumen.ciiuDestinoCredito,
                        CodSeguroDesgravamen = seguro_Desgravamen
                    });
                    if (condiciones.state == 200)
                    {
                        if (FLAG_SCOCRE_MICROCREDITO == "S")
                        {
                            //genera score Aprobación
                            var responseScore = await solicitudService.GetScore(solicitud.data.ToString(), user.matricula);
                        }

                        if (model.garantia)
                        {
                            PersonaModel model_conyugue = new PersonaModel();
                            if (!string.IsNullOrEmpty(model.datosGarante.numIdcConyuge))
                            {
                                if (model.datosGarante.numIdcConyuge != "00")
                                {
                                    log.RegistroLogInformacion($"[IDC] [CONYUGUE GARANTE] {model.datosGarante.numIdcConyuge}");
                                    string mensaje = string.Empty;
                                    string cadena = model.datosGarante.numIdcConyuge.Trim();
                                    string idc = cadena.Substring(0, 8);
                                    string complemento = cadena.Substring(cadena.Length - 2);
                                    string TipoCI = cadena.Substring(8, 1);
                                    string extension = cadena.Substring(9, cadena.Length - 11);
                                    model_conyugue = solicitudService.BusquedaInfocliente(idc, TipoCI, extension, complemento, ref mensaje, model.resumen.tipoVivienda, model.resumen.matriculaOficial);
                                    model_conyugue.tipoIDC = TipoCI;
                                }

                            }
                            await solicitudService.AddFiadores(solicitud.data, model.datosGarante, model_conyugue);
                        }

                        bool response = service.Aprobar(model.nroEvaluacion, solicitud.data, user.matricula);
                        if (response)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Error = "NO SE PUDO REALIZAR EL CAMBIO A APROBADO.";
                            return await GetEvaluacion(model.nroEvaluacion);
                        }
                    }
                    else
                    {
                        ErrorControl error = new ErrorControl
                        {
                            error = 500,
                            exception = condiciones.exception
                        };
                        Session["error"] = error;
                        return RedirectToAction("Index", "Error");
                    }
                }
                else
                {
                    ErrorControl error = new ErrorControl
                    {
                        error = 500,
                        exception = solicitud.exception
                    };
                    Session["error"] = error;
                    return RedirectToAction("Index", "Error");
                }
            }
            catch (Exception ex)
            {
                ErrorControl error = new ErrorControl
                {
                    error = 500,
                    exception = ex.Message
                };
                Session["error"] = error;
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public JsonResult GetDeudasInternas(EvaluacionCuantitativa model)
        {
            RefinanciamientoCompraService refinanciamientoCompraService = new RefinanciamientoCompraService();
            var response = refinanciamientoCompraService.GetRefinanciamiento(model.codSolicitud);
            if (response.state)
            {
                response.data.refinanciamientoTitular = response.data.refinanciamientoTitular == null ? new List<RefinanciamientoCompra>() : response.data.refinanciamientoTitular;
                response.data.refinanciamientoConyuge = response.data.refinanciamientoConyuge == null ? new List<RefinanciamientoCompra>() : response.data.refinanciamientoConyuge;
                var operaciones = new
                {
                    titular = response.data.refinanciamientoTitular,
                    conyuge = response.data.refinanciamientoConyuge,
                    mensaje = ""
                };
                return Json(operaciones, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var operaciones = new
                {
                    titular = new List<RefinanciamientoCompra>(),
                    conyuge = new List<RefinanciamientoCompra>(),
                    mensaje = ""
                };
                return Json(operaciones, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Guardar(CondicionesSolicitudModel model)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                #region logica de asignacion para desgravament tradicional o plus
                model.codSeguroDesgravamen = model.codSeguroDesgravamen.Trim() == "NORMAL" ? "I" : "F";
                #endregion
                string FLAG_SCOCRE_MICROCREDITO = ConfigurationManager.AppSettings["FLAG_SCORE_MICROCREDITO"].ToString();

                model.matricula = user.matricula;

                var response =await solicitudService.GuardarCondiciones(model);
                if (response.state == 200)
                {
                    if (FLAG_SCOCRE_MICROCREDITO == "S")
                    {
                        //genera score Actualización
                        var responseScore = await solicitudService.GetScore(model.codSolicitud.ToString(), user.matricula);
                    }
                    //agregar consulta score 
                    //model.solicitud.descripcionScore = await solicitudService.GetScoreGeneradoByCod(model.codSolicitud.ToString());
                    RefinanciamientoCompraService service = new RefinanciamientoCompraService();
                    if (model.cbCompraRef)
                    {
                        if (model.codOperacion == 1 || model.codOperacion == 2 || model.codOperacion == 3)
                        {
                            List<TipoOperacion> lstParametro = service.GetTipoOperacion();
                            TipoOperacion selectParametro = lstParametro.FirstOrDefault(x => x.Codigo == model.codOperacion);

                            TipoOperacionRequest request = new TipoOperacionRequest();
                            request.codSolicitud = model.codSolicitud;
                            request.codTipoOperacion = model.codOperacion;
                            request.tipoOperacion = selectParametro.Operacion;
                            request.matricula = user.matricula;
                            request.moneda = selectParametro.TipoMoneda.Trim();
                            if (model.operacion.lstRefinanciamiento != null)
                            {
                                request.monto = model.operacion.lstRefinanciamiento.Sum(x => x.monto);
                                request.lstRefinanciamiento = model.operacion.lstRefinanciamiento.Select(x => new RefinanciamientoRequest
                                {
                                    monto = x.monto,
                                    nroOperacion = x.nroOperacion.Trim()
                                }).ToList();
                            }
                            else
                                request.monto = model.operacion.monto;
                            request.nroSolicitud = model.nroSolicitud;
                            if (request.monto > 0)
                                service.Guardar(request, model.codOperacion);
                            else
                            {
                                var _responseCompra = new
                                {
                                    mensaje = "MONTO COMPRA/REFINANCIAMIENTO INVÁLIDO: " + request.monto
                                };
                                return Json(_responseCompra);
                            }
                        }
                    }
                    else
                        service.Limpiar(model.codSolicitud, model.nroSolicitud, user.matricula);

                }
                var _response = new
                {
                    mensaje = "LA INFORMACION FUE GUARDADA CORRECTAMENTE."
                };
                return Json(_response);
            }
            catch (Exception ex)
            {
                var _response = new
                {
                    mensaje = $"ERROR: NO SE PUDO GUARDAR LA INFORMACION DE MANERA CORRECTA; MENSAJE: {ex.Message}"
                };
                return Json(_response);
            }
        }

        public async Task<JsonResult> GetListTipoCredito(string tipoCredito)
        {
            SolicitudService solicitudService = new SolicitudService();
            var _lstFinalidad =await solicitudService.GetFinalidad(tipoCredito);
            var lstFinalidad = _lstFinalidad.Select(x => new SelectListItem
            {
                Text = x.finalidad,
                Value = x.codFinalidad
            }).ToList();
            return Json(lstFinalidad, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetOfertas(string codDestinoCredito, decimal monto, string producto, int plazo, int diaPago, string tipoDesgravamen)
        {
            #region logica de asignacion para desgravament tradicional o plus
            tipoDesgravamen = tipoDesgravamen.Trim() == "NORMAL" ? "I": "F";
            #endregion
            SolicitudService solicitudService = new SolicitudService();
            var _ciiu = await solicitudService.GetCiiu();
            var ciiu = _ciiu.FirstOrDefault(x => x.codigo == codDestinoCredito);
            var lstOferta =await solicitudService.GetOfertasMicrocredito(ciiu.tipoCredito, monto, producto, plazo, diaPago, false, tipoDesgravamen);
            return Json(lstOferta, JsonRequestBehavior.AllowGet);
        }


        #region add consulta Inventario
        [HttpGet]
        public ActionResult GetInventario(string codSolicitud, string _nroEvaluacion)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                var response = solicitudService.ObtenerInventario(codSolicitud, _nroEvaluacion);
                return View("View", response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ADDInventario(DatosAdicionalesModel model)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                var response = solicitudService.GuardarInventario(model);

                var result = new
                {
                    SuccessMessage = "Inventario guardado con éxito.",
                    Response = response
                };

                return Json(result);
            }
            catch (Exception e)
            {
                var result = new
                {
                    ErrorMessage = "Hubo un error al guardar el inventario.",
                    Exception = e.Message
                };

                return Json(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult> RealizarDesembolso(DatosDesembolso model)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();

                //Validacion documento impreso y estado de la solicitud excepcion
                ExceptionsByCodeRequest excepcionResponseByCode = new ExceptionsByCodeRequest
                {
                    CodigoSolicitud = model.codSolicitud,
                    Matricula = model.matricula
                };
                var respuesta = await solicitudService.ValidacionExepciones(excepcionResponseByCode);
                if (respuesta)
                {
                    RefinanciamientoCompraService refinanciamientoCompraService = new RefinanciamientoCompraService();

                    model.Desembolso.matriculaDesembolso = model.matricula;
                    model.Desembolso.codigoSolicitud = model.codSolicitud;
                    model.Desembolso.nroCuenta = model.cuentaSelecionada;
                    //DESEMBOLSO DE LA SOLICITUD
                    ResponseDesembolso response = await solicitudService.Desembolso(model);

                    if (response.resultado)
                    {
                        //COBRO DEL MICROSEGURO
                        EvaluacionCuantitativa ConsultaSeguro = new EvaluacionCuantitativa();
                        var respuesta_seguros = solicitudService.ConsultaSegurosMICROCREDITO(Convert.ToString(model.codSolicitud), model.numSolicitud);
                        if (respuesta_seguros.IsMicroSeguroDevida)
                        {
                            RespSeguro result = null;
                            string URL_COORDINADOR = ConfigurationManager.AppSettings["API_COORDINADOR"].ToString();
                            var request = JsonConvert.SerializeObject(new
                            {
                                codSolicitud = model.Desembolso.codigoSolicitud
                            });
                            log.RegistroLogInformacion($"[INICIA] [COBRO SEGURO] VARIABLES:{JsonConvert.SerializeObject(model.Desembolso.codigoSolicitud)}");
                            result = Util.Post<RespSeguro>(URL_COORDINADOR, $"Seguro/Cobro?codSolicitud={model.Desembolso.codigoSolicitud}", request);
                            log.RegistroLogInformacion($"[RESPUESTA] [COBRO MICROSEGURO]: {JsonConvert.SerializeObject(result)}");
                            if (result.Codigo == "000")
                                response.mensajerespuesta = "DESEMBOLSO: " + response.mensajerespuesta.ToUpper() + Environment.NewLine + "MICROSEGURO: Se realizó el cobro del seguro";
                            else
                                response.mensajerespuesta = "DESEMBOLSO: " + response.mensajerespuesta.ToUpper() + Environment.NewLine + "MICROSEGURO: Error al realizar el cobro del seguro";
                        }

                        //COMPRA Y REFINANCIAMIENTO
                        var respCompraReu = refinanciamientoCompraService.CompraReutilizacionDebito(Convert.ToInt32(model.codSolicitud), model.matricula);
                        if (!respCompraReu.message.Contains("NO EXISTE LA SOLICITUD"))
                        {
                            if (respCompraReu != null && respCompraReu.state)
                            {
                                if (respCompraReu.data.CodOperacion == 1 && respCompraReu.data.CodOperacion == 2)
                                    response.mensajerespuesta = response.mensajerespuesta.ToUpper() + Environment.NewLine + "- DEBITO AUTOMATICO REALIZADO EXITOSAMENTE";
                                else
                                    response.mensajerespuesta = response.mensajerespuesta.ToUpper() + Environment.NewLine + "- DEBITO Y PAGO AUTOMATICO REALIZADO EXITOSAMENTE";
                            }
                            else
                            {
                                //PROCESO EXTORNO PARA ERROR DE COMPRA - REFINANCIAMIENTO
                                ResponseGeneric<bool> responseExtorno = refinanciamientoCompraService.PostExtorno(int.Parse(model.codSolicitud), model.matricula);
                                if (responseExtorno.state)
                                    response.mensajerespuesta = response.mensajerespuesta.ToUpper() + Environment.NewLine + "- SE REALIZO EL EXTORNO DEL ABONO. FAVOR CONTACTAR CON CENTRO DE ASISTENCIA TECNICA.";
                                else
                                    response.mensajerespuesta = response.mensajerespuesta.ToUpper() + Environment.NewLine + "- ERROR AL REALIZAR EL EXTORNO AUTOMATICO. FAVOR CONTACTAR CON CENTRO DE ASISTENCIA TECNICA.";
                            }
                        }
                        return Json(new { success = true, message = response.mensajerespuesta.ToUpper() });
                    }
                    else
                        return Json(new { success = true, message = response.mensajerespuesta.ToUpper() });
                }
                else
                {
                    return Json(new { success = false, errorMessage = "No se realizo el desembolso, Validar que se cuente con Documento Impreso y una de las Excepciones se encuentre APROBADA" });
                }

               
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = "Ocurrió un error en el proceso de desembolso. Favor contactar con centro de asistencia técnica." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GenerarContrato(DatosDesembolso model)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                var inventario = solicitudService.ObtenerInventario(model.codSolicitud, model.numSolicitud);
                model.contratoCronograma.codSolicitud = model.codSolicitud;
                model.contratoCronograma.usuario = model.matricula;
                model.contratoCronograma.fechaInventario = inventario.dataInventario.fechaInventario;
                model.contratoCronograma.inventario = FintrarInventario(inventario.dataInventario);
                model.contratoCronograma.cuentaDesembolso = model.cuentaSelecionada;
                string messageValidation = solicitudService.Validaciones(model.contratoCronograma);
                if (!string.IsNullOrEmpty(messageValidation))
                {
                    return Json(new { success = false, message = messageValidation });
                }
                var (fileBytes, fileName) = await solicitudService.Contrato(model.contratoCronograma);
                if (fileBytes != null && fileName != null)
                {
                    return Json(new { success = true, fileBytes, fileName });
                }
                else
                {
                    return Json(new { success = false, message = "Error, No se pudo generar el Contrato" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GenerarCronograma(DatosDesembolso model)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                var inventario = solicitudService.ObtenerInventario(model.codSolicitud, model.numSolicitud);
                model.contratoCronograma.codSolicitud = model.codSolicitud;
                model.contratoCronograma.usuario = model.matricula;
                model.contratoCronograma.fechaInventario = inventario.dataInventario.fechaInventario;
                model.contratoCronograma.inventario = FintrarInventario(inventario.dataInventario);
                model.contratoCronograma.cuentaDesembolso = model.cuentaSelecionada;
                string messageValidation = solicitudService.Validaciones(model.contratoCronograma);
                if (!string.IsNullOrEmpty(messageValidation))
                {
                    return Json(new { success = false, errorMessage = $"{messageValidation}" });
                }
                var (fileBytes, fileName) = await solicitudService.Cronograma(model.contratoCronograma);
                if (fileBytes != null && fileName != null)
                {
                    return Json(new { success = true, fileBytes, fileName });
                }
                else
                {
                    return Json(new { success = false, errorMessage = "Error, No se pudo generar el Cronograma" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, errorMessage = e.Message });
            }
        }

        public List<string> FintrarInventario(DataInventario dataInventario)
        {
            List<string> inventarios = new List<string>();

            for (int i = 1; i <= 7; i++)
            {
                string inventarioValue = (string)typeof(DataInventario).GetProperty($"inventario{i}").GetValue(dataInventario);
                if (!string.IsNullOrEmpty(inventarioValue))
                {
                    inventarios.Add(inventarioValue);
                }
            }

            return inventarios;
        }

        [HttpPost]
        public async Task<ActionResult> ConsultaCuenta(EvaluacionCuantitativa model)
        {
            try
            {
                string NOMBRE_APLICATIVO = ConfigurationManager.AppSettings["CANAL"].ToString();
                SolicitudService solicitudService = new SolicitudService();
                model.desembolso.ReqNumCuenta.usuario = model.desembolso.matricula;
                model.desembolso.ReqNumCuenta.canal = NOMBRE_APLICATIVO;
                model.desembolso.ReqNumCuenta.numCta = model.desembolso.cuentaSelecionada;
                string messageValidation = solicitudService.ValidacionesConsultaCuenta(model.desembolso.ReqNumCuenta);
                if (!string.IsNullOrEmpty(messageValidation))
                {
                    return Json(new { success = false, message = messageValidation });
                }
                var response = await solicitudService.ConsultaCuenta(model.desembolso.ReqNumCuenta);

                return Json(new { success = true, data = response });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = $"{model.desembolso.ReqNumCuenta.numCta}" });
            }
        }

        [HttpGet]
        public ActionResult GetSeguros(string codSolicitud, string _nroEvaluacion)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                var response = solicitudService.ConsultaSegurosMICROCREDITO(codSolicitud, _nroEvaluacion);
                return View("View", response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardarSeguros(DatosDesembolso model)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                var response = await solicitudService.ADDSegurosMICROCREDITO(model.ConsultaSegurosResponse, model.codSolicitud, model.numSolicitud);

                if (response == false)
                {
                    return Json(new { success = false });
                }

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false });
            }
        }
        #endregion

        #region EXCEPCIONES
        public async Task<ActionResult> VincularException(List<Excepcion> excepciones, string codsolicitud, string matricula)
        {
            try
            {
                SolicitudService solicitudService = new SolicitudService();
                List<object> responses = new List<object>();

                foreach (var data in excepciones)
                {
                    if (data.TipoProducto == "05")
                    {
                        if (data.CodigoSolicitud.ToString().Trim() == codsolicitud.Trim() || data.CodigoSolicitud == 0)
                        {
                            AssociateExceptionRequest associate = new AssociateExceptionRequest
                            {
                                codigoSolicitud = codsolicitud,
                                IdExcepcion = data.IdExcepcion,
                                asociarDesasociar = data.EstadoAsociacion,
                                matricula = matricula
                            };

                            var response = await solicitudService.AssociateDisassociateException(associate);
                            if (!response.Status)
                            {
                                throw new Exception("La asociación de la excepción falló.");
                            }
                            responses.Add(new
                            {
                                IdExcepcion = data.IdExcepcion,
                                Response = response
                            });
                        }
                    }
                }

                var result = new
                {
                    SuccessMessage = "Se registraron las excepciones con éxito.",
                    Responses = responses,
                    Exitoso = true
                };

                return Json(result);
            }
            catch (Exception e)
            {
                var result = new
                {
                    ErrorMessage = "Hubo un error al registrar las excepciones.",
                    Exception = e.Message,
                    Exitoso = false
                };

                return Json(result);
            }
        }
        #endregion
    }
}
using MICRE_APP_EXCEPCIONES.Models;
using MICRE_APP_EXCEPCIONES.SessionHandler;
using MICRE_APP_EXCEPCIONES.TDOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NETCORE.Models;
using NETCORE.Services.Interfaces;
using NETCORE.TDOs;
using Newtonsoft.Json;
using System.Diagnostics;

namespace NETCORE.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SessionHandler _sessionHandler;
        private readonly ILogger<HomeController> _logger;
        private readonly IFormularioInmuebleService _service;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IFormularioInmuebleService service, SessionHandler sessionHandler, IConfiguration configuration)
        {
            _logger = logger;
            _sessionHandler = sessionHandler;
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _configuration = configuration;
        }
        //[Authorize(Roles = "Administrador")]
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> IndexExcepcionAsync(ClienteBusqueda clienteBusqueda)
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "IndexExcepcion";
            ViewBag.Message = HttpContext.Session.GetString("message");
            HttpContext.Session.Remove("message");
            var usuarioRecuperado = SessionHandler<TokenData>("token");
            if (clienteBusqueda.Ci != null)
            {
                string numeroConCeros = clienteBusqueda?.Ci?.PadLeft(8, '0');

                var clienteResquest = new ClienteIdc
                {
                    Idc = numeroConCeros + clienteBusqueda.Extencion + clienteBusqueda.Complemento,
                    Canal = "Micredito",
                    Usuario = usuarioRecuperado?.Matricula
                };

                try
                {
                    var response = await _service.FindUser(clienteResquest);
                    if (response.Status == false)
                    {
                        HttpContext.Session.SetString("message", response.Message);
                        return RedirectToAction("IndexExcepcion");
                    }
                    else
                    {
                        HttpContext.Session.SetString("clienteBusqueda", clienteBusqueda.Ci + "," + clienteBusqueda.Extencion + "," + clienteBusqueda.Complemento);
                        return RedirectToAction("InmuebleFormTitular", clienteBusqueda);
                    }
                }
                catch (Exception)
                {
                    HttpContext.Session.SetString("clienteBusqueda", clienteBusqueda.Ci + "," + clienteBusqueda.Extencion + "," + clienteBusqueda.Complemento);
                    return RedirectToAction("InmuebleFormTitular");
                }
            }

            ViewBag.NombreUsuario = usuarioRecuperado?.NombreUsuario;

            return View();
        }
        //fromulario inmueble LOGIC4
        public async Task<IActionResult> InmuebleFormTitular(ClienteBusqueda clienteBusqueda)
        {
            HttpContext.Session.Remove("ExceptionsArray");

            var usuarioRecuperado = SessionHandler<TokenData>("token");
            await _sessionHandler.SignInAsync(usuarioRecuperado?.Matricula);

            var productosApi = await _service.GetProductos("PRODUCTO");
            var laboralApi = await _service.GetProductos("LABORAL");
            var finalidadApi_C = await _service.GetProductos("FINALIDAD CONSUMO");
            var finalidadApi_N = await _service.GetProductos("FINALIDAD NEGOCIO");
            var finalidadApi_H = await _service.GetProductos("FINALIDAD HIPOTECARIO");
            var antiguiedadApi = await _service.GetProductos("ANTIGUEDAD");
            var civilApi = await _service.GetProductos("ESTADO_CIVIL");
            var profesionApi = await _service.GetProductos("PROFESION");
            var actividadApi = await _service.GetProductos("ACTIVIDAD");
            var lstScore = await _service.GetProductos("SCORE");
            var lstTipoCliente = await _service.GetProductos("TIPO_CLIENTE");
            //var cambioApi = await _service.GetCambio();
            var itemScoreDefault = lstScore.Data.First(x => x.Codigo.Equals("0"));
            var itemTipoClienteDefault = lstTipoCliente.Data.First(x => x.Codigo.Equals("0"));
            //ViewBag.Cambio = cambioApi?.Data?.TipoCambioVenta;
            ViewBag.Cambio = _configuration.GetValue<decimal>("Configuration.Site:TipoCambioContable");
            var viewModel = new InmuebleForm
            {
                ProductosChecks = productosApi.Data,
                Laboral = laboralApi.Data,
                Finalidades_C = finalidadApi_C.Data,
                Finalidades_N = finalidadApi_N.Data,
                Finalidades_H = finalidadApi_H.Data,
                Antiguiedades = antiguiedadApi.Data,
                EstadoCivil = civilApi.Data,
                Profesiones = profesionApi.Data,
                ActividadEconomica = actividadApi.Data,
                score = itemScoreDefault.Descripcion,
                tipoCliente = itemTipoClienteDefault.Descripcion,
                LstScore = lstScore.Data.Select(x => new SelectListItem(x.Descripcion, x.Descripcion)).ToList(),
                LstTipoCliente = lstTipoCliente.Data.Select(x => new SelectListItem(x.Descripcion, x.Descripcion)).ToList()
            };
            try
            {
                ViewBag.NombreUsuario = usuarioRecuperado?.NombreUsuario;

                if (clienteBusqueda.Ci != null)
                {
                    itemTipoClienteDefault = lstTipoCliente.Data.First(x => x.Codigo.Equals("1"));
                    viewModel.tipoCliente = itemTipoClienteDefault.Descripcion;
                    string numeroConCeros = clienteBusqueda.Ci.PadLeft(8, '0');
                    var clienteResquest = new ClienteIdc
                    {
                        Idc = numeroConCeros + clienteBusqueda.Extencion + clienteBusqueda.Complemento,
                        Canal = "Micredito",
                        Usuario = usuarioRecuperado?.Matricula
                    };

                    var response = await _service.FindUser(clienteResquest);

                    ViewBag.Conyugue = response?.Data?.TieneConyugue;
                    ViewBag.Cic = response?.Data?.Cic;

                    if (response != null && response.Data != null)
                    {
                        // datos del titular
                        ViewBag.Ci = response.Data.Ci;
                        ViewBag.Extension = response.Data.Extension;
                        ViewBag.Complemento = response.Data.Complemento;
                        ViewBag.Cic = response.Data.Cic;
                        ViewBag.NombreTitular = response.Data.Nombre;
                        ViewBag.ApellidoPTitular = response.Data.Paterno;
                        ViewBag.ApellidoMTitular = response.Data.Materno;
                        ViewBag.EstadoCivil = response.Data.IdEstadoCivil.ToString();
                        ViewBag.IdSituacionLaboral = response.Data.IdSituacionLaboral;
                        ViewBag.IdProfesion = response.Data.IdProfesion;
                        ViewBag.IdActividadEconomica = response.Data.IdActividadEconomica;
                        ViewBag.Empresa = response.Data.Empresa;
                        ViewBag.IdAntiguedadLaboral = response.Data.idAntiguedadLaboral;
                        ViewBag.TieneConyugue = response.Data.TieneConyugue;
                        ViewBag.ClientePDH = response.Data.ClientePDH;
                        ViewBag.CiConyugue = response.Data.CiConyugue;
                        ViewBag.ExtensionConyugue = response.Data.ExtensionConyugue;
                        ViewBag.ComplementoConyugue = response.Data.ComplementoConyugue;
                        ViewBag.NombreConyugue = response.Data.NombreConyugue;
                        ViewBag.ApellidoPConyugue = response.Data.PaternoConyugue;
                        ViewBag.ApellidoMConyugue = response.Data.MaternoConyugue;
                        ViewBag.IdSituacionLaboralConyugue = response.Data.IdSituacionLaboralConyugue;
                        ViewBag.IdProfesionConyugue = response.Data.IdProfesionConyugue;
                        ViewBag.IdEstadoCivilConyugue = response.Data.IdEstadoCivilConyugue;
                        ViewBag.IdActividadEconomicaConyugue = response.Data.IdActividadEconomicaConyugue;
                        ViewBag.EmpresaConyugue = response.Data.EmpresaConyugue;
                        ViewBag.IdAntiguedadLaboralConyugue = response.Data.idAntiguedadLaboralConyugue;
                        HttpContext.Session.SetString("TitularData", JsonConvert.SerializeObject(response.Data));
                    }
                }
                else
                {
                    var data = HttpContext.Session.GetString("clienteBusqueda");

                    string[] splitData = data.Split(',');

                    ViewBag.Ci = splitData[0];
                    ViewBag.Extension = splitData[1].Substring(1);
                    ViewBag.Complemento = splitData[2];

                }
                return View(viewModel);
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode.HasValue ? (int)ex.StatusCode.Value : 500;
                Console.WriteLine(statusCode.ToString());
                ViewBag.ErrorMessage = $"Request failed with status code {statusCode}";
                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExceptRegisterForm(InmuebleForm inmuebleForm)
        {

            List<ProductoTDO> productosTdo = new List<ProductoTDO>();

            var usuarioRecuperado = SessionHandler<TokenData>("token");

            foreach (var producto in inmuebleForm.Productos)
            {
                if (producto.idProducto != "false")
                {
                    if (producto.valorInmuebleBOL != 0 && producto.valorInmuebleBOL != null)
                    {
                        producto.ltv = producto.montoSolicitadoBOL / producto.valorInmuebleBOL;

                    }
                    productosTdo.Add(producto);
                }
            }

            var rh = new FormTitularTDO
            {
                matricula = usuarioRecuperado?.NombreUsuario + " - " + usuarioRecuperado?.Cargo,
                tipoCliente = inmuebleForm.tipoCliente,
                score = inmuebleForm.score,
                InformacionPersonal = inmuebleForm.InformacionPersonalTitular,
                InformacionLaboral = inmuebleForm.InformacionLaboral,
                DeudaPersonal = inmuebleForm.DeudaPersonale,
                DeudaInmueble = inmuebleForm.DeudaInmueble,
                InformacionPersonalConyugue = inmuebleForm.InformacionPersonalConyugue,
                DeudaPersonalConyugue = inmuebleForm.DeudaPersonalConyugue,
                InformacionLaboralConyugue = inmuebleForm.InformacionLaboralConyugue,
                Productos = productosTdo

            };

            try
            {
                await _sessionHandler.SignInAsync(usuarioRecuperado?.Matricula);

                rh.InformacionPersonal.extension = rh.InformacionPersonal?.extension?.Substring(1);


                var response = await _service.SubmitTitularForm(rh);


                HttpContext.Session.SetString("ProcutosId", JsonConvert.SerializeObject(productosTdo));

                HttpContext.Session.SetString("Inmueble", JsonConvert.SerializeObject(response.data));

                var respaldos = new BackUps();
                HttpContext.Session.SetString("RespadoRequerido", JsonConvert.SerializeObject(respaldos));

                HttpContext.Session.SetString("message", "Formulario enviado correctamente");

                return RedirectToAction("ExceptRegister");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode.HasValue ? (int)ex.StatusCode.Value : 500;
                Console.WriteLine(statusCode.ToString());
                HttpContext.Session.SetString("message", $"Request failed with status code {statusCode}");
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HttpContext.Session.SetString("message", "Ocurrio un error inesperado");
                return View();
            }
        }

        public async Task<IActionResult> ExceptRegister()
        {
            ViewBag.Message = HttpContext.Session.GetString("message");

            var response = await _service.GetException();
            var productosIds = await _service.GetProductos("PRODUCTO");


            var usuarioRecuperado = SessionHandler<TokenData>("token");
            var clienteId = SessionHandler<List<ExceptionIdData>>("ExceptionsArray");
            var productoIdoRecuperado = SessionHandler<List<ProductoTDO>>("ProcutosId");

            HttpContext.Session.SetString("BackUpArray", JsonConvert.SerializeObject(new List<RegisterRespladoRequest>()));

            // aqui mostrare las excepciones que hay hasta ahora
            var inmueble = SessionHandler<FormInmuebleData>("Inmueble");

            var responseExceptions = await _service.GetExcepcionesByID(inmueble.idCliente.ToString());  /// aqui las excepciones registradas

            await _sessionHandler.SignInAsync(usuarioRecuperado.Matricula);
            ViewBag.NombreUsuario = usuarioRecuperado.NombreUsuario;

            if (clienteId == null)
            {
                clienteId = new List<ExceptionIdData>();
            }

            try
            {
                List<string> stringList = productoIdoRecuperado.Select(p => p.idProducto).ToList();
                List<int> intList = stringList.ConvertAll(int.Parse);

                var filteredResponse = productosIds.Data.Where(r => intList.Contains((r.IdParametro))).ToList();  /// aqui listo las excepcions a marcar

                // Verificar si todas las descripciones de Parametro existen en ExceptionIdData
                bool allDescriptionsExist = filteredResponse.All(p => responseExceptions.Data.Any(e => e.DescripcionProducto == p.Descripcion));

                var respaldosSubidos = SessionHandler<BackUps>("RespadoRequerido");

                // Verificar si alguna descripción contiene la palabra "tarjeta"
                bool containsTarjeta = filteredResponse.Any(p => p.Descripcion != null && p.Descripcion.Contains("Tarjeta", StringComparison.OrdinalIgnoreCase));

                respaldosSubidos.Tarjeta = containsTarjeta;

                HttpContext.Session.SetString("RespadoRequerido", JsonConvert.SerializeObject(respaldosSubidos));

                ViewBag.ExistAll = allDescriptionsExist;

                if (inmueble != null)
                {
                    var prod = inmueble.idProductos;
                    var model = new ExceptionViewModel
                    {
                        excepcionApi = response,
                        exception = new ExceptionModel(),
                        ProductosChecks = inmueble.idProductos,
                        exceptionList = clienteId
                    };

                    return View(model);
                }

                return View();

            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode.HasValue ? (int)ex.StatusCode.Value : 500;
                Console.WriteLine(statusCode.ToString());
                ViewBag.ErrorMessage = $"Request failed with status code {statusCode}";
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View();
            }

        }

        public async Task<IActionResult> ExceptRegisterE(ExceptionModel exception)
        {
            try
            {
                // aqui registro la excepcion
                var usuarioRecuperado = SessionHandler<TokenData>("token");
                await _sessionHandler.SignInAsync(usuarioRecuperado.Matricula);
                var ExceptionsApi = await _service.GetException();
                var result = ExceptionsApi.FirstOrDefault(e => e.Descripcion == exception.detalleBuscar);

                var productos = exception.idProductos;
                var newProductos = new List<string>();

                foreach (var producto in productos)
                {
                    if (producto != "false")
                    {
                        newProductos.Add(producto);
                    }
                }

                var request = new RegisterExceptionTDO
                {
                    matricula = usuarioRecuperado?.NombreUsuario + " - " + usuarioRecuperado?.Cargo,
                    idProductoExcepcion = result?.IdDetalle ?? 0,
                    justificacion = exception.Justificacion,
                    idProductos = newProductos
                };

                var response = await _service.RegistrarException(request);

                // aqui mostrare las excepciones que hay hasta ahora
                var inmueble = SessionHandler<FormInmuebleData>("Inmueble");

                var responseExceptions = await _service.GetExcepcionesByID(inmueble.idCliente.ToString());

                HttpContext.Session.SetString("ExceptionsArray", JsonConvert.SerializeObject(responseExceptions.Data));
                HttpContext.Session.Remove("message");

                return Redirect("ExceptRegister");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("message", ex.Message);
                return Redirect("ExceptRegister");
            }

        }

        public async Task<IActionResult> BorrarExcepcion(string id)
        {
            try
            {
                var responseExceptions = await _service.PostRemoveException(id);

                var inmueble = SessionHandler<FormInmuebleData>("Inmueble");

                var getAll = await _service.GetExcepcionesByID(inmueble.idCliente.ToString());
                HttpContext.Session.SetString("ExceptionsArray", JsonConvert.SerializeObject(getAll.Data));
                HttpContext.Session.Remove("message");
                return Redirect("ExceptRegister");
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                HttpContext.Session.SetString("message", ex.Message);
                return Redirect("ExceptRegister");
            }
        }

        public async Task<IActionResult> Actualizar(ExceptionModel exception)
        {
            try
            {
                var usuarioRecuperado = SessionHandler<TokenData>("token");

                await _sessionHandler.SignInAsync(usuarioRecuperado.Matricula);

                var ExceptionsApi = await _service.GetException();
                var result = ExceptionsApi.FirstOrDefault(e => e.Descripcion == exception.detalleBuscar);

                var request = new UpdateExceptionRequest
                {
                    Matricula = usuarioRecuperado?.NombreUsuario + " - " + usuarioRecuperado?.Cargo,
                    IdProductoExcepcion = exception.idProductoExcepcion,
                    Justificacion = exception.Justificacion,
                    IdExcepcion = result?.IdDetalle ?? 0,
                };

                var responseExceptions = await _service.PostUpdateException(request);

                var inmueble = SessionHandler<FormInmuebleData>("Inmueble");

                var getAll = await _service.GetExcepcionesByID(inmueble.idCliente.ToString());
                HttpContext.Session.SetString("ExceptionsArray", JsonConvert.SerializeObject(getAll.Data));
                HttpContext.Session.Remove("message");
                return Redirect("ExceptRegister");
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                HttpContext.Session.SetString("message", ex.Message);
                return Redirect("ExceptRegister");
            }

        }

        public async Task<IActionResult> CargarResplado()
        {
            ViewBag.Message = HttpContext.Session.GetString("message");
            try
            {
                var usuarioRecuperado = SessionHandler<TokenData>("token");
                var clienteId = SessionHandler<FormInmuebleData>("Inmueble")?.idCliente;
                var conyugueId = SessionHandler<FormInmuebleData>("Inmueble")?.idClienteConyugue;
                var respaldosSubidos = SessionHandler<BackUps>("RespadoRequerido");

                ViewBag.NombreUsuario = usuarioRecuperado?.NombreUsuario;
                await _sessionHandler.SignInAsync(usuarioRecuperado.Matricula);

                respaldosSubidos.conyuigueId = SessionHandler<FormInmuebleData>("Inmueble")?.idClienteConyugue.ToString();

                respaldosSubidos.MaxFilesizeExcepcion = _configuration.GetValue<int>("Configuration.Site:MaxFilesizeExcepcion");



                return View(respaldosSubidos);
            }
            catch (Exception ex)
            {
                return View(false);
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, string tipoRespaldo)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return Content("File not selected");
                }

                var requests = SessionHandler<List<RegisterRespladoRequest>>("BackUpArray");
                var usuarioRecuperado = SessionHandler<TokenData>("token");
                var inmueble = SessionHandler<FormInmuebleData>("Inmueble");
                // Obtener el nombre del archivo
                var fileName = Path.GetFileName(file.FileName);

                // Obtener el tipo de archivo (MIME type)
                var fileType = file.ContentType;

                // Leer el archivo en un arreglo de bytes
                byte[] fileBytes;
                using (var inputStream = file.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        inputStream.CopyTo(memoryStream);
                        fileBytes = memoryStream.ToArray();
                    }
                }

                // Convertir el arreglo de bytes a una cadena Base64
                string base64String = Convert.ToBase64String(fileBytes);

                var respados = SessionHandler<BackUps>("RespadoRequerido");

                var esCongugue = false;

                switch (tipoRespaldo)
                {
                    case "INFOCRED":
                        respados.Infocred = tipoRespaldo;
                        break;
                    case "ASFI":
                        respados.Asfi = tipoRespaldo;
                        break;
                    case "CINFOCRED":
                        respados.InfocredConyugue = tipoRespaldo;
                        tipoRespaldo = "INFOCRED";
                        esCongugue = true;
                        break;
                    case "CASFI":
                        respados.AsfiConyugue = tipoRespaldo;
                        tipoRespaldo = "ASFI";
                        esCongugue = true;
                        break;
                    case "CAMAI_NAZIR":
                        respados.AmaiNazirConyugue = tipoRespaldo;
                        tipoRespaldo = "AMAI_NAZIR";
                        esCongugue = true;
                        break;
                    case "AMAI_NAZIR":
                        respados.AmaiNazir = tipoRespaldo;
                        break;
                    case "RESPALDO1":
                        respados.Respaldo1 = tipoRespaldo;
                        tipoRespaldo = "RESPALDO";
                        break;
                    case "RESPALDO2":
                        respados.Respaldo2 = tipoRespaldo;
                        tipoRespaldo = "RESPALDO";
                        break;
                    case "RESPALDO3":
                        respados.Respaldo3 = tipoRespaldo;
                        tipoRespaldo = "RESPALDO";
                        break;
                    case "RESPALDO4":
                        respados.Respaldo4 = tipoRespaldo;
                        tipoRespaldo = "RESPALDO";
                        break;
                }

                if (esCongugue == true)
                {
                    requests.Add(new RegisterRespladoRequest
                    {
                        IdCliente = inmueble?.idClienteConyugue ?? 0,
                        TipoRespaldo = tipoRespaldo,
                        Data = base64String,
                        Matricula = usuarioRecuperado?.NombreUsuario + " - " + usuarioRecuperado?.Cargo,
                        NombreArchivo = fileName,
                        TipoArchivo = fileType

                    });
                }
                else
                {
                    requests.Add(new RegisterRespladoRequest
                    {
                        IdCliente = inmueble?.idCliente ?? 0,
                        TipoRespaldo = tipoRespaldo,
                        Data = base64String,
                        Matricula = usuarioRecuperado?.NombreUsuario + " - " + usuarioRecuperado?.Cargo,
                        NombreArchivo = fileName,
                        TipoArchivo = fileType

                    });
                }


                HttpContext.Session.SetString("BackUpArray", JsonConvert.SerializeObject(requests));
                HttpContext.Session.SetString("RespadoRequerido", JsonConvert.SerializeObject(respados));


                HttpContext.Session.SetString("message", "Archivo subido con exito");

                return RedirectToAction("CargarResplado");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode.HasValue ? (int)ex.StatusCode.Value : 500;

                HttpContext.Session.SetString("message", $"error con status: {statusCode}");
                return RedirectToAction("CargarResplado");
            }
        }
        public async Task<IActionResult> SendDate()
        {
            try
            {
                var requests = SessionHandler<List<RegisterRespladoRequest>>("BackUpArray");

                foreach (var request in requests)
                {
                    await Task.Delay(320);
                    var response = _service.PostRegisterBackUp(request);
                }

                HttpContext.Session.SetString("message", "Archivos Enviados");
                return RedirectToAction("SolicitudTable", "Solicitud");
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.Message;

                HttpContext.Session.SetString("message", $"error con status: {statusCode}");
                return RedirectToAction("CargarResplado");
            }


        }

        public T? SessionHandler<T>(string session) where T : class
        {
            var miObjetoJson = HttpContext.Session.GetString(session);
            if (miObjetoJson != null)
            {
                var miObjeto = JsonConvert.DeserializeObject<T>(miObjetoJson);
                return miObjeto;
            }
            return null;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
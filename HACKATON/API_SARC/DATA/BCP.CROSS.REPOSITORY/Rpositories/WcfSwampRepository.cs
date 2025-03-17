using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.WcfSwamp;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.WCFSWAMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class WcfSwampRepository : IWcfSwampRepository
    {
        private readonly WCFSwamp _wcfSwamp;
        private readonly IServiciosSwampRepository _srvSwamp;
        private readonly IRepExtRepository _repExt;
        private readonly ISmartLinkRepository _smartLink;
        private readonly IDevolucionRepository _devolucionRepository;
        private readonly ICobroRepository _cobroRepository;
        public WcfSwampRepository(WCFSwamp wcfSwamp, IServiciosSwampRepository srvSwamp, IRepExtRepository repExt, ISmartLinkRepository smartLink, IDevolucionRepository devolucionRepository, ICobroRepository cobroRepository)
        {
            _wcfSwamp = wcfSwamp;
            _srvSwamp = srvSwamp;
            _repExt = repExt;
            _smartLink=smartLink;
            _devolucionRepository=devolucionRepository;
            _cobroRepository=cobroRepository;
        }

        public async Task<CobroPRResponse> RealizarCobroAsync(CobroPR request)
        {
            CobroPRResponse cobroPRResponse = new CobroPRResponse();
            #region validacion
            request.NroCuenta = request.NroCuenta.Replace("-", "");
            request.Monto = await  validarCuentaMoneda(request.NroCuenta, request.Monto, formatoMoneda(request.Moneda), false);
            request.Moneda = formatoMonedaCuenta(request.NroCuenta);
            #endregion
            bool continuar = true;
            #region cobro
            var responseRepext = await _repExt.GetDatosCuentaAsync(formatoRepExt(request.NroCuenta));
            continuar = (responseRepext != null && responseRepext.Count > 0);
            if (continuar)
            {
                CobroSwamp cobro = new CobroSwamp();
                cobro.Banca = responseRepext[0].TipoBanca;
                cobro.Centro = "000";
                cobro.CIC = responseRepext[0].CIC;
                cobro.Funcionario = request.FuncionarioAtencion;
                cobro.Supervisor = request.Supervisor;
                cobro.NombreCompleto = request.Empresa;
                cobro.ClienteIdc = request.ClienteIdc.PadLeft(8,'0')+request.ClienteIdcTipo+(string.IsNullOrEmpty(request.ClienteIdcExtension)?"":request.ClienteIdcExtension);
                cobro.NroCaso = request.NroCaso;
                cobro.NroCuenta = request.NroCuenta;
                cobro.ClienteNit = request.ClienteIdc;
                cobro.Moneda = request.Moneda;
                cobro.DatosFactura = request.Paterno;
                cobro.Direccion = request.DireccionRespuesta;
                cobro.DescripcionTramite = request.DescripcionServicio;
                cobro.Monto = request.Monto;
                cobro.Correo = request.EmailRespuesta;
                cobro.Telefono=request.TelefonoRespuesta;
                cobro.FacturacionOnline = request.FacturacionOnline;

                var cuentasSW = await _srvSwamp.GetServicioCanalByIdAsync(request.ServiciosCanalesId);
                if (cuentasSW == null)
                {
                    cobroPRResponse.StatusCode = 425;
                    cobroPRResponse.NroOperacion = "NO SE OBTUVO DATOS DE SERVICIOS CANALES";
                    return cobroPRResponse;
                }
                cobro.ServiciosCanalesId = request.ServiciosCanalesId;
                cobro.CanalClave = request.Moneda.Equals("068") ? cuentasSW.IdModeloBOL : cuentasSW.IdModeloUSD;
                cobro.CanalCuenta = cuentasSW.CuentaContable;
                cobro.CanalProducto = cuentasSW.Producto;              

                var cobroSW = await _cobroRepository.CobroByPRAsync(request.ProductoId, request.ServicioId);
                if (cobroSW == null)
                {
                    cobroPRResponse.StatusCode = 425;
                    cobroPRResponse.NroOperacion = "NO SE OBTUVO DATOS DE SW";
                    return cobroPRResponse;
                }
                cobro.FacturacionClave = cobroSW[0].CodigoFacturacion;
                cobro.FacturacionDescripcion = cobroSW[0].DescripcionFacturacion;
                cobro.Glosa = cobroSW[0].Glosa;
                cobro.ID_COBRO = cobroSW[0].IdCobro;
                cobro.DescripcionCobro = cobroSW[0].DescripcionCobro;
                cobro.ProductoId = request.ProductoId;
                cobro.ServicioId = request.ServicioId;
               
                var responseCobro = await _wcfSwamp.CobroSwamp(cobro);
                if (!request.FacturacionOnline && responseCobro.StatusCode==0)
                {
                    responseCobro.Factura=await _wcfSwamp.GeneraFactura(request.ClienteIdc,request.ClienteIdcTipo,request.ClienteIdcExtension,responseCobro.CUF);
                    /*if (string.IsNullOrEmpty(responseCobro.Factura))
                    {
                        responseCobro.StatusCode = 427;
                    }*/
                }
                return responseCobro;
            }
            else
            {
                cobroPRResponse.StatusCode = 425;
                cobroPRResponse.NroOperacion = "NO SE OBTUVO DATOS DE REPEXT";
            }
            #endregion
            return cobroPRResponse;
        }
        public async Task<DevolucionPRResponse> RealizarAbonoAsync(DevolucionPR request)
        {
            DevolucionPRResponse devolucionPRResponse = new DevolucionPRResponse();
            #region validacion
            request.NroCuenta = request.NroCuenta.Replace("-", "");
            request.Monto = await validarCuentaMoneda(request.NroCuenta, request.Monto, formatoMoneda(request.Moneda), true);
            request.Moneda = formatoMonedaCuenta(request.NroCuenta);            
            #endregion
            bool continuar = true;
            #region devolucion
            var responseRepext = await _repExt.GetDatosCuentaAsync(formatoRepExt(request.NroCuenta));
            continuar = (responseRepext != null && responseRepext.Count > 0);
            if (continuar)
            {
                AbonosSwamp abono = new AbonosSwamp();
                abono.Banca = responseRepext[0].TipoBanca;
                abono.Centro = request.ParametroCentro;
                abono.CIC = responseRepext[0].CIC;
                abono.Funcionario = request.FuncionarioAtencion;
                abono.Supervisor = request.Supervisor;
                abono.NombreCompleto = request.Empresa;
                abono.ClienteIdc = request.ClienteIdc.PadLeft(8, '0') + request.ClienteIdcTipo + (string.IsNullOrEmpty(request.ClienteIdcExtension) ? "" : request.ClienteIdcExtension);
                abono.NroCaso = request.NroCaso;
                abono.NroCuenta = request.NroCuenta;
                abono.ClienteNit = request.ClienteIdc;
                abono.Moneda = request.Moneda;
                abono.DatosFactura = request.Paterno;
                abono.Direccion = request.DireccionRespuesta;
                abono.DescripcionTramite = request.DescripcionServicio;
                abono.Monto = request.Monto;


                var cuentasSW = await _srvSwamp.GetServicioCanalByIdAsync(request.ServiciosCanalesId);
                if(cuentasSW == null)
                {
                    devolucionPRResponse.StatusCode = 425;
                    devolucionPRResponse.NroOperacion = "ERROR RESULTADO SERVICIOS CANALES";
                    return devolucionPRResponse;
                }

                abono.ServiciosCanalesId = request.ServiciosCanalesId;
                abono.CanalClave = request.Moneda.Equals("068") ? cuentasSW.IdModeloBOL : cuentasSW.IdModeloUSD;
                abono.CanalCuenta = cuentasSW.CuentaContable;
                abono.CanalProducto = cuentasSW.Producto;


                var devolucionSW = await _devolucionRepository.DevolucionByPRAsync(request.ProductoId, request.ServicioId);
                if(devolucionSW == null)
                {
                    devolucionPRResponse.StatusCode = 425;
                    devolucionPRResponse.NroOperacion = "ERROR RESULTADO SW";
                    return devolucionPRResponse;
                }
                abono.Glosa = devolucionSW[0].Glosa;
                abono.ID_DEVOLUCION = devolucionSW[0].IdDevolucion;
                abono.ProductoId = request.ProductoId;
                abono.ServicioId = request.ServicioId;

                var responseDevolucion = await _wcfSwamp.AbonosSwamp(abono);
                return responseDevolucion;
            }
            else
            {
                devolucionPRResponse.StatusCode = 425;
                devolucionPRResponse.NroOperacion = "ERROR EN CONSULTA A REPEXT";
            }
            #endregion
            return devolucionPRResponse;
        }

        #region VALIDACION
        private async Task<decimal> validarCuentaMoneda(string cuenta,decimal importe,string moneda,bool debAbo)
        {
            decimal monto = 0M;
            if(cuenta[cuenta.Length - 3].Equals('2'))
            {
                if (moneda.Equals("840"))
                {
                    var tipoCambio= await _smartLink.GetTipoCambioAsync();
                    if (debAbo)
                    {
                        monto = importe * tipoCambio.Compra;
                    }
                    else
                    {
                        monto = importe * tipoCambio.Venta;
                    }
                }
                else
                {
                    monto = importe;
                }
            }
            else
            {
                if (moneda.Equals("068"))
                {
                    monto = importe;
                }
                else
                {
                    var tipoCambio = await _smartLink.GetTipoCambioAsync();
                    if (debAbo)
                    {
                        monto = importe * tipoCambio.Compra;
                    }
                    else
                    {
                        monto = importe * tipoCambio.Venta;
                    }
                }
            }

            return monto;
        }
        private string formatoMoneda(string moneda)
        {
            string resp = string.Empty;
            switch (moneda)
            {
                case "BS":
                case "MNA":
                case "BOL":
                case "BOB":
                case "01":
                case "068":
                    resp = "068";
                    break;
                case "DOL":
                case "USD":
                case "SUS":
                case "02":
                case "840":
                    resp = "840";
                    break;
            }

            return resp;
        }
        private string formatoMonedaCuenta(string cuenta)
        {
            return "068";
            if (cuenta[cuenta.Length - 3].Equals('2'))
            {
                return "840";
            }
            else
            {
                return "068";
            }
        }
        private string formatoRepExt(string Cuenta)
        {
            string cuentaRepExt = string.Empty;
            switch (Cuenta.Length)
            {
                case 14:
                    cuentaRepExt = "1030" + Cuenta.Substring(11, 1) +Cuenta.Substring(0, 3) + "0001000000" +Cuenta.Substring(3, 8);
                    break;

                case 13:
                    cuentaRepExt = "1030" + Cuenta.Substring(10, 1) +Cuenta.Substring(0, 3) + "00000000000" +Cuenta.Substring(3, 7);
                    break;

                default:
                    cuentaRepExt = "";
                    break;
            }
            return cuentaRepExt;
        }
        #endregion
    }
}

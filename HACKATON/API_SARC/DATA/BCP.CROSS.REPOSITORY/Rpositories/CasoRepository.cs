using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.DTOs.Area;
using BCP.CROSS.MODELS.DTOs.Asignacion;
using BCP.CROSS.MODELS.DTOs.Caso;
using BCP.CROSS.MODELS.RepExt;
using BCP.CROSS.MODELS.ServiciosSwamp;
using BCP.CROSS.REPORTES;
using BCP.CROSS.REPOSITORY.Contracts;
using BCP.CROSS.SHAREPOINT;
using BCP.CROSS.WCFSWAMP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY
{
    public class CasoRepository : ICasoRepository
    {
        private readonly BD_SARC _sarc_Bd;
        private readonly Sharepoint _sharepoint;
        private readonly Reporte _reporte;
        public CasoRepository(BD_SARC sarc_bd, Sharepoint sharepoint, Reporte reporte)
        {
            _sarc_Bd = sarc_bd;
            _sharepoint = sharepoint;
            _reporte = reporte;
        }

        public async Task<InsertCasoResponse> InsertCaso(Caso casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@FUNC_REG", casoRequest.FuncionarioRegistro));
            parameters.Add(new SqlParameter("@FEC_REG", casoRequest.FechaRegistro));
            parameters.Add(new SqlParameter("@HORA_REG", casoRequest.HoraAsignacion));
            parameters.Add(new SqlParameter("@IDC_CLIE", casoRequest.ClienteIdc));
            parameters.Add(new SqlParameter("@IDC_TIPO", casoRequest.ClienteIdcTipo.ToUpper()));
            parameters.Add(new SqlParameter("@IDC_EXT", casoRequest.ClienteIdcExtension.ToUpper()));
            parameters.Add(new SqlParameter("@ID_PROD", casoRequest.ProductoId));
            parameters.Add(new SqlParameter("@ID_SERV", casoRequest.ServicioId));
            parameters.Add(new SqlParameter("@PATERNO", casoRequest.Paterno.ToUpper()));//null
            parameters.Add(new SqlParameter("@MATERNO", casoRequest.Materno.ToUpper()));//null
            parameters.Add(new SqlParameter("@NOMBRES", casoRequest.Nombres.ToUpper()));//null
            parameters.Add(new SqlParameter("@EMPRESA", casoRequest.Empresa.ToUpper()));
            parameters.Add(new SqlParameter("@SUCURSAL", string.IsNullOrEmpty(casoRequest.Sucursal) ? "204" : casoRequest.Sucursal));
            parameters.Add(new SqlParameter("@AGENCIA", string.IsNullOrEmpty(casoRequest.Agencia) ? "201" : casoRequest.Agencia));
            parameters.Add(new SqlParameter("@DEPARTAMENTO", casoRequest.Departamento));//null
            parameters.Add(new SqlParameter("@CIUDAD", casoRequest.Ciudad));//null            
            parameters.Add(new SqlParameter("@FUNC_ATN", casoRequest.FuncinarioAtencion));
            parameters.Add(new SqlParameter("@FEC_ASIG", casoRequest.FechaAsignacion));
            parameters.Add(new SqlParameter("@HORA_ASIG", casoRequest.HoraAsignacion));
            parameters.Add(new SqlParameter("@ESTADO", "R"));
            parameters.Add(new SqlParameter("@FEC_INI_ATN", casoRequest.FechaIncioAtencion));
            parameters.Add(new SqlParameter("@HORA_INI_ATN", casoRequest.HoraIncioAtension));
            parameters.Add(new SqlParameter("@FEC_FIN_ATN", casoRequest.FechaFinAtencion));
            parameters.Add(new SqlParameter("@HORA_FIN_ATN", casoRequest.HoraFinAtencion));
            parameters.Add(new SqlParameter("@FEC_DEATH_LINE", casoRequest.FechaDeathLine));
            parameters.Add(new SqlParameter("@HORA_DEATH_LINE", casoRequest.HoraDeathLine));
            parameters.Add(new SqlParameter("@NROCTA", casoRequest.NroCuenta));
            parameters.Add(new SqlParameter("@NROTARJETA", casoRequest.NroTarjeta));
            parameters.Add(new SqlParameter("@MONTO", casoRequest.Monto));
            parameters.Add(new SqlParameter("@MONEDA", casoRequest.Moneda));
            parameters.Add(new SqlParameter("@FEC_TXN", casoRequest.FechaTxn));
            parameters.Add(new SqlParameter("@HORA_TXN", casoRequest.HoraTxn));
            parameters.Add(new SqlParameter("@INF_ADICIONAL", casoRequest.InformacionAdicional));
            parameters.Add(new SqlParameter("@ATM_SUC", casoRequest.AtmSucursal));
            parameters.Add(new SqlParameter("@ATM_UBICACION", casoRequest.AtmUbicacion));
            parameters.Add(new SqlParameter("@DOC_ADJ_IN", casoRequest.DocumentosAdjuntoIn));
            parameters.Add(new SqlParameter("@TIPO_SOLUCION", casoRequest.DescripcionSolucion));
            parameters.Add(new SqlParameter("@DESC_SOLUCION", casoRequest.DescripcionSolucion));
            parameters.Add(new SqlParameter("@SW_DESCENTR", casoRequest.SwDescentralizado));
            parameters.Add(new SqlParameter("@AREA_RESP", casoRequest.AreaResponsable));
            parameters.Add(new SqlParameter("@SUC_SOLUCION", casoRequest.SucursalSolucion));
            parameters.Add(new SqlParameter("@DOC_ADJ_OUT", casoRequest.DocumentosAdjuntoOu));
            parameters.Add(new SqlParameter("@VIA_ENVIO_CODIGO", casoRequest.ViaEnvioCodigo));//null
            parameters.Add(new SqlParameter("@VIA_ENVIO", casoRequest.ViaEnvioRespuesta));
            parameters.Add(new SqlParameter("@TIPO_CARTA", casoRequest.TipoCarta));
            parameters.Add(new SqlParameter("@SW_GEN_CARTA", casoRequest.SwGeneraCarta));
            parameters.Add(new SqlParameter("@NRO_CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@SW_RESP_ENVIADA", casoRequest.SwRespuestaEnviada));
            parameters.Add(new SqlParameter("@SW_COM_TEL", casoRequest.SwComunicacionTelefono));
            parameters.Add(new SqlParameter("@SW_COM_EMAIL", casoRequest.SwComunicacionEmail));//null
            parameters.Add(new SqlParameter("@SW_COM_SMS", casoRequest.SwComunicacionSms));//null
            parameters.Add(new SqlParameter("@SW_COM_EN_OFICINA", casoRequest.SwComunicacionEnOficina));//null
            parameters.Add(new SqlParameter("@SW_COM_WHATSAPP", casoRequest.SwComunicacionWhatsapp));//null
            parameters.Add(new SqlParameter("@DIRECCION_RESP", casoRequest.DireccionRespuesta));//null
            parameters.Add(new SqlParameter("@TELEFONO_RESP", casoRequest.TelefonoRespuesta));//null
            parameters.Add(new SqlParameter("@EMAIL_RESP", casoRequest.EmailRespuesta));//null
            parameters.Add(new SqlParameter("@SMS_RESP", casoRequest.SmsRespuesta));//null
            parameters.Add(new SqlParameter("@IMPORTE_DEV", casoRequest.ImporteDevolucion));//null
            parameters.Add(new SqlParameter("@MONEDA_DEV", casoRequest.MonedaDevolucion));//null
            parameters.Add(new SqlParameter("@SW_ERROR", casoRequest.SwErrorRegistro));//null
            parameters.Add(new SqlParameter("@ANT_SERV", casoRequest.ANT_SERV));//null
            parameters.Add(new SqlParameter("@CLAS_OBS", casoRequest.CLAS_OBS));//null
            parameters.Add(new SqlParameter("@AREA_OR", casoRequest.AREA_OR));//null
            parameters.Add(new SqlParameter("@PATERNO_OR", casoRequest.PaternoOr));//null
            parameters.Add(new SqlParameter("@MATERNO_OR", casoRequest.MaternoOr));//null
            parameters.Add(new SqlParameter("@NOMBRES_OR", casoRequest.NombresOr));//null
            parameters.Add(new SqlParameter("@USUARIO_OR", casoRequest.UsuarioOr));//null
            parameters.Add(new SqlParameter("@CANAL", string.IsNullOrEmpty(casoRequest.Canal) ? "2" : casoRequest.Canal));
            parameters.Add(new SqlParameter("@RUTA_SHAREPOINT", string.IsNullOrEmpty(casoRequest.RutaSharePoint) ? _sharepoint.GetRutaSharePoint() : casoRequest.RutaSharePoint));

            var caso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_RECLAMOSInsert_V1", parameters);
            if (caso.Rows.Count > 0)
            {
                var response = new InsertCasoResponse()
                {
                    NroCarta = caso.Rows[0].Field<string>("NRO_CARTA").Trim(),
                    RutaSharePoint = caso.Rows[0].Field<string>("RUTA_SHAREPOINT")
                };
                return response;
            }

            return null;
        }

        public async Task<bool> UpdateSolucionCasoAsync(UpdateCasoSolucionDTO casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@FUNC_MODIFICACION", casoRequest.FuncionarioModificacion));
            parameters.Add(new SqlParameter("@NRO_CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@ESTADO", string.IsNullOrEmpty(casoRequest.Estado) ? "S" : casoRequest.Estado));
            parameters.Add(new SqlParameter("@TIPO_SOLUCION", casoRequest.TipoSolucion));
            parameters.Add(new SqlParameter("@DESC_SOLUCION", casoRequest.DescripcionSolucion));
            parameters.Add(new SqlParameter("@SUC_SOLUCION", string.IsNullOrEmpty(casoRequest.SucursalSolucion) ? "204" : casoRequest.SucursalSolucion));
            parameters.Add(new SqlParameter("@DOC_ADJ_OUT", string.IsNullOrEmpty(casoRequest.DocumentoAdjuntoOut) ? "" : casoRequest.DocumentoAdjuntoOut));
            parameters.Add(new SqlParameter("@TIPO_CARTA", casoRequest.TipoCarta));
            parameters.Add(new SqlParameter("@IMPORTE_DEV", casoRequest.ImporteDevolucion));
            parameters.Add(new SqlParameter("@MONEDA_DEV", casoRequest.MonedaDevolucion));

            return await _sarc_Bd.ExecuteSP_bool("SARC.SP_SARC_RECLAMOSUpdate_SolucionCASO_V1", parameters);
        }

        public async Task<bool> UpdateAsignacionCasoExpressAsync(UpdateAsignacionCasoDTO casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@FUNC_ATENCION", casoRequest.FuncionarioAtension));
            parameters.Add(new SqlParameter("@FUNC_MODIFICACION", casoRequest.FuncionarioModificacion));
            parameters.Add(new SqlParameter("@NRO_CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@ESTADO", casoRequest.Estado));
            parameters.Add(new SqlParameter("@TIEMPO_RESOL", casoRequest.TiempoResolucion));
            parameters.Add(new SqlParameter("@COMPLEJIDAD", casoRequest.Complejidad));

            return await _sarc_Bd.ExecuteSP_bool("SARC.SP_SARC_RECLAMOSUpdate_Asignacion_V1", parameters);
        }

        public async Task<CasoDTO> GetCasoAsync(string nroCaso)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NRO_CARTA", nroCaso));
            var caso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_GetCaso_V1", parameters);

            if (caso.Rows.Count > 0)
            {
                return new CasoDTO {
                    FechaRegistro = caso.Rows[0].Field<string>("FEC_REG") == null ? "" : caso.Rows[0].Field<string>("FEC_REG").Trim(),
                    NroCaso = caso.Rows[0].Field<string>("NRO_CARTA") == null ? "" : caso.Rows[0].Field<string>("NRO_CARTA").Trim(),
                    Apellidos = caso.Rows[0].Field<string>("APELLIDOS") == null ? "" : caso.Rows[0].Field<string>("APELLIDOS").Trim(),
                    Nombres = caso.Rows[0].Field<string>("NOMBRES") == null ? "" : caso.Rows[0].Field<string>("NOMBRES").Trim(),
                    Idc = caso.Rows[0].Field<string>("IDC") == null ? "" : caso.Rows[0].Field<string>("IDC").Trim(),
                    Sucursal = caso.Rows[0].Field<string>("SUCURSAL") == null ? "" : caso.Rows[0].Field<string>("SUCURSAL").Trim(),
                    Agencia = caso.Rows[0].Field<string>("AGENCIA") == null ? "" : caso.Rows[0].Field<string>("AGENCIA").Trim(),
                    Direccion = caso.Rows[0].Field<string>("DIRECCION") == null ? "" : caso.Rows[0].Field<string>("DIRECCION").Trim(),
                    NombreEmpresa = caso.Rows[0].Field<string>("NOMBRE_EMPRESA_REPRESENTA") == null ? "" : caso.Rows[0].Field<string>("NOMBRE_EMPRESA_REPRESENTA").Trim(),
                    ProductoId = caso.Rows[0].Field<string>("ID_PROD") == null ? "" : caso.Rows[0].Field<string>("ID_PROD").Trim(),
                    Producto = caso.Rows[0].Field<string>("PRODUCTO") == null ? "" : caso.Rows[0].Field<string>("PRODUCTO").Trim(),
                    Cuenta = caso.Rows[0].Field<string>("CUENTA") == null ? "" : caso.Rows[0].Field<string>("CUENTA").Trim(),
                    Tarjeta = caso.Rows[0].Field<string>("TARJETA") == null ? "" : caso.Rows[0].Field<string>("TARJETA").Trim(),
                    Monto = caso.Rows[0].Field<decimal>("MONTO"),
                    Moneda = caso.Rows[0].Field<string>("MONEDA") == null ? "" : caso.Rows[0].Field<string>("MONEDA").Trim(),
                    Descripcion = caso.Rows[0].Field<string>("DESCRIPCION") == null ? "" : caso.Rows[0].Field<string>("DESCRIPCION").Trim(),
                    DocumentacionAdjunta = caso.Rows[0].Field<string>("DOCUMENTACION_ADJ") == null ? "" : caso.Rows[0].Field<string>("DOCUMENTACION_ADJ").Trim(),
                    ViaEnvioCodigo = caso.Rows[0].Field<string>("VIA_ENVIO_CODIGO") == null ? "" : caso.Rows[0].Field<string>("VIA_ENVIO_CODIGO").Trim(),
                    ClasificacionCaso = caso.Rows[0].Field<string>("CLASIFICACION_RECLAMO") == null ? "" : caso.Rows[0].Field<string>("CLASIFICACION_RECLAMO").Trim(),
                    AtendidoPor = caso.Rows[0].Field<string>("ATENDIDO_POR") == null ? "" : caso.Rows[0].Field<string>("ATENDIDO_POR").Trim(),
                    AsignadoPor = caso.Rows[0].Field<string>("ASIGNADO_POR") == null ? "" : caso.Rows[0].Field<string>("ASIGNADO_POR").Trim()
                };
            }

            return null;
        }

        public async Task<bool> CerrarCasoAsync(CerrarCasoRequest casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@PRODUCTO", casoRequest.Producto));
            parameters.Add(new SqlParameter("@SERVICIO", casoRequest.Servicio));
            parameters.Add(new SqlParameter("@DOCUMENTO", string.IsNullOrEmpty(casoRequest.Documento) ? "" : casoRequest.Documento));
            parameters.Add(new SqlParameter("@SW", string.IsNullOrEmpty(casoRequest.ErrorReg) ? "" : casoRequest.ErrorReg));
            parameters.Add(new SqlParameter("@TIPERR", string.IsNullOrEmpty(casoRequest.IdErrorReg) ? "" : casoRequest.IdErrorReg));
            parameters.Add(new SqlParameter("@DESC_ERROR", string.IsNullOrEmpty(casoRequest.DescripcionError) ? "" : casoRequest.DescripcionError));
            parameters.Add(new SqlParameter("@NRO_CARTAS_ENV", casoRequest.CartasEnviadas));

            return await _sarc_Bd.ExecuteSP("SARC.SP_CERRAR_CASO", parameters);
        }

        public async Task<bool> UpdateDevolucionCobroAsync(string NroCarta, string NroCuentaPR, int IdServiciosCanales, string centro, decimal Monto, string Moneda, string TipoFacturacion, int IndDevolucionCobro)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NRO_CARTA", NroCarta));
            parameters.Add(new SqlParameter("@CUENTA_PR", NroCuentaPR));
            parameters.Add(new SqlParameter("@ID_SERV_CANALES_PR", IdServiciosCanales));
            parameters.Add(new SqlParameter("@CENTRO_PR", centro));
            parameters.Add(new SqlParameter("@MONTO_PR", Monto));
            parameters.Add(new SqlParameter("@MONEDA_PR", Moneda));
            parameters.Add(new SqlParameter("@TIPO_FACTURACION_PR", TipoFacturacion));
            parameters.Add(new SqlParameter("@DEV_CRED_PR", IndDevolucionCobro));

            return await _sarc_Bd.ExecuteSP("SARC.SP_RECLAMOS_UpdateCobroDevolucion", parameters);
        }

        public async Task<bool> UpdateViaEnvioAsync(UpdateCasoDTOViaEnvio request)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@nro_carta", request.NroCarta));
            parameters.Add(new SqlParameter("@via_envio", request.ViaEnvio));
            parameters.Add(new SqlParameter("@sw_resp_enviada", request.RespuestaEnviada));
            parameters.Add(new SqlParameter("@sw_com_tel", request.ComTelefono));
            parameters.Add(new SqlParameter("@sw_com_email", request.ComEmail));
            parameters.Add(new SqlParameter("@sw_com_sms", request.ComSMS));
            parameters.Add(new SqlParameter("@sw_com_oficina", request.ComOficina));
            parameters.Add(new SqlParameter("@sw_com_whastapp", request.ComWhastapp));
            parameters.Add(new SqlParameter("@direccion_resp", request.Direccion));
            parameters.Add(new SqlParameter("@telefono_resp", request.Telefono));
            parameters.Add(new SqlParameter("@email_resp", request.Email));
            parameters.Add(new SqlParameter("@sms_resp", request.SMS));

            return await _sarc_Bd.ExecuteSP("SARC.SP_UPDATE_VIA_ENVIO_RESPUESTA_V1", parameters);
        }

        public async Task<bool> UpdateSolucionCasoOrigenAsync(UpdateSolucionCasoDTOOrigenRequest casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@TIEMPO", casoRequest.Tiempo));
            parameters.Add(new SqlParameter("@RIESGO", casoRequest.Riesgo));
            parameters.Add(new SqlParameter("@PROCESO", casoRequest.Proceso));

            return await _sarc_Bd.ExecuteSP("SARC.SP_RECLAMOS_UpdateSolucion", parameters);
        }

        public async Task<string> GetReporteRegistroAsync(string NroCarta, string Usuario, string TipoReporte)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NRO_CARTA", NroCarta));
            var listaDevolucionPR = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_REGISTRO", parametros);
            if (listaDevolucionPR.Rows.Count == 1)
            {
                var response = new ReporteRegistroResponse();
                response.Usuario = Usuario;
                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.FechaRegistro = row.Field<string>("FEC_REG").Trim();
                    response.NroCaso = row.Field<string>("NRO_CARTA").Trim();
                    response.Apellidos = row.Field<string>("APELLIDOS").Trim();
                    response.Nombres = row.Field<string>("NOMBRES").Trim();
                    response.ClienteIdc = row.Field<string>("IDC").Trim();
                    response.Sucursal = string.IsNullOrEmpty(row.Field<string>("SUCURSAL")) ? "" : row.Field<string?>("SUCURSAL").Trim();
                    response.Agencia = row.Field<string>("AGENCIA").Trim();
                    response.Direccion = row.Field<string>("DIRECCION").Trim();
                    response.NombreEmpresa = row.Field<string>("NOMBRE_EMPRESA_REPRESENTA").Trim();
                    response.IdProducto = row.Field<string>("ID_PROD").Trim();
                    response.DescripcionProducto = row.Field<string>("PRODUCTO").Trim();
                    response.NroCuenta = row.Field<string>("CUENTA").Trim().PadLeft(14, ' ');
                    response.NroTarjeta = row.Field<string>("TARJETA").Trim().PadLeft(16, ' ');
                    response.Monto = row.Field<decimal>("MONTO").ToString(CultureInfo.InvariantCulture).PadLeft(12, ' ');
                    response.Moneda = row.Field<string>("MONEDA").Trim();
                    response.DescripcionCaso = row.Field<string>("DESCRIPCION").Trim();
                    response.DocumentacionAdjunta = row.Field<string>("DOCUMENTACION_ADJ").Trim();
                    response.ClasificacionReclamo = row.Field<string>("CLASIFICACION_RECLAMO").Trim();
                    response.AreaAtencionReclamo = row.Field<string>("ATENDIDO_POR").Trim();
                    response.FuncionarioAtencionReclamo = row.Field<string>("ASIGNADO_POR").Trim();
                }
                if (response.ClienteIdc.Contains("XX"))
                {
                    response.ClienteIdc = response.ClienteIdc.Replace("XX", "&ensp;&ensp;");
                }
                return await _reporte.GetReporteAsync(response, TipoReporte);
            }
            return null;
        }

        public async Task<bool> UpdateCasoGrabarOrigenAsync(UpdateOrigenCasoDTORequest casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@AREA", casoRequest.Area));
            parameters.Add(new SqlParameter("@NOMBRE", casoRequest.Nombre));
            parameters.Add(new SqlParameter("@APP", casoRequest.Paterno));
            parameters.Add(new SqlParameter("@APM", casoRequest.Materno));
            parameters.Add(new SqlParameter("@MATRICULA", casoRequest.Matricula));

            return await _sarc_Bd.ExecuteSP("SARC.SP_RECLAMOS_UpdateOrigen", parameters);
        }

        public async Task<CasoDTOCliente> GetCasoAllAsync(string nroCaso)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NRO_CARTA", nroCaso));
            var caso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_RECLAMOSSelectByNRO_CARTA_V2", parameters);

            if (caso.Rows.Count == 1)
            {
                CasoDTOCliente response = new CasoDTOCliente();
                response.FuncionarioRegistro = caso.Rows[0].Field<string>("FUNC_REG") == null ? "" : caso.Rows[0].Field<string>("FUNC_REG").Trim();
                response.FechaRegistro = caso.Rows[0].Field<string>("FEC_REG") == null ? "" : caso.Rows[0].Field<string>("FEC_REG").Trim();
                response.HoraRegistro = caso.Rows[0].Field<string>("HORA_REG") == null ? "" : caso.Rows[0].Field<string>("HORA_REG").Trim();
                response.ApellidoPaterno = caso.Rows[0].Field<string>("PATERNO") == null ? "" : caso.Rows[0].Field<string>("PATERNO").Trim();
                response.ApellidoMaterno = caso.Rows[0].Field<string>("MATERNO") == null ? "" : caso.Rows[0].Field<string>("MATERNO").Trim();
                response.Nombres = caso.Rows[0].Field<string>("NOMBRES") == null ? "" : caso.Rows[0].Field<string>("NOMBRES").Trim();
                response.Producto = caso.Rows[0].Field<string>("PRODUCTO") == null ? "" : caso.Rows[0].Field<string>("PRODUCTO").Trim();
                response.ProductoId = caso.Rows[0].Field<string>("ID_PROD") == null ? "" : caso.Rows[0].Field<string>("ID_PROD").Trim();
                response.Servicio = caso.Rows[0].Field<string>("SERVICIO") == null ? "" : caso.Rows[0].Field<string>("SERVICIO").Trim();
                response.ServicioId = caso.Rows[0].Field<string>("ID_SERV") == null ? "" : caso.Rows[0].Field<string>("ID_SERV").Trim();
                response.Empresa = caso.Rows[0].Field<string>("EMPRESA") == null ? "" : caso.Rows[0].Field<string>("EMPRESA").Trim();
                response.Sucursal = caso.Rows[0].Field<string>("SUCURSAL") == null ? "" : caso.Rows[0].Field<string>("SUCURSAL").Trim();
                response.Departamento = caso.Rows[0].Field<string>("DEPARTAMENTO") == null ? "" : caso.Rows[0].Field<string>("DEPARTAMENTO").Trim();
                response.Ciudad = caso.Rows[0].Field<string>("CIUDAD") == null ? "" : caso.Rows[0].Field<string>("CIUDAD").Trim();
                response.Agencia = caso.Rows[0].Field<string>("AGENCIA") == null ? "" : caso.Rows[0].Field<string>("AGENCIA").Trim();
                response.FuncionarioAtencion = caso.Rows[0].Field<string>("FUNC_ATN") == null ? "" : caso.Rows[0].Field<string>("FUNC_ATN").Trim();
                response.FechaAsignacion = caso.Rows[0].Field<string>("FEC_ASIG") == null ? "" : caso.Rows[0].Field<string>("FEC_ASIG").Trim();
                response.HoraAsignacion = caso.Rows[0].Field<string>("HORA_ASIG") == null ? "" : caso.Rows[0].Field<string>("HORA_ASIG").Trim();
                response.Estado = caso.Rows[0].Field<string>("ESTADO") == null ? "" : caso.Rows[0].Field<string>("ESTADO").Trim();
                response.EstadoCaso = caso.Rows[0].Field<string>("ESTADO_CASO") == null ? "" : caso.Rows[0].Field<string>("ESTADO_CASO").Trim();
                response.FechaInicioAtencion = caso.Rows[0].Field<string>("FEC_INI_ATN") == null ? "" : caso.Rows[0].Field<string>("FEC_INI_ATN").Trim();
                response.HoraInicioAtencion = caso.Rows[0].Field<string>("HORA_INI_ATN") == null ? "" : caso.Rows[0].Field<string>("HORA_INI_ATN").Trim();
                response.FechaFinAtencion = caso.Rows[0].Field<string>("FEC_FIN_ATN") == null ? "" : caso.Rows[0].Field<string>("FEC_FIN_ATN").Trim();
                response.HoraFinAtencion = caso.Rows[0].Field<string>("HORA_FIN_ATN") == null ? "" : caso.Rows[0].Field<string>("HORA_FIN_ATN").Trim();
                response.FechaDeathLine = caso.Rows[0].Field<string>("FEC_DEATH_LINE") == null ? "" : caso.Rows[0].Field<string>("FEC_DEATH_LINE").Trim();
                response.HoraDeathLine = caso.Rows[0].Field<string>("HORA_DEATH_LINE") == null ? "" : caso.Rows[0].Field<string>("HORA_DEATH_LINE").Trim();
                response.Cuenta = caso.Rows[0].Field<string>("NROCTA") == null ? "" : caso.Rows[0].Field<string>("NROCTA").Trim();
                response.Tarjeta = caso.Rows[0].Field<string>("NROTARJETA") == null ? "" : caso.Rows[0].Field<string>("NROTARJETA").Trim();
                response.Monto = caso.Rows[0].Field<decimal>("MONTO");
                response.Moneda = caso.Rows[0].Field<string>("MONEDA") == null ? "" : caso.Rows[0].Field<string>("MONEDA").Trim();
                response.FechaTxn = caso.Rows[0].Field<string>("FEC_TXN") == null ? "" : caso.Rows[0].Field<string>("FEC_TXN").Trim();
                response.HoraTxn = caso.Rows[0].Field<string>("HORA_TXN") == null ? "" : caso.Rows[0].Field<string>("HORA_TXN").Trim();
                response.InformacionAdicional = caso.Rows[0].Field<string>("INF_ADICIONAL") == null ? "" : caso.Rows[0].Field<string>("INF_ADICIONAL").Trim();
                response.ATMSucursal = caso.Rows[0].Field<string>("ATM_SUC") == null ? "" : caso.Rows[0].Field<string>("ATM_SUC").Trim();
                response.ATMUbicacion = caso.Rows[0].Field<string>("ATM_UBICACION") == null ? "" : caso.Rows[0].Field<string>("ATM_UBICACION").Trim();
                response.DocumentacionAdjuntaEntrada = caso.Rows[0].Field<string>("DOC_ADJ_IN") == null ? "" : caso.Rows[0].Field<string>("DOC_ADJ_IN").Trim();
                response.TipoSolucion = caso.Rows[0].Field<string>("DESC_TIPO_SOL") == null ? "" : caso.Rows[0].Field<string>("DESC_TIPO_SOL").Trim();
                response.DescripcionSolucion = caso.Rows[0].Field<string>("DESC_SOLUCION") == null ? "" : caso.Rows[0].Field<string>("DESC_SOLUCION").Trim();
                response.SWDescCenter = caso.Rows[0].Field<string>("SW_DESCENTR") == null ? "" : caso.Rows[0].Field<string>("SW_DESCENTR").Trim();
                response.AreaRespuesta = caso.Rows[0].Field<string>("AREA_RESP") == null ? "" : caso.Rows[0].Field<string>("AREA_RESP").Trim();
                response.SucursalSolucion = caso.Rows[0].Field<string>("SUC_SOLUCION") == null ? "" : caso.Rows[0].Field<string>("SUC_SOLUCION").Trim();
                response.DocumentacionAdjuntaSalida = caso.Rows[0].Field<string>("DOC_ADJ_OUT") == null ? "" : caso.Rows[0].Field<string>("DOC_ADJ_OUT").Trim();
                response.ViaEnvioRespuesta = caso.Rows[0].Field<string>("VIA_ENVIO") == null ? "" : caso.Rows[0].Field<string>("VIA_ENVIO").Trim();
                response.ViaEnvioCodigo = caso.Rows[0].Field<string>("VIA_CODIGO") == null ? "" : caso.Rows[0].Field<string>("VIA_CODIGO").Trim();
                response.TipoCarta = caso.Rows[0].Field<string>("TIPO_CARTA") == null ? "" : caso.Rows[0].Field<string>("TIPO_CARTA").Trim();
                response.SWGeneracionCarta = caso.Rows[0].Field<string>("SW_GEN_CARTA") == null ? "" : caso.Rows[0].Field<string>("SW_GEN_CARTA").Trim();
                response.NroCaso = caso.Rows[0].Field<string>("NRO_CARTA") == null ? "" : caso.Rows[0].Field<string>("NRO_CARTA").Trim();
                response.SWRespuestaEnviada = caso.Rows[0].Field<string>("SW_RESP_ENVIADA") == null ? "" : caso.Rows[0].Field<string>("SW_RESP_ENVIADA").Trim();
                response.SWComEmail = caso.Rows[0].Field<string>("SW_COM_EMAIL") == null ? "" : caso.Rows[0].Field<string>("SW_COM_EMAIL").Trim();
                response.SWComSMS = caso.Rows[0].Field<string>("SW_COM_SMS") == null ? "" : caso.Rows[0].Field<string>("SW_COM_SMS").Trim();
                response.SWComTel = caso.Rows[0].Field<string>("SW_COM_TEL") == null ? "" : caso.Rows[0].Field<string>("SW_COM_TEL").Trim();
                response.DireccionRespuesta = caso.Rows[0].Field<string>("DIRECCION_RESP") == null ? "" : caso.Rows[0].Field<string>("DIRECCION_RESP").Trim();
                response.EmailRespuesta = caso.Rows[0].Field<string>("EMAIL_RESP") == null ? "" : caso.Rows[0].Field<string>("EMAIL_RESP").Trim();
                response.SMSRespuesta = caso.Rows[0].Field<string>("SMS_RESP") == null ? "" : caso.Rows[0].Field<string>("SMS_RESP").Trim();
                response.TelefonoRespuesta = caso.Rows[0].Field<string>("TELEFONO_RESP") == null ? "" : caso.Rows[0].Field<string>("TELEFONO_RESP").Trim();
                response.AntServ = caso.Rows[0].Field<string>("ANT_SERV") == null ? "" : caso.Rows[0].Field<string>("ANT_SERV").Trim();
                response.ClasObs = caso.Rows[0].Field<string>("CLAS_OBS") == null ? "" : caso.Rows[0].Field<string>("CLAS_OBS").Trim();
                response.AreaRegistro = caso.Rows[0].Field<string>("AREA_REG") == null ? "" : caso.Rows[0].Field<string>("AREA_REG").Trim();
                response.AreaOrigen = caso.Rows[0].Field<string>("AREA_OR") == null ? "" : caso.Rows[0].Field<string>("AREA_OR").Trim();
                response.PaternoOrigen = caso.Rows[0].Field<string>("PATERNO_OR") == null ? "" : caso.Rows[0].Field<string>("PATERNO_OR").Trim();
                response.MaternoOrigen = caso.Rows[0].Field<string>("MATERNO_OR") == null ? "" : caso.Rows[0].Field<string>("MATERNO_OR").Trim();
                response.NombresOrigen = caso.Rows[0].Field<string>("NOMBRES_OR") == null ? "" : caso.Rows[0].Field<string>("NOMBRES_OR").Trim();
                response.UsuarioOrigen = caso.Rows[0].Field<string>("USUARIO_OR") == null ? "" : caso.Rows[0].Field<string>("USUARIO_OR").Trim();
                response.RutaSharepoint = caso.Rows[0].Field<string>("RUTA_SHAREPOINT") == null ? "" : caso.Rows[0].Field<string>("RUTA_SHAREPOINT").Trim();
                response.TipoServicio = caso.Rows[0].Field<string>("TIPO") == null ? "" : caso.Rows[0].Field<string>("TIPO").Trim();
                //Idc = caso.Rows[0].Field<string>("IDC") == null ? "" : caso.Rows[0].Field<string>("IDC").Trim();
                response.IdRegistroError = caso.Rows[0].Field<string>("ID_ERROR_REG") == null ? "" : caso.Rows[0].Field<string>("ID_ERROR_REG").Trim();
                response.DescripcionRegistroError = caso.Rows[0].Field<string>("DESC_ERROR_REG") == null ? "" : caso.Rows[0].Field<string>("DESC_ERROR_REG").Trim();
                response.IdRegistroErrorSolucion = caso.Rows[0].Field<string>("ID_ERROR_SOL") == null ? "" : caso.Rows[0].Field<string>("ID_ERROR_REG").Trim();
                response.DescripcionRegistroErrorSolucion = caso.Rows[0].Field<string>("DESC_ERROR_SOL") == null ? "" : caso.Rows[0].Field<string>("DESC_ERROR_SOL").Trim();
                response.ParametroRiesgo = caso.Rows[0].Field<int?>("RIESGO")??0;
                response.ParametroProceso = caso.Rows[0].Field<int?>("TIEMPO")??0;
                response.ParametroTiempo = caso.Rows[0].Field<int?>("PROCESO")??0;
                string idcNumero = caso.Rows[0].Field<string>("IDC_CLIE") == null ? "" : caso.Rows[0].Field<string>("IDC_CLIE").Trim();
                string idcTipo = caso.Rows[0].Field<string>("IDC_TIPO") == null ? "" : caso.Rows[0].Field<string>("IDC_TIPO").Trim();
                string idcExtension = caso.Rows[0].Field<string>("IDC_EXT") == null ? "" : caso.Rows[0].Field<string>("IDC_EXT").Trim();
                response.Idc = idcNumero + idcTipo + idcExtension;
                response.ClienteIdc = idcNumero;
                response.ClienteTipo = idcTipo;
                response.ClienteExtension = idcExtension;
                try
                {
                    response.ImporteDev = caso.Rows[0].Field<decimal>("IMPORTE_DEV");
                    response.MonedaDev = caso.Rows[0].Field<string>("MONEDA_DEV").Trim();
                }
                catch (System.Exception)
                {
                    response.ImporteDev = 0M;
                    response.MonedaDev = "";
                }
                return response;
            }

            return null;
        }

        public async Task<CasoDTOCompleto> GetCasoCompletoAsync(string nroCaso)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NRO_CARTA", nroCaso));
            var caso = await _sarc_Bd.ExecuteSP_DataTable("SARC.RECLAMOS_ListDevolucion", parameters);

            if (caso.Rows.Count == 1)
            {
                CasoDTOCompleto response = new CasoDTOCompleto();
                response.FuncionarioRegistro = caso.Rows[0].Field<string>("FUNC_REG") == null ? "" : caso.Rows[0].Field<string>("FUNC_REG").Trim();
                    response.FechaRegistro = caso.Rows[0].Field<string>("FEC_REG") == null ? "" : caso.Rows[0].Field<string>("FEC_REG").Trim();
                    response.HoraRegistro = caso.Rows[0].Field<string>("HORA_REG") == null ? "" : caso.Rows[0].Field<string>("HORA_REG").Trim();
                    response.ApellidoPaterno = caso.Rows[0].Field<string>("PATERNO") == null ? "" : caso.Rows[0].Field<string>("PATERNO").Trim();
                    response.ApellidoMaterno = caso.Rows[0].Field<string>("MATERNO") == null ? "" : caso.Rows[0].Field<string>("MATERNO").Trim();
                    response.Nombres = caso.Rows[0].Field<string>("NOMBRES") == null ? "" : caso.Rows[0].Field<string>("NOMBRES").Trim();
                    response.ProductoId = caso.Rows[0].Field<string>("ID_PROD") == null ? "" : caso.Rows[0].Field<string>("ID_PROD").Trim();
                    response.ServicioId = caso.Rows[0].Field<string>("ID_SERV") == null ? "" : caso.Rows[0].Field<string>("ID_SERV").Trim();
                    response.Empresa = caso.Rows[0].Field<string>("EMPRESA") == null ? "" : caso.Rows[0].Field<string>("EMPRESA").Trim();
                    response.Sucursal = caso.Rows[0].Field<string>("SUCURSAL") == null ? "" : caso.Rows[0].Field<string>("SUCURSAL").Trim();
                    response.Departamento = caso.Rows[0].Field<string>("DEPARTAMENTO") == null ? "" : caso.Rows[0].Field<string>("DEPARTAMENTO").Trim();
                    response.Ciudad = caso.Rows[0].Field<string>("CIUDAD") == null ? "" : caso.Rows[0].Field<string>("CIUDAD").Trim();
                    response.Agencia = caso.Rows[0].Field<string>("AGENCIA") == null ? "" : caso.Rows[0].Field<string>("AGENCIA").Trim();
                    response.FuncionarioAtencion = caso.Rows[0].Field<string>("FUNC_ATN") == null ? "" : caso.Rows[0].Field<string>("FUNC_ATN").Trim();
                    response.FechaAsignacion = caso.Rows[0].Field<string>("FEC_ASIG") == null ? "" : caso.Rows[0].Field<string>("FEC_ASIG").Trim();
                    response.HoraAsignacion = caso.Rows[0].Field<string>("HORA_ASIG") == null ? "" : caso.Rows[0].Field<string>("HORA_ASIG").Trim();
                    response.Estado = caso.Rows[0].Field<string>("ESTADO") == null ? "" : caso.Rows[0].Field<string>("ESTADO").Trim();
                response.FechaInicioAtencion = caso.Rows[0].Field<string>("FEC_INI_ATN") == null ? "" : caso.Rows[0].Field<string>("FEC_INI_ATN").Trim();
                    response.HoraInicioAtencion = caso.Rows[0].Field<string>("HORA_INI_ATN") == null ? "" : caso.Rows[0].Field<string>("HORA_INI_ATN").Trim();
                    response.FechaFinAtencion = caso.Rows[0].Field<string>("FEC_FIN_ATN") == null ? "" : caso.Rows[0].Field<string>("FEC_FIN_ATN").Trim();
                    response.HoraFinAtencion = caso.Rows[0].Field<string>("HORA_FIN_ATN") == null ? "" : caso.Rows[0].Field<string>("HORA_FIN_ATN").Trim();
                    response.FechaDeathLine = caso.Rows[0].Field<string>("FEC_DEATH_LINE") == null ? "" : caso.Rows[0].Field<string>("FEC_DEATH_LINE").Trim();
                    response.HoraDeathLine = caso.Rows[0].Field<string>("HORA_DEATH_LINE") == null ? "" : caso.Rows[0].Field<string>("HORA_DEATH_LINE").Trim();
                    response.Cuenta = caso.Rows[0].Field<string>("NROCTA") == null ? "" : caso.Rows[0].Field<string>("NROCTA").Trim();
                response.Tarjeta = caso.Rows[0].Field<string>("NROTARJETA") == null ? "" : caso.Rows[0].Field<string>("NROTARJETA").Trim();
                    response.Monto = caso.Rows[0].Field<decimal>("MONTO");
                    response.Moneda = caso.Rows[0].Field<string>("MONEDA") == null ? "" : caso.Rows[0].Field<string>("MONEDA").Trim();
                    response.FechaTxn = caso.Rows[0].Field<string>("FEC_TXN") == null ? "" : caso.Rows[0].Field<string>("FEC_TXN").Trim();
                   response.HoraTxn = caso.Rows[0].Field<string>("HORA_TXN") == null ? "" : caso.Rows[0].Field<string>("HORA_TXN").Trim();
                    response.InformacionAdicional = caso.Rows[0].Field<string>("INF_ADICIONAL") == null ? "" : caso.Rows[0].Field<string>("INF_ADICIONAL").Trim();
                    response.ATMSucursal = caso.Rows[0].Field<string>("ATM_SUC") == null ? "" : caso.Rows[0].Field<string>("ATM_SUC").Trim();
                    response.ATMUbicacion = caso.Rows[0].Field<string>("ATM_UBICACION") == null ? "" : caso.Rows[0].Field<string>("ATM_UBICACION").Trim();
                    response.DocumentacionAdjuntaEntrada = caso.Rows[0].Field<string>("DOC_ADJ_IN") == null ? "" : caso.Rows[0].Field<string>("DOC_ADJ_IN").Trim();
                    response.TipoSolucion = caso.Rows[0].Field<string>("TIPO_SOLUCION") == null ? "" : caso.Rows[0].Field<string>("TIPO_SOLUCION").Trim();
                    response.DescripcionSolucion = caso.Rows[0].Field<string>("DESC_SOLUCION") == null ? "" : caso.Rows[0].Field<string>("DESC_SOLUCION").Trim();
                    response.SWErrorSolucion = caso.Rows[0].Field<string>("SW_ERROR_SOL") == null ? "" : caso.Rows[0].Field<string>("SW_ERROR_SOL").Trim();
                    response.IdRegistroErrorSolucion = caso.Rows[0].Field<string>("ID_ERROR_SOL") == null ? "" : caso.Rows[0].Field<string>("ID_ERROR_SOL").Trim();
                    response.DescripcionRegistroErrorSolucion = caso.Rows[0].Field<string>("DESC_ERROR_SOL") == null ? "" : caso.Rows[0].Field<string>("DESC_ERROR_SOL").Trim();
                    response.FechaProrroga = caso.Rows[0].Field<string>("FEC_PRORROGA") == null ? "" : caso.Rows[0].Field<string>("FEC_PRORROGA").Trim();
                    response.SWDescCenter = caso.Rows[0].Field<string>("SW_DESCENTR") == null ? "" : caso.Rows[0].Field<string>("SW_DESCENTR").Trim();
                    response.AreaRespuesta = caso.Rows[0].Field<string>("AREA_RESP") == null ? "" : caso.Rows[0].Field<string>("AREA_RESP").Trim();
                    response.SucursalSolucion = caso.Rows[0].Field<string>("SUC_SOLUCION") == null ? "" : caso.Rows[0].Field<string>("SUC_SOLUCION").Trim();
                    response.DocumentacionAdjuntaSalida = caso.Rows[0].Field<string>("DOC_ADJ_OUT") == null ? "" : caso.Rows[0].Field<string>("DOC_ADJ_OUT").Trim();
                    response.ViaEnvioRespuesta = caso.Rows[0].Field<string>("VIA_ENVIO") == null ? "" : caso.Rows[0].Field<string>("VIA_ENVIO").Trim();
                    response.TipoCarta = caso.Rows[0].Field<string>("TIPO_CARTA") == null ? "" : caso.Rows[0].Field<string>("TIPO_CARTA").Trim();
                    response.SWGeneracionCarta = caso.Rows[0].Field<string>("SW_GEN_CARTA") == null ? "" : caso.Rows[0].Field<string>("SW_GEN_CARTA").Trim();
                    response.NroCaso = caso.Rows[0].Field<string>("NRO_CARTA") == null ? "" : caso.Rows[0].Field<string>("NRO_CARTA").Trim();
                    response.SWRespuestaEnviada = caso.Rows[0].Field<string>("SW_RESP_ENVIADA") == null ? "" : caso.Rows[0].Field<string>("SW_RESP_ENVIADA").Trim();
                    response.SWComEmail = caso.Rows[0].Field<string>("SW_COM_EMAIL") == null ? "" : caso.Rows[0].Field<string>("SW_COM_EMAIL").Trim();
                    response.SWComSMS = caso.Rows[0].Field<string>("SW_COM_SMS") == null ? "" : caso.Rows[0].Field<string>("SW_COM_SMS").Trim();
                    response.SWComTel = caso.Rows[0].Field<string>("SW_COM_TEL") == null ? "" : caso.Rows[0].Field<string>("SW_COM_TEL").Trim();
                    response.DireccionRespuesta = caso.Rows[0].Field<string>("DIRECCION_RESP") == null ? "" : caso.Rows[0].Field<string>("DIRECCION_RESP").Trim();
                    response.EmailRespuesta = caso.Rows[0].Field<string>("EMAIL_RESP") == null ? "" : caso.Rows[0].Field<string>("EMAIL_RESP").Trim();
                    response.SMSRespuesta = caso.Rows[0].Field<string>("SMS_RESP") == null ? "" : caso.Rows[0].Field<string>("SMS_RESP").Trim();
                    response.TelefonoRespuesta = caso.Rows[0].Field<string>("TELEFONO_RESP") == null ? "" : caso.Rows[0].Field<string>("TELEFONO_RESP").Trim();
                    response.SWErrorReg = caso.Rows[0].Field<string>("SW_ERROR_REG") == null ? "" : caso.Rows[0].Field<string>("SW_ERROR_REG").Trim();
                    response.IdRegistroError = caso.Rows[0].Field<string>("ID_ERROR_REG") == null ? "" : caso.Rows[0].Field<string>("ID_ERROR_REG").Trim();
                    response.DescripcionRegistroError = caso.Rows[0].Field<string>("DESC_ERROR_REG") == null ? "" : caso.Rows[0].Field<string>("DESC_ERROR_REG").Trim();
                    response.AntServ = caso.Rows[0].Field<string>("ANT_SERV") == null ? "" : caso.Rows[0].Field<string>("ANT_SERV").Trim();
                    response.ClasObs = caso.Rows[0].Field<string>("CLAS_OBS") == null ? "" : caso.Rows[0].Field<string>("CLAS_OBS").Trim();
                    response.AreaOrigen = caso.Rows[0].Field<string>("AREA_OR") == null ? "" : caso.Rows[0].Field<string>("AREA_OR").Trim();
                    response.PaternoOrigen = caso.Rows[0].Field<string>("PATERNO_OR") == null ? "" : caso.Rows[0].Field<string>("PATERNO_OR").Trim();
                    response.MaternoOrigen = caso.Rows[0].Field<string>("MATERNO_OR") == null ? "" : caso.Rows[0].Field<string>("MATERNO_OR").Trim();
                    response.NombresOrigen = caso.Rows[0].Field<string>("NOMBRES_OR") == null ? "" : caso.Rows[0].Field<string>("NOMBRES_OR").Trim();
                    response.UsuarioOrigen = caso.Rows[0].Field<string>("USUARIO_OR") == null ? "" : caso.Rows[0].Field<string>("USUARIO_OR").Trim();
                    response.RutaSharepoint = caso.Rows[0].Field<string>("RUTA_SHAREPOINT") == null ? "" : caso.Rows[0].Field<string>("RUTA_SHAREPOINT").Trim();
                    //Idc = caso.Rows[0].Field<string>("IDC") == null ? "" : caso.Rows[0].Field<string>("IDC").Trim(),
                    response.ParametroRiesgo = caso.Rows[0].Field<int?>("RIESGO")??0;
                    response.ParametroProceso = caso.Rows[0].Field<int?>("TIEMPO")??0;
                    response.ParametroTiempo = caso.Rows[0].Field<int?>("PROCESO")??0;
                    response.Canal = caso.Rows[0].Field<string>("CANAL") == null ? "" : caso.Rows[0].Field<string>("CANAL").Trim();
                    response.IdServAnt = caso.Rows[0].Field<string>("ID_SERV_ANT") == null ? "" : caso.Rows[0].Field<string>("ID_SERV_ANT").Trim();
                    response.Complejidad = caso.Rows[0].Field<string>("COMPLEJIDAD") == null ? "" : caso.Rows[0].Field<string>("COMPLEJIDAD").Trim();
                    response.NumeroCartasEnviadas = caso.Rows[0].Field<int?>("NRO_CARTAS_ENV") ?? 0;
                    response.FechaCierre = caso.Rows[0].Field<DateTime?>("FEC_CIERRE") ?? DateTime.MaxValue;
                    response.CuentaSW = caso.Rows[0].Field<string>("CUENTA_SW") == null ? "" : caso.Rows[0].Field<string>("CUENTA_SW").Trim();
                    response.MontoSW = caso.Rows[0].Field<decimal?>("MONTO_SW")??0M;
                    response.MonedaSW = caso.Rows[0].Field<string>("MONEDA_SW") == null ? "" : caso.Rows[0].Field<string>("MONEDA_SW").Trim();
                    response.IdServiciosCanalesSW = caso.Rows[0].Field<int?>("ID_SERV_CANALES_SW") ?? 0;
                    response.TipoFacturacionSW = caso.Rows[0].Field<string>("TIPO_FACTURACION_SW") == null ? "" : caso.Rows[0].Field<string>("TIPO_FACTURACION_SW").Trim();
                    response.DevCredSW = caso.Rows[0].Field<int?>("DEV_CRED_SW") ?? 0;
                    response.CuentaPR = caso.Rows[0].Field<string>("CUENTA_PR") == null ? "" : caso.Rows[0].Field<string>("CUENTA_PR").Trim();
                    response.MontoPR = caso.Rows[0].Field<decimal?>("MONTO_PR") ?? 0M;
                    response.MonedaPR = caso.Rows[0].Field<string>("MONEDA_PR") == null ? "" : caso.Rows[0].Field<string>("MONEDA_PR").Trim();
                    response.IdServiciosCanalesPR = caso.Rows[0].Field<int?>("ID_SERV_CANALES_PR") ?? 0;
                    response.TipoFacturacionPR = caso.Rows[0].Field<string>("TIPO_FACTURACION_PR") == null ? "" : caso.Rows[0].Field<string>("TIPO_FACTURACION_PR").Trim();
                    response.DevCredPR = caso.Rows[0].Field<int?>("DEV_CRED_PR") ?? 0;
                response.CentroCostoPR = caso.Rows[0].Field<string>("CENTRO_PR") == null ? "" : caso.Rows[0].Field<string>("CENTRO_PR").Trim();
                string idcNumero = caso.Rows[0].Field<string>("IDC_CLIE") == null ? "" : caso.Rows[0].Field<string>("IDC_CLIE").Trim();
                string idcTipo = caso.Rows[0].Field<string>("IDC_TIPO") == null ? "" : caso.Rows[0].Field<string>("IDC_TIPO").Trim();
                string idcExtension = caso.Rows[0].Field<string>("IDC_EXT") == null ? "" : caso.Rows[0].Field<string>("IDC_EXT").Trim();
                response.Idc = idcNumero + idcTipo + idcExtension;
                response.ClienteIdc = idcNumero;
                response.ClienteTipo = idcTipo;
                response.ClienteExtension = idcExtension;
                try
                {
                    response.ImporteDev = caso.Rows[0].Field<decimal>("IMPORTE_DEV");
                    response.MonedaDev = caso.Rows[0].Field<string>("MONEDA_DEV").Trim();
                }
                catch (System.Exception)
                {
                    response.ImporteDev = 0M;
                    response.MonedaDev = "";
                }
                return response;
            }

            return null;
        }

        public async Task<List<CasoDTOBaseAtencion>> GetCasoAtencionByEstadoAsync(string estado)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ESTADO", estado));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_RECLAMOSelectAllByEstado", parameters);
            return GetRespuestaCasoAtencion(casos);
        }
        public async Task<List<CasoDTOBaseAtencion>> GetCasoAtencionByEstadoUsuarioAsync(string estado,string funcionario)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ESTADO", estado));
            parameters.Add(new SqlParameter("@FUNC_ATN", funcionario));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_RECLAMOSelectAllByEstadoUsuario", parameters);
            return GetRespuestaCasoAtencion(casos);
        }

        

        private List<CasoDTOBaseAtencion> GetRespuestaCasoAtencion(DataTable casos)
        {
            if (casos.Rows.Count > 0)
            {
                var response = new List<CasoDTOBaseAtencion>();
                foreach (DataRow caso in casos.Rows)
                {
                    response.Add(new CasoDTOBaseAtencion
                    {
                        FuncionarioRegistro = caso.Field<string>("FUNC_REG") == null ? "" : caso.Field<string>("FUNC_REG").Trim(),
                        FechaRegistro = caso.Field<string>("FEC_REG") == null ? "" : caso.Field<string>("FEC_REG").Trim(),
                        HoraRegistro = caso.Field<string>("HORA_REG") == null ? "" : caso.Field<string>("HORA_REG").Trim(),
                        ClienteIdc = caso.Field<string>("IDC_CLIE") == null ? "" : caso.Field<string>("IDC_CLIE").Trim(),
                        ClienteTipo = caso.Field<string>("IDC_TIPO") == null ? "" : caso.Field<string>("IDC_TIPO").Trim(),
                        ClienteExtension = caso.Field<string>("IDC_EXT") == null ? "" : caso.Field<string>("IDC_EXT").Trim(),
                        Idc= caso.Field<string>("IDC_CLIENTE") == null ? "" : caso.Field<string>("IDC_CLIENTE").Trim(),
                        Producto = caso.Field<string>("PRODUCTO") == null ? "" : caso.Field<string>("PRODUCTO").Trim(),
                        ProductoId = caso.Field<string>("ID_PROD") == null ? "" : caso.Field<string>("ID_PROD").Trim(),
                        Servicio = caso.Field<string>("SERVICIO") == null ? "" : caso.Field<string>("SERVICIO").Trim(),
                        ServicioId = caso.Field<string>("ID_SERV") == null ? "" : caso.Field<string>("ID_SERV").Trim(),
                        ApellidoPaterno = caso.Field<string>("PATERNO") == null ? "" : caso.Field<string>("PATERNO").Trim(),
                        ApellidoMaterno = caso.Field<string>("MATERNO") == null ? "" : caso.Field<string>("MATERNO").Trim(),
                        Nombres = caso.Field<string>("NOMBRES") == null ? "" : caso.Field<string>("NOMBRES").Trim(),
                        Empresa = caso.Field<string>("EMPRESA") == null ? "" : caso.Field<string>("EMPRESA").Trim(),
                        Sucursal = caso.Field<string>("SUCURSAL") == null ? "" : caso.Field<string>("SUCURSAL").Trim(),
                        Agencia = caso.Field<string>("AGENCIA") == null ? "" : caso.Field<string>("AGENCIA").Trim(),
                        FuncionarioAtencion = caso.Field<string>("FUNC_ATN") == null ? "" : caso.Field<string>("FUNC_ATN").Trim(),
                        FechaAsignacion = caso.Field<string>("FEC_ASIG") == null ? "" : caso.Field<string>("FEC_ASIG").Trim(),
                        HoraAsignacion = caso.Field<string>("HORA_ASIG") == null ? "" : caso.Field<string>("HORA_ASIG").Trim(),
                        Estado = caso.Field<string>("ESTADO") == null ? "" : caso.Field<string>("ESTADO").Trim(),
                        EstadoCaso = caso.Field<string>("ESTADO_CASO") == null ? "" : caso.Field<string>("ESTADO_CASO").Trim(),
                        FechaInicioAtencion = caso.Field<string>("FEC_INI_ATN") == null ? "" : caso.Field<string>("FEC_INI_ATN").Trim(),
                        HoraInicioAtencion = caso.Field<string>("HORA_INI_ATN") == null ? "" : caso.Field<string>("HORA_INI_ATN").Trim(),
                        FechaFinAtencion = caso.Field<string>("FEC_FIN_ATN") == null ? "" : caso.Field<string>("FEC_FIN_ATN").Trim(),
                        HoraFinAtencion = caso.Field<string>("HORA_FIN_ATN") == null ? "" : caso.Field<string>("HORA_FIN_ATN").Trim(),
                        FechaDeathLine = caso.Field<string>("FEC_DEATH_LINE") == null ? "" : caso.Field<string>("FEC_DEATH_LINE").Trim(),
                        HoraDeathLine = caso.Field<string>("HORA_DEATH_LINE") == null ? "" : caso.Field<string>("HORA_DEATH_LINE").Trim(),
                        Cuenta = caso.Field<string>("NROCTA") == null ? "" : caso.Field<string>("NROCTA").Trim(),
                        Tarjeta = caso.Field<string>("NROTARJETA") == null ? "" : caso.Field<string>("NROTARJETA").Trim(),
                        Monto = caso.Field<decimal>("MONTO"),
                        Moneda = caso.Field<string>("MONEDA") == null ? "" : caso.Field<string>("MONEDA").Trim(),
                        FechaTxn = caso.Field<string>("FEC_TXN") == null ? "" : caso.Field<string>("FEC_TXN").Trim(),
                        HoraTxn = caso.Field<string>("HORA_TXN") == null ? "" : caso.Field<string>("HORA_TXN").Trim(),
                        InformacionAdicional = caso.Field<string>("INF_ADICIONAL") == null ? "" : caso.Field<string>("INF_ADICIONAL").Trim(),
                        ATMSucursal = caso.Field<string>("ATM_SUC") == null ? "" : caso.Field<string>("ATM_SUC").Trim(),
                        ATMUbicacion = caso.Field<string>("ATM_UBICACION") == null ? "" : caso.Field<string>("ATM_UBICACION").Trim(),
                        DocumentacionAdjuntaEntrada = caso.Field<string>("DOC_ADJ_IN") == null ? "" : caso.Field<string>("DOC_ADJ_IN").Trim(),
                        TipoSolucion = caso.Field<string>("TIPO_SOLUCION") == null ? "" : caso.Field<string>("TIPO_SOLUCION").Trim(),
                        DescripcionSolucion = caso.Field<string>("DESC_SOLUCION") == null ? "" : caso.Field<string>("DESC_SOLUCION").Trim(),
                        IdRegistroErrorSolucion = caso.Field<string>("ID_ERROR_SOL") == null ? "" : caso.Field<string>("ID_ERROR_SOL").Trim(),
                        DescripcionRegistroErrorSolucion = caso.Field<string>("DESC_ERROR_SOL") == null ? "" : caso.Field<string>("DESC_ERROR_SOL").Trim(),
                        SWErrorSolucion = caso.Field<string>("SW_ERROR_SOL") == null ? "" : caso.Field<string>("SW_ERROR_SOL").Trim(),
                        SWDescCenter = caso.Field<string>("SW_DESCENTR") == null ? "" : caso.Field<string>("SW_DESCENTR").Trim(),
                        AreaRespuesta = caso.Field<string>("AREA_RESP") == null ? "" : caso.Field<string>("AREA_RESP").Trim(),
                        SucursalSolucion = caso.Field<string>("SUC_SOLUCION") == null ? "" : caso.Field<string>("SUC_SOLUCION").Trim(),
                        DocumentacionAdjuntaSalida = caso.Field<string>("DOC_ADJ_OUT") == null ? "" : caso.Field<string>("DOC_ADJ_OUT").Trim(),
                        ViaEnvioRespuesta = caso.Field<string>("VIA_ENVIO") == null ? "" : caso.Field<string>("VIA_ENVIO").Trim(),
                        TipoCarta = caso.Field<string>("TIPO_CARTA") == null ? "" : caso.Field<string>("TIPO_CARTA").Trim(),
                        SWGeneracionCarta = caso.Field<string>("SW_GEN_CARTA") == null ? "" : caso.Field<string>("SW_GEN_CARTA").Trim(),
                        NroCaso = caso.Field<string>("NRO_CARTA") == null ? "" : caso.Field<string>("NRO_CARTA").Trim(),
                        SWRespuestaEnviada = caso.Field<string>("SW_RESP_ENVIADA") == null ? "" : caso.Field<string>("SW_RESP_ENVIADA").Trim(),
                        SWComEmail = caso.Field<string>("SW_COM_EMAIL") == null ? "" : caso.Field<string>("SW_COM_EMAIL").Trim(),
                        SWComTel = caso.Field<string>("SW_COM_TEL") == null ? "" : caso.Field<string>("SW_COM_TEL").Trim(),
                        DireccionRespuesta = caso.Field<string>("DIRECCION_RESP") == null ? "" : caso.Field<string>("DIRECCION_RESP").Trim(),
                        EmailRespuesta = caso.Field<string>("EMAIL_RESP") == null ? "" : caso.Field<string>("EMAIL_RESP").Trim(),
                        TelefonoRespuesta = caso.Field<string>("TELEFONO_RESP") == null ? "" : caso.Field<string>("TELEFONO_RESP").Trim(),
                        SWErrorReg = caso.Field<string>("SW_ERROR_REG") == null ? "" : caso.Field<string>("SW_ERROR_REG").Trim(),
                        IdRegistroError = caso.Field<string>("ID_ERROR_REG") == null ? "" : caso.Field<string>("ID_ERROR_REG").Trim(),
                        DescripcionRegistroError = caso.Field<string>("DESC_ERROR_REG") == null ? "" : caso.Field<string>("DESC_ERROR_REG").Trim(),
                        AntServ = caso.Field<string>("ANT_SERV") == null ? "" : caso.Field<string>("ANT_SERV").Trim(),
                        ClasObs = caso.Field<string>("CLAS_OBS") == null ? "" : caso.Field<string>("CLAS_OBS").Trim(),
                        AreaOrigen = caso.Field<string>("AREA_OR") == null ? "" : caso.Field<string>("AREA_OR").Trim(),
                        PaternoOrigen = caso.Field<string>("PATERNO_OR") == null ? "" : caso.Field<string>("PATERNO_OR").Trim(),
                        MaternoOrigen = caso.Field<string>("MATERNO_OR") == null ? "" : caso.Field<string>("MATERNO_OR").Trim(),
                        NombresOrigen = caso.Field<string>("NOMBRES_OR") == null ? "" : caso.Field<string>("NOMBRES_OR").Trim(),
                        UsuarioOrigen = caso.Field<string>("USUARIO_OR") == null ? "" : caso.Field<string>("USUARIO_OR").Trim(),
                        RutaSharepoint = caso.Field<string>("RUTA_SHAREPOINT") == null ? "" : caso.Field<string>("RUTA_SHAREPOINT").Trim(),
                        DiasAtencion= caso.Field<int>("DIAS_ATN")
                    });
                    try
                    {
                        response.Last().ImporteDev = caso.Field<decimal>("IMPORTE_DEV");
                        response.Last().MonedaDev = caso.Field<string>("MONEDA_DEV").Trim();
                    }
                    catch (System.Exception)
                    {
                        response.Last().ImporteDev = 0M;
                        response.Last().MonedaDev = "";
                    }
                }
                return response;
            }
            return null;
        }

        public async Task<List<GetCasoDTOByAnalistaResponse>> GetCasoByAnalistaAsync(string usuario, string estado)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@USUARIO", usuario));
            parameters.Add(new SqlParameter("@ESTADO", estado));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_RECLAMOSelectAllALERTA_ANALISTA", parameters);

            
            if (casos.Rows.Count >0)
            {
                List<GetCasoDTOByAnalistaResponse> response = new List<GetCasoDTOByAnalistaResponse>();

                foreach (DataRow caso in casos.Rows)
                {
                    response.Add( new GetCasoDTOByAnalistaResponse
                    {
                        FuncionarioRegistro = caso.Field<string>("FUNC_REG") == null ? "" : caso.Field<string>("FUNC_REG").Trim(),
                        FechaRegistro = caso.Field<string>("FEC_REG") == null ? "" : caso.Field<string>("FEC_REG").Trim(),
                        HoraRegistro = caso.Field<string>("HORA_REG") == null ? "" : caso.Field<string>("HORA_REG").Trim(),
                        ApellidoPaterno = caso.Field<string>("PATERNO") == null ? "" : caso.Field<string>("PATERNO").Trim(),
                        ApellidoMaterno = caso.Field<string>("MATERNO") == null ? "" : caso.Field<string>("MATERNO").Trim(),
                        Nombres = caso.Field<string>("NOMBRES") == null ? "" : caso.Field<string>("NOMBRES").Trim(),
                        Producto = caso.Field<string>("DESC_PRODUCTO") == null ? "" : caso.Field<string>("DESC_PRODUCTO").Trim(),
                        ProductoId = caso.Field<string>("ID_PROD") == null ? "" : caso.Field<string>("ID_PROD").Trim(),
                        Servicio = caso.Field<string>("DESC_SERVICIO") == null ? "" : caso.Field<string>("DESC_SERVICIO").Trim(),
                        ServicioId = caso.Field<string>("ID_SERV") == null ? "" : caso.Field<string>("ID_SERV").Trim(),
                        NombreEmpresa = caso.Field<string>("EMPRESA") == null ? "" : caso.Field<string>("EMPRESA").Trim(),
                        Sucursal = caso.Field<string>("SUCURSAL") == null ? "" : caso.Field<string>("SUCURSAL").Trim(),                       
                        FuncionarioAtencion = caso.Field<string>("FUNC_ATN") == null ? "" : caso.Field<string>("FUNC_ATN").Trim(),
                        Estado = caso.Field<string>("ESTADO") == null ? "" : caso.Field<string>("ESTADO").Trim(),                        
                        InformacionAdicional = caso.Field<string>("INF_ADICIONAL") == null ? "" : caso.Field<string>("INF_ADICIONAL").Trim(),
                        NroCaso = caso.Field<string>("NRO_CARTA") == null ? "" : caso.Field<string>("NRO_CARTA").Trim(),
                        DescripcionErrorRegistro= caso.Field<string>("DESC_ERROR_REG") == null ? "" : caso.Field<string>("DESC_ERROR_REG").Trim(),
                        IdcNumero = caso.Field<string>("IDC_CLIE") == null ? "" : caso.Field<string>("IDC_CLIE").Trim(),
                        IdcTipo = caso.Field<string>("IDC_TIPO") == null ? "" : caso.Field<string>("IDC_TIPO").Trim(),
                        IdcExtension = caso.Field<string>("IDC_EXT") == null ? "" : caso.Field<string>("IDC_EXT").Trim(),
                        IdcCliente= caso.Field<string>("IDC_CLIENTE") == null ? "" : caso.Field<string>("IDC_CLIENTE").Trim(),
                        EstadoDescripcion = caso.Field<string>("DESCRIPCION_ESTADO") == null ? "" : caso.Field<string>("DESCRIPCION_ESTADO").Trim(),
                        DiasAtencion = caso.Field<int>("DIAS_ATN"),
                        DiasLimite = caso.Field<int>("DIAS_LIM"),
                        DiasDiferencia = caso.Field<int>("DIAS_DIF")
                    });
                }                
                return response;
            }

            return null;
        }

        public async Task<List<GetCasoDTOByEstadoResponse>> GetCasoByEstadoAsync(string estado)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@ESTADO", estado));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_BANDEJA_AllByEstado", parameters);


            if (casos.Rows.Count > 0)
            {
                var response = new List<GetCasoDTOByEstadoResponse>();

                foreach (DataRow caso in casos.Rows)
                {
                    response.Add(new GetCasoDTOByEstadoResponse
                    {
                        FechaRegistro = caso.Field<string>("FEC_REG") == null ? "" : caso.Field<string>("FEC_REG").Trim(),
                        NombreEmpresa = caso.Field<string>("EMPRESA") == null ? "" : caso.Field<string>("EMPRESA").Trim(),
                        FuncionarioAtencion = caso.Field<string>("MATRICULA") == null ? "" : caso.Field<string>("MATRICULA").Trim(),
                        Estado = caso.Field<string>("ESTADO") == null ? "" : caso.Field<string>("ESTADO").Trim(),
                        InformacionAdicional = caso.Field<string>("DETALLE") == null ? "" : caso.Field<string>("DETALLE").Trim(),
                        NroCaso = caso.Field<string>("CARTA") == null ? "" : caso.Field<string>("CARTA").Trim(),
                        Descripcion = caso.Field<string>("SERVICIO") == null ? "" : caso.Field<string>("SERVICIO").Trim(),
                        IdcCliente = caso.Field<string>("IDC") == null ? "" : caso.Field<string>("IDC").Trim(),
                        DiasAtencion = caso.Field<int>("DIAS_ATN"),
                        DiasDiferencia = caso.Field<int>("DIAS_DIF"),
                        FuncionarioNombre= caso.Field<string>("FUNCIONARIO") == null ? "" : caso.Field<string>("FUNCIONARIO").Trim(),
                        Dias=caso.Field<int>("DIAS")
                    });
                }
                return response;
            }

            return null;
        }

        public async Task<List<GetCasoResponse>> GetListCasoAsync(GetCasoRequest request)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ESTADO", request.Estado));
            parameters.Add(new SqlParameter("@PRODUCTO", request.Producto));
            parameters.Add(new SqlParameter("@SERVICIO", request.Servicio));
            parameters.Add(new SqlParameter("@TIPOCASO", request.TipoCaso));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_BANDEJA_All", parameters);


            if (casos.Rows.Count > 0)
            {
                var response = new List<GetCasoResponse>();

                foreach (DataRow caso in casos.Rows)
                {
                    response.Add(new GetCasoResponse
                    {
                        FuncionarioAtencion = caso.Field<string>("MATRICULA") == null ? "" : caso.Field<string>("MATRICULA").Trim(),
                        Estado = caso.Field<string>("ESTADO") == null ? "" : caso.Field<string>("ESTADO").Trim(),
                        InformacionAdicional = caso.Field<string>("DETALLE") == null ? "" : caso.Field<string>("DETALLE").Trim(),
                        NroCaso = caso.Field<string>("CARTA") == null ? "" : caso.Field<string>("CARTA").Trim(),
                        IdcCliente = caso.Field<string>("IDC") == null ? "" : caso.Field<string>("IDC").Trim(),
                        FuncionarioNombre = caso.Field<string>("FUNCIONARIO") == null ? "" : caso.Field<string>("FUNCIONARIO").Trim(),
                        Dias = caso.Field<int>("DIAS")
                    });
                }
                return response;
            }

            return null;
        }

        public async Task<bool> UpdateSolucionCasoDateAsync(UpdateCasoSolucionDateDTO casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@FUNC_REG", casoRequest.FuncionarioModificacion));
            parameters.Add(new SqlParameter("@FEC_REG", casoRequest.FechaRegistro));
            parameters.Add(new SqlParameter("@HORA_REG", casoRequest.HoraRegistro));
            parameters.Add(new SqlParameter("@NRO_CARTA", casoRequest.NroCarta));
            parameters.Add(new SqlParameter("@ESTADO", casoRequest.Estado));
            parameters.Add(new SqlParameter("@TIPO_SOLUCION", casoRequest.TipoSolucion));
            parameters.Add(new SqlParameter("@DESC_SOLUCION", casoRequest.DescripcionSolucion));
            parameters.Add(new SqlParameter("@SUC_SOLUCION", casoRequest.SucursalSolucion));
            parameters.Add(new SqlParameter("@DOC_ADJ_OUT", casoRequest.DocumentoAdjuntoOut));
            parameters.Add(new SqlParameter("@TIPO_CARTA", casoRequest.TipoCarta));
            parameters.Add(new SqlParameter("@IMPORTE_DEV", casoRequest.ImporteDevolucion));
            parameters.Add(new SqlParameter("@MONEDA_DEV", casoRequest.MonedaDevolucion));

            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_RECLAMOSUpdate_SolucionCASO", parameters);
        }

        public async Task<bool> InsertLogsCasoAsync(string nroCarta, string funMod)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NRO_CARTA", nroCarta));
            parameters.Add(new SqlParameter("@FUNC_MOD", funMod));

            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_INSERT_LOG", parameters);
        }

        

        public async Task<List<CasoResumen>> GetCasoResumenByEstadoAsync(string estado)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ESTADO", estado));
            var areas = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_ASIGNACIONSelectAll",parameters);
            if (areas.Rows.Count > 0)
            {
                var response = new List<CasoResumen>();
                foreach (DataRow row in areas.Rows)
                {
                    response.Add(new CasoResumen
                    {
                        NroCaso = row.Field<string>("CARTA").Trim(),
                        Producto = row.Field<string>("PRODUCTO").Trim(),
                        Servicio = row.Field<string>("SERVICIO").Trim(),
                        Detalle = row.Field<string>("DETALLE").Trim(),
                        Complejidad = row.Field<string>("COMPLEJIDAD").Trim(),
                        Dias = row.Field<int>("DIAS"),
                        DescripcionError = row.Field<string>("DECS_ERROR_REG"),
                        FuncionarioAtencion = row.Field<string>("FUNC_ATN").Trim()
                    });
                    return response;
                }
            }
            return null;
        }
        public async Task<bool> UpdateCasoEstadoComplejidadAsync(CasoComplejidad casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@CARTA", casoRequest.NroCaso));
            parameters.Add(new SqlParameter("@ESTADO", casoRequest.Estado));
            parameters.Add(new SqlParameter("@SERVICIO", casoRequest.ServicioId));
            parameters.Add(new SqlParameter("@COMPLEJIDAD", casoRequest.Complejidad));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_ASIGNACIONUpdateServicio", parameters);
        }
        public async Task<bool> UpdateCasoRechazarAsync(UpdateCasoRechazarDTO casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@CARTA", casoRequest.NroCaso));
            parameters.Add(new SqlParameter("@SW", casoRequest.SW));
            parameters.Add(new SqlParameter("@DESC", casoRequest.Descripcion));
            parameters.Add(new SqlParameter("@TIPERR", casoRequest.TipoError));
            parameters.Add(new SqlParameter("@FECHA_PRORROGA", casoRequest.FechaProrroga));
            return await _sarc_Bd.ExecuteSP("SARC.SP_RECHAZAR_CASO", parameters);
        }
        public async Task<bool> UpdateCasoReaperturaAsync(UpdateCasoDTOEstado casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@CARTA", casoRequest.NroCaso));
            parameters.Add(new SqlParameter("@ESTADO", casoRequest.Estado));
            return await _sarc_Bd.ExecuteSP("SARC.RE_APERTUA_CASO", parameters);
        }

        public async Task<List<CasoLog>> GetCasoLogsAllAsync(string nroCaso)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NRO_CARTA", nroCaso));
            var areas = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_ASIGNACIONSelectAll", parameters);
            if (areas.Rows.Count > 0)
            {
                var response = new List<CasoLog>();
                foreach (DataRow row in areas.Rows)
                {
                    response.Add(new CasoLog
                    {
                        FechaModificacion = row.Field<string>("FECHA_MOD").Trim(),
                        HoraModificacion = row.Field<string>("HORA_MOD").Trim(),
                        Estado = row.Field<string>("NUEVO_ESTADO").Trim(),
                        FuncionarioRegistro = row.Field<string>("FUNCIONARIO_ASIG").Trim(),
                        FuncionarioModificacion = row.Field<string>("FUNCIONARIO_MOD").Trim()
                    });
                    return response;
                }
            }
            return null;
        }

        public async Task<bool> UpdateCasoRechazoAnalistaAsync(UpdateCasoRechazoAnalistaDTO request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FUNC_REG", request.FuncionarioRegistro));
            parametros.Add(new SqlParameter("@FEC_REG", request.FechaRegistro));
            parametros.Add(new SqlParameter("@HORA_REG", request.HoraRegistro));
            parametros.Add(new SqlParameter("@NRO_CARTA", request.NroCaso));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado));
            parametros.Add(new SqlParameter("@ANT_SERV", request.AntServ));
            parametros.Add(new SqlParameter("@ID_PROD", request.ProductoId));
            parametros.Add(new SqlParameter("@ID_SERV", request.ServicioId));
            parametros.Add(new SqlParameter("@SW_ERROR_REG", request.SWErrorReg));
            parametros.Add(new SqlParameter("@ID_ERROR_REG", request.IdRegistroError));
            parametros.Add(new SqlParameter("@DESC_ERROR_REG", request.DescripcionRegistroError));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_RECLAMOSUpdate_ErrorReg", parametros);
        }

        public async Task<List<Reclamos>> GetReclamoFuncionarioTipoAsync(string funcionario, string estado)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@USUARIO", funcionario));
            parametros.Add(new SqlParameter("@ESTADO", estado));
            var listaAlerta = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_RECLAMOSelectAllALERTA_ANALISTA", parametros);
            if (listaAlerta.Rows.Count > 0)
            {
                var response = new List<Reclamos>();
                foreach (DataRow row in listaAlerta.Rows)
                {
                    response.Add(new Reclamos()
                    {
                        NRO_CARTA = row.Field<string>("NRO_CARTA").Trim(),
                        IDC_CLIE = row.Field<string>("IDC_CLIE").Trim(),
                        IDC_TIPO = row.Field<string>("IDC_TIPO").Trim(),
                        IDC_EXT = row.Field<string>("IDC_EXT").Trim(),
                        IDC_CLIENTE = row.Field<string>("IDC_CLIENTE").Trim(),
                        PATERNO = row.Field<string>("PATERNO").Trim(),
                        MATERNO = row.Field<string>("MATERNO").Trim(),
                        NOMBRES = row.Field<string>("NOMBRES").Trim(),
                        EMPRESA = row.Field<string>("EMPRESA").Trim(),
                        ESTADO = row.Field<string>("ESTADO").Trim(),
                        DESCRIPCION_ESTADO = row.Field<string>("DESCRIPCION_ESTADO").Trim(),
                        PRODUCTOID = row.Field<string>("ID_PROD").Trim(),
                        PRODUCTO_DESCRIPCION = row.Field<string>("DESC_PRODUCTO").Trim(),
                        SERVICIOID = row.Field<string>("ID_SERV").Trim(),
                        SERVICIO_DESCRIPCION = row.Field<string>("DESC_SERVICIO").Trim(),
                        FUNC_ATN = row.Field<string>("FUNC_ATN").Trim(),
                        FUNC_REG = row.Field<string>("FUNC_REG").Trim(),
                        FEC_REG = row.Field<string>("FEC_REG").Trim(),
                        HORA_REG = row.Field<string>("HORA_REG").Trim(),
                        INF_ADICIONAL = row.Field<string>("INF_ADICIONAL").Trim(),
                        NRO_DIAS_ATN = row.Field<int>("DIAS_ATN"),
                        NRO_DIAS_LIMITE = row.Field<int>("DIAS_LIM"),
                        NRO_DIAS_RETRASO = row.Field<int>("DIAS_DIF"),
                        //------
                        SUCURSAL = row.Field<string>("SUCURSAL"),
                        DESC_ERROR_REG = row.Field<string>("DESC_ERROR_REG")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<List<string>> GetReclamoFuncionarioTipoAlertaAsync(string funcionario, string estado)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@USUARIO", funcionario));
            parametros.Add(new SqlParameter("@ESTADO", estado));
            var listaAlerta = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_RECLAMOSelectAllALERTA_ANALISTA_ALERTA", parametros);
            if (listaAlerta.Rows.Count > 0)
            {
                var response = new List<string>();
                foreach (DataRow row in listaAlerta.Rows)
                {
                    response.Add(row.Field<string>("NRO_CARTA").Trim());
                }
                return response;
            }
            return null;
        }

    } 
}

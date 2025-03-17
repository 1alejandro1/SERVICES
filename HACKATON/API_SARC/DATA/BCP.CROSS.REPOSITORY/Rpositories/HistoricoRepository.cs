using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Historico;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class HistoricoRepository: IHistoricoRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public HistoricoRepository(BD_SARC sarc_bd)
        {
            _sarc_Bd = sarc_bd;
        }
        public async Task<List<CasoDTOHistorico>> GetCasoHistoricoByIdcAllAsync(string ClienteIdc, string ClienteTipo, string ClienteExtension)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IDC_CLIE", ClienteIdc));
            parameters.Add(new SqlParameter("@IDC_TIPO", ClienteTipo));
            parameters.Add(new SqlParameter("@IDC_EXT", ClienteExtension));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_RECLAMOS_HISTORICOSelect", parameters);
            return GetRespuestaCasoHistorico(casos);
        }
        public async Task<List<CasoDTOHistorico>> GetCasoHistoricoByIdcFechaAllAsync(string ClienteIdc, string ClienteTipo, string ClienteExtension, string FechaInicio, string FechaFinal)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IDC_CLIE", ClienteIdc));
            parameters.Add(new SqlParameter("@IDC_TIPO", ClienteTipo));
            parameters.Add(new SqlParameter("@IDC_EXT", ClienteExtension));
            parameters.Add(new SqlParameter("@FECHA_INICIAL", FechaInicio));
            parameters.Add(new SqlParameter("@FECHA_FINAL", FechaFinal));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_RECLAMOS_HISTORICOSelectByFecha", parameters);
            return GetRespuestaCasoHistorico(casos);
        }
        public async Task<List<CasoDTOHistorico>> GetCasoHistoricoByDbcAllAsync(ClienteDbcRequest request)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IDC_CLIE", request.ClienteIdc));
            parameters.Add(new SqlParameter("@IDC_TIPO", request.ClienteTipo));
            parameters.Add(new SqlParameter("@IDC_EXT", request.ClienteExtension));
            parameters.Add(new SqlParameter("@PATERNO", request.Paterno));
            parameters.Add(new SqlParameter("@MATERNO", request.Materno));
            parameters.Add(new SqlParameter("@NOMBRES", request.Nombres));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_RECLAMOS_HISTORICOSelect_DBC", parameters);
            return GetRespuestaCasoHistorico(casos);
        }
        public async Task<List<CasoDTOHistorico>> GetCasoHistoricoByDbcFechaAllAsync(ClienteDbcFechaRequest request)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IDC_CLIE", request.ClienteIdc));
            parameters.Add(new SqlParameter("@IDC_TIPO", request.ClienteTipo));
            parameters.Add(new SqlParameter("@IDC_EXT", request.ClienteExtension));
            parameters.Add(new SqlParameter("@PATERNO", request.Paterno));
            parameters.Add(new SqlParameter("@MATERNO", request.Materno));
            parameters.Add(new SqlParameter("@NOMBRES", request.Nombres));
            parameters.Add(new SqlParameter("@FECHA_INICIAL", request.FechaInicio));
            parameters.Add(new SqlParameter("@FECHA_FINAL", request.FechaFinal));
            var casos = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_RECLAMOS_HISTORICOSelectByFecha_DBC", parameters);
            return GetRespuestaCasoHistorico(casos);
        }
        private List<CasoDTOHistorico> GetRespuestaCasoHistorico(DataTable casos)
        {
            if (casos.Rows.Count > 0)
            {
                var response = new List<CasoDTOHistorico>();
                foreach (DataRow caso in casos.Rows)
                {
                    response.Add(new CasoDTOHistorico
                    {
                        FuncionarioRegistro = caso.Field<string>("FUNC_REG") == null ? "" : caso.Field<string>("FUNC_REG").Trim(),
                        FechaRegistro = caso.Field<string>("FEC_REG") == null ? "" : caso.Field<string>("FEC_REG").Trim(),
                        HoraRegistro = caso.Field<string>("HORA_REG") == null ? "" : caso.Field<string>("HORA_REG").Trim(),
                        ClienteIdc = caso.Field<string>("IDC_CLIE") == null ? "" : caso.Field<string>("IDC_CLIE").Trim(),
                        ClienteTipo = caso.Field<string>("IDC_TIPO") == null ? "" : caso.Field<string>("IDC_TIPO").Trim(),
                        ClienteExtension = caso.Field<string>("IDC_EXT") == null ? "" : caso.Field<string>("IDC_EXT").Trim(),
                        Producto = caso.Field<string>("PRODUCTO") == null ? "" : caso.Field<string>("PRODUCTO").Trim(),
                        ProductoId = caso.Field<string>("ID_PROD") == null ? "" : caso.Field<string>("ID_PROD").Trim(),
                        Servicio = caso.Field<string>("SERVICIO") == null ? "" : caso.Field<string>("SERVICIO").Trim(),
                        ServicioId = caso.Field<string>("ID_SERV") == null ? "" : caso.Field<string>("ID_SERV").Trim(),
                        TipoServicioDescripcion = caso.Field<string>("TIPO_SERV") == null ? "" : caso.Field<string>("TIPO_SERV").Trim(),
                        TipoServicio = caso.Field<string>("TIPO") == null ? "" : caso.Field<string>("TIPO").Trim(),
                        ApellidoPaterno = caso.Field<string>("PATERNO") == null ? "" : caso.Field<string>("PATERNO").Trim(),
                        ApellidoMaterno = caso.Field<string>("MATERNO") == null ? "" : caso.Field<string>("MATERNO").Trim(),
                        Nombres = caso.Field<string>("NOMBRES") == null ? "" : caso.Field<string>("NOMBRES").Trim(),
                        Empresa = caso.Field<string>("EMPRESA") == null ? "" : caso.Field<string>("EMPRESA").Trim(),
                        Sucursal = caso.Field<string>("SUCURSAL") == null ? "" : caso.Field<string>("SUCURSAL").Trim(),
                        Agencia = caso.Field<string>("AGENCIA") == null ? "" : caso.Field<string>("AGENCIA").Trim(),
                        FuncionarioAtencion = caso.Field<string>("FUNC_ATN") == null ? "" : caso.Field<string>("FUNC_ATN").Trim(),
                        FechaAsignacion = caso.Field<string>("FEC_ASIG") == null ? "" : caso.Field<string>("FEC_ASIG").Trim(),
                        HoraAsignacion = caso.Field<string>("HORA_ASIG") == null ? "" : caso.Field<string>("HORA_ASIG").Trim(),
                        Estado = caso.Field<string>("ID_ESTADO") == null ? "" : caso.Field<string>("ID_ESTADO").Trim(),
                        EstadoDescripcion = caso.Field<string>("ESTADO") == null ? "" : caso.Field<string>("ESTADO").Trim(),
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
                        TipoSolucionDescripcion = caso.Field<string>("DESC_TIPO_SOL") == null ? "" : caso.Field<string>("DESC_TIPO_SOL").Trim(),
                        DescripcionSolucion = caso.Field<string>("DESC_SOLUCION") == null ? "" : caso.Field<string>("DESC_SOLUCION").Trim(),
                        IdRegistroErrorSolucion = caso.Field<string>("ID_ERROR_SOL") == null ? "" : caso.Field<string>("ID_ERROR_SOL").Trim(),
                        DescripcionRegistroErrorSolucion = caso.Field<string>("DESC_ERROR_SOL") == null ? "" : caso.Field<string>("DESC_ERROR_SOL").Trim(),
                        SWDescCenter = caso.Field<string>("SW_DESCENTR") == null ? "" : caso.Field<string>("SW_DESCENTR").Trim(),
                        AreaRespuesta = caso.Field<string>("AREA_RESP") == null ? "" : caso.Field<string>("AREA_RESP").Trim(),
                        SucursalSolucion = caso.Field<string>("SUC_SOLUCION") == null ? "" : caso.Field<string>("SUC_SOLUCION").Trim(),
                        DocumentacionAdjuntaSalida = caso.Field<string>("DOC_ADJ_OUT") == null ? "" : caso.Field<string>("DOC_ADJ_OUT").Trim(),
                        ViaEnvioRespuesta = caso.Field<string>("VIA_ENVIO") == null ? "" : caso.Field<string>("VIA_ENVIO").Trim(),
                        TipoCarta = caso.Field<string>("TIPO_CARTA_R") == null ? "" : caso.Field<string>("TIPO_CARTA_R").Trim(),
                        TipoCartaDescripcion = caso.Field<string>("TIPO_CARTA") == null ? "" : caso.Field<string>("TIPO_CARTA").Trim(),
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
                        Canal = caso.Field<string>("CANAL") == null ? "" : caso.Field<string>("CANAL").Trim()
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
    }
}

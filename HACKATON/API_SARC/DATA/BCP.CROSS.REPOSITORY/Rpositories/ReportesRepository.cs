using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Reportes;
using BCP.CROSS.MODELS.Reportes.Requests;
using BCP.CROSS.REPORTES;
using BCP.CROSS.REPOSITORY.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AnalistaModel = BCP.CROSS.MODELS.Reportes.Analista;
using ASFIModel = BCP.CROSS.MODELS.Reportes.ASFI;
using CNRModel = BCP.CROSS.MODELS.Reportes.CNR;

namespace BCP.CROSS.REPOSITORY.Rpositories
{

    public class ReportesRepository : IReportesRepository
    {
        private readonly BD_SARC _sarc_Bd;
        private readonly BD_SERVICIOS_SWAMP _ServiciosSwampBD;
        private readonly ReporteSettings _reporteSettings;
        public ReportesRepository(BD_SARC sarc_bd, BD_SERVICIOS_SWAMP ServiciosSwampBD,  IOptions<ReporteSettings> reporteSettings)
        {
            _sarc_Bd = sarc_bd;
            _ServiciosSwampBD = ServiciosSwampBD;
            _reporteSettings = reporteSettings.Value;
        }
        public async Task<List<ASFIModel.RegistradosModel>> GetASFIRegistrados(ReporteRequest Request)
        {
            List<ASFIModel.RegistradosModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ENTIDAD", _reporteSettings.Entidad));
            parameters.Add(new SqlParameter("@CORREENTIDAD", _reporteSettings.Correlativo));
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));
            parameters.Add(new SqlParameter("@CONTACTO", _reporteSettings.Contacto));
            parameters.Add(new SqlParameter("@PLAZO", _reporteSettings.Plazo));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_ASFI_REGISTRO", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new ASFIModel.RegistradosModel
                    {
                        CTipoEntidad = row.Field<string>("CTipoEntidad").Trim(),
                        CCorrelativoEntidad = row.Field<string>("CCorrelativoEntidad").Trim(),
                        CReclamo = row.Field<string>("CReclamo").Trim(),
                        TGestion = row.Field<string>("TGestion").Trim(),
                        FechaReclamo = row.Field<string>("FechaReclamo").Trim(),
                        CTipoIdentificacion = row.Field<string>("CTipoIdentificacion").Trim(),
                        CIDReclamante = row.Field<string>("CIDReclamante").Trim(),
                        TNombre = row.Field<string>("TNombre").Trim(),
                        TApellido = row.Field<string>("TApellido").Trim(),
                        CTipoOficina = row.Field<int>("CTipoOficina"),
                        CLocalidadOficina = row.Field<string>("CLocalidadOficina").Trim(),
                        CTipologia = row.Field<string>("CTipologia").Trim(),
                        CSubTipologia = row.Field<string>("CSubTipologia").Trim(),
                        TGlosa = row.Field<string>("TGlosa").Trim(),
                        NMontoComprometido = row.Field<string>("NMontoComprometido").Trim(),
                        CMoneda = row.Field<string>("CMoneda").Trim(),
                        CMonedaExtranjera = row.Field<string>("CMonedaExtranjera").Trim(),
                        NPlazoEstimado = row.Field<string>("NPlazoEstimado").Trim(),
                        TPersonaDeContacto = row.Field<string>("TPersonaDeContacto").Trim(),
                        DFechaReporte = row.Field<string>("DFechaReporte").Trim(),
                    });
                }
                return Response;
            }

            return null;
        }

        public async Task<List<ASFIModel.SolucionadosModel>> GetASFISolucionados(ReporteRequest Request)
        {
            List<ASFIModel.SolucionadosModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ENTIDAD", _reporteSettings.Entidad));
            parameters.Add(new SqlParameter("@CORREENTIDAD", _reporteSettings.Correlativo));
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_ASFI_SOLUCION", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new ASFIModel.SolucionadosModel
                    {
                        Entidad = row.Field<string>("ENTIDAD").Trim(),
                        Correentidad = row.Field<string>("CORREENTIDAD").Trim(),
                        Reclamo = row.Field<string>("RECLAMO").Trim(),
                        Anio = row.Field<string>("ANIO").Trim(),
                        Fecsol = row.Field<string>("FECSOL").Trim(),
                        Cite_Respuesta = row.Field<string>("CITE_RESPUESTA").Trim(),
                        Des_Sol = row.Field<string>("DESC_SOL").Trim(),
                        Tresultado = row.Field<string>("TRESULTADO").Trim(),
                        Fecrep = row.Field<string>("FECREP").Trim(),
                    });
                }
                return Response;
            }

            return null;
        }
        public async Task<List<AnalistaModel.CantidadCasosModel>> GetAnalistaCantidadCaso(ReporteRequest Request)
        {
            List<AnalistaModel.CantidadCasosModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_ANALISTA_CASOS", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new AnalistaModel.CantidadCasosModel
                    {
                        Nombre = row.Field<string>("NOMBRE").Trim(),
                        Tipo = row.Field<string>("TIPO").Trim(),
                        Cantidad = row.Field<int>("NRO")
                    });
                }
                return Response;
            }

            return null;
        }
        public async Task<List<AnalistaModel.CasosSolucionadosModel>> GetAnalistaCasosSolucionados(ReporteRequest Request)
        {
            List<AnalistaModel.CasosSolucionadosModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_CAPACIDAD_ANALISTA_SOLUCIONADOS", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new AnalistaModel.CasosSolucionadosModel
                    {
                        Nombre = row.Field<string>("NOMBRE").Trim(),
                        Cantidad = row.Field<int>("CAPACIDAD")
                    });
                }
                return Response;
            }

            return null;
        }
        public async Task<List<AnalistaModel.EspecialidadModel>> GetAnalistaEspecialidad(ReporteRequest Request)
        {
            List<AnalistaModel.EspecialidadModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_ANALISTA_X_ESPECIALIDAD", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new AnalistaModel.EspecialidadModel
                    {
                        Nombre = row.Field<string>("ANALISTA").Trim(),
                        Cantidad = row.Field<int>("CANTIDAD"),
                        Especialidad = row.Field<string>("ESPECIALIDAD").Trim()
                    });
                }
                return Response;
            }

            return null;
        }

        public async Task<List<ReporteBaseModel>> GetReporteBase(ReporteRequest Request)
        {
            List<ReporteBaseModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_BASE", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var t = row.Field<string>("SERVICIO");
                    Response.Add(new ReporteBaseModel
                    {
                        Carta = row.Field<string>("CARTA").Trim(),
                        Estado = row.Field<string>("ESTADO").Trim(),
                        TipoServicio = row.Field<string>("TIPOSERVICIO").Trim(),
                        DiasServicio = row.Field<string>("DIASSERVICIO").Trim(),
                        Servicio = row.Field<string>("SERVICIO") == null ? "" : row.Field<string>("SERVICIO").Trim(),
                        Producto = row.Field<string>("PRODUCTO") == null ? "" : row.Field<string>("PRODUCTO").Trim(),
                        Agencia = row.Field<string>("AGENCIA") == null ? "" : row.Field<string>("AGENCIA").Trim(),
                        DescAgencia = row.Field<string>("DESCAGENCIA") == null ? "" : row.Field<string>("DESCAGENCIA").Trim(),
                        FunReg = row.Field<string>("FUNREG") == null ? "" : row.Field<string>("FUNREG").Trim(),
                        FunAte = row.Field<string>("FUNATE") == null ? "" : row.Field<string>("FUNATE").Trim(),
                        NomFunCate = row.Field<string>("NOMFUNCATE") == null ? "" : row.Field<string>("NOMFUNCATE").Trim(),
                        Sucsol = row.Field<string>("SUCSOL") == null ? "" : row.Field<string>("SUCSOL").Trim(),
                        FecReg = row.Field<string>("FECREG") == null ? "" : row.Field<string>("FECREG").Trim(),
                        FecIni = row.Field<string>("FECINI") == null ? "" : row.Field<string>("FECINI").Trim(),
                        FecFin = row.Field<string>("FECFIN") == null ? "" : row.Field<string>("FECFIN").Trim(),
                        IDC = row.Field<string>("IDC") == null ? "" : row.Field<string>("IDC").Trim(),
                        IDCTipo = row.Field<string>("IDCTIPO") == null ? "" : row.Field<string>("IDCTIPO").Trim(),
                        IDCExt = row.Field<string>("IDCEXT") == null ? "" : row.Field<string>("IDCEXT").Trim(),
                        NroCuenta = row.Field<string>("NROCUENTA") == null ? "" : row.Field<string>("NROCUENTA").Trim(),
                        NroTarjeta = row.Field<string>("NROTARJETA") == null ? "" : row.Field<string>("NROTARJETA").Trim(),
                        Paterno = row.Field<string>("PATERNO") == null ? "" : row.Field<string>("PATERNO").Trim(),
                        Materno = row.Field<string>("MATERNO") == null ? "" : row.Field<string>("MATERNO").Trim(),
                        Nombres = row.Field<string>("NOMBRES") == null ? "" : row.Field<string>("NOMBRES").Trim(),
                        DirCli = row.Field<string>("DIRCLI") == null ? "" : row.Field<string>("DIRCLI").Trim(),
                        DirEnvio = row.Field<string>("DIRENVIO") == null ? "" : row.Field<string>("DIRENVIO").Trim(),
                        Telefono = row.Field<string>("TELEFONO") == null ? "" : row.Field<string>("TELEFONO").Trim(),
                        Celular = row.Field<string>("CELULAR") == null ? "" : row.Field<string>("CELULAR").Trim(),
                        AreaRor = row.Field<string>("AREAROR") == null ? "" : row.Field<string>("AREAROR").Trim(),
                        UsrOr = row.Field<string>("USROR") == null ? "" : row.Field<string>("USROR").Trim(),
                        NomOr = row.Field<string>("NOMOR") == null ? "" : row.Field<string>("NOMOR").Trim(),
                        DesReclamo = row.Field<string>("DESRECLAMO") == null ? "" : row.Field<string>("DESRECLAMO").Trim(),
                        WsDesc = row.Field<string>("WSDESC") == null ? "" : row.Field<string>("WSDESC").Trim(),
                        AreaResp = row.Field<string>("AREARESP") == null ? "" : row.Field<string>("AREARESP").Trim(),
                        AreaDesc = row.Field<string>("AREADESC") == null ? "" : row.Field<string>("AREADESC").Trim(),
                        TipoSol = row.Field<string>("TIPOSOL") == null ? "" : row.Field<string>("TIPOSOL").Trim(),
                        DescSol = row.Field<string>("DESCSOL") == null ? "" : row.Field<string>("DESCSOL").Trim(),
                        ATMSuc = row.Field<string>("ATMSUC") == null ? "" : row.Field<string>("ATMSUC").Trim(),
                        ATMUbi = row.Field<string>("ATMUBI") == null ? "" : row.Field<string>("ATMUBI").Trim(),
                        Monto = row.Field<decimal>("MONTO"),
                        Moneda = row.Field<string>("MONEDA") == null ? "" : row.Field<string>("MONEDA").Trim(),
                        FecTXN = row.Field<string>("FECTXN") == null ? "" : row.Field<string>("FECTXN").Trim(),
                        ViaEnvio = row.Field<string>("VIAENVIO") == null ? "" : row.Field<string>("VIAENVIO").Trim(),
                        ImporteDev = row.Field<decimal>("IMPORTEDEV"),
                        MonedaDev = row.Field<string>("MONEDADEV") == null ? "" : row.Field<string>("MONEDADEV").Trim(),
                        SwReg = row.Field<string>("SWREG") == null ? "" : row.Field<string>("SWREG").Trim(),
                        DescErrorReg = row.Field<string>("DESCERRORREG") == null ? "" : row.Field<string>("DESCERRORREG").Trim(),
                        T = row.Field<int>("T"),
                        Tarea = row.Field<string>("TAREA") == null ? "" : row.Field<string>("TAREA").Trim(),
                        Clasificacion = row.Field<string>("CLASIFICACION") == null ? "" : row.Field<string>("CLASIFICACION").Trim(),
                        AreaExt = row.Field<string>("AREAEXT") == null ? "" : row.Field<string>("AREAEXT").Trim()
                    });
                }
                return Response;
            }
            return null;
        }

        public async Task<List<CapacidadEspecialidadModel>> GetCapacidadEspecialidad(ReporteRequest Request)
        {
            List<CapacidadEspecialidadModel> Response = new();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_CAPACIDAD_ESPECIALIDAD", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new CapacidadEspecialidadModel
                    {
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Capacidad = row.Field<int>("CAPACIDAD")
                    });
                }
                return Response;
            }

            return null;
        }

        public async Task<List<CNRModel.TotalModel>> GetCNRTotal(ReporteRequest Request)
        {
            List<CNRModel.TotalModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));
            parameters.Add(new SqlParameter("@SERVICIO", _reporteSettings.ConsumosNoReconocidos));
            parameters.Add(new SqlParameter("@PRODUCTO", _reporteSettings.ProdCredito));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_CNR_TOTAL", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new CNRModel.TotalModel
                    {
                        SolucionadosTiempo = row.Field<int>("SA"),
                        SolucionadosVencidos = row.Field<int>("PE"),
                        PendientesTiempo = row.Field<int>("SC"),
                        PendientesVencidos = row.Field<int>("PV"),
                        TotalCasos = row.Field<int>("SA") + row.Field<int>("PE") + row.Field<int>("SC") + row.Field<int>("PV")
                    });
                }
                return Response;
            }

            return null;
        }

        public async Task<List<CNRModel.DetalleModel>> GetCNRDetalle(CRNDetalleRequest Request)
        {
            List<CNRModel.DetalleModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));
            parameters.Add(new SqlParameter("@SERVICIO", _reporteSettings.ConsumosNoReconocidos));
            parameters.Add(new SqlParameter("@PRODUCTO", _reporteSettings.ProdCredito));
            parameters.Add(new SqlParameter("@TIPO", Request.Tipo));

            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_CNR_DETALLADO", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new CNRModel.DetalleModel
                    {
                        Carta = row.Field<string>("CARTA").Trim(),
                        Nombre = row.Field<string>("NOMBRE").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Complejidad = row.Field<string>("COMPLEJIDAD").Trim(),
                        TSARC = row.Field<int>("TSARC"),
                        Tarea = row.Field<int>("Tarea"),
                        Tipo = row.Field<string>("TIPO").Trim()
                    });
                }
                return Response;
            }

            return null;
        }

        public async Task<List<TipoReclamoModel>> GetTipoReclamo(ReporteRequest Request)
        {
            List<TipoReclamoModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));


            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_RECLAMO_SERVICIO", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new TipoReclamoModel
                    {

                        Nombre = row.Field<string>("NOMBRE"),
                        Tipo = row.Field<string>("TIPO").Trim(),
                        Cantidad = row.Field<int>("NRO")

                    });
                }
                return Response;
            }

            return null;
        }

        public async Task<List<ReposicionTarjetaModel>> GetReposicionTarjeta(ReporteRequest Request)
        {
            List<ReposicionTarjetaModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));
            parameters.Add(new SqlParameter("@SERVICIO", _reporteSettings.SerReposicion));
            parameters.Add(new SqlParameter("@PRODUCTO", _reporteSettings.ProdCredito));


            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_REPOSICION_CREDITO", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new ReposicionTarjetaModel
                    {
                        Carta = row.Field<string>("NOMBRE").Trim(),
                        Fecha = row.Field<string>("FECHA").Trim(),
                        IDC = row.Field<string>("IDC").Trim(),
                        IDCTipo = row.Field<string>("IDCTIPO").Trim(),
                        IDCExt = row.Field<string>("IDCEXT").Trim(),
                        Tarjeta = row.Field<string>("TARJETA").Trim(),
                        Cliente = row.Field<string>("CLIENTE").Trim(),
                        DirCli = row.Field<string>("DIRCLI").Trim(),
                        DirEnvio = row.Field<string>("DIRENVIO").Trim(),
                        DirExtra = row.Field<string>("DIREXTRA").Trim(),
                        DirSyne = row.Field<string>("SIRSYNE").Trim(),
                        Telefono = row.Field<string>("TELEFONO").Trim(),
                        Celular = row.Field<string>("CELULAR").Trim()
                    });
                }
                return Response;
            }
            return null;
        }

        public async Task<List<DevolucionesATMPOSModel>> GetDevolucionesATMPOS(ReporteRequest Request)
        {
            List<DevolucionesATMPOSModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));
            parameters.Add(new SqlParameter("@PRODCREDITO", _reporteSettings.ATMPOS));
            parameters.Add(new SqlParameter("@PRODDEBITO", _reporteSettings.ProdDebito));


            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_CREDITO_DEBITO", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new DevolucionesATMPOSModel
                    {
                        Nro = row.Field<long>("NRO"),
                        Fecha = row.Field<string>("FECHA") == null ? "" : row.Field<string>("FECHA").Trim(),
                        Telefono = row.Field<string>("TELEFONO") == null ? "" : row.Field<string>("TELEFONO").Trim(),
                        Moneda = row.Field<string>("MONEDA") == null ? "" : row.Field<string>("MONEDA").Trim(),
                        Monto = row.Field<decimal>("MONTO"),
                        NroCaso = row.Field<string>("NROCASO") == null ? "" : row.Field<string>("NROCASO").Trim(),
                        NroTarjeta = row.Field<string>("NRO_TARJETA") == null ? "" : row.Field<string>("NRO_TARJETA").Trim(),
                        Cliente = row.Field<string>("CLIENTE") == null ? "" : row.Field<string>("CLIENTE").Trim(),
                        Servicio = row.Field<string>("SERVICIO") == null ? "" : row.Field<string>("SERVICIO").Trim(),
                        Observaciones = row.Field<string>("OBSERVACIONES") == null ? "" : row.Field<string>("OBSERVACIONES").Trim(),
                        Llamada1 = row.Field<string>("LLAMADA1") == null ? "" : row.Field<string>("LLAMADA1").Trim(),
                        Hora1 = row.Field<string>("HORA1") == null ? "" : row.Field<string>("HORA1").Trim(),
                        Llamada2 = row.Field<string>("LLAMADA2") == null ? "" : row.Field<string>("LLAMADA2").Trim(),
                        Hora2 = row.Field<string>("HORA2") == null ? "" : row.Field<string>("HORA2").Trim(),
                        Detalle = row.Field<string>("DETALLE") == null ? "" : row.Field<string>("DETALLE").Trim(),
                        Usuario = row.Field<string>("USUARIO") == null ? "" : row.Field<string>("USUARIO").Trim()
                    });
                }
                return Response;
            }
            return null;
        }

        public async Task<List<ExpedicionModel>> GetExpedicion(ReporteRequest Request)
        {
            List<ExpedicionModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FECHAINI", Request.FechaIni));
            parameters.Add(new SqlParameter("@FECHAFIN", Request.FechaFin));


            var dt = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_REP_EXPEDICION", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new ExpedicionModel
                    {
                        Nro = row.Field<long>("NRO"),
                        FecEmi = row.Field<string>("FEC_EMI") == null ? "" : row.Field<string>("FEC_EMI").Trim(),
                        FecExp = row.Field<string>("FEC_EXP") == null ? "" : row.Field<string>("FEC_EXP").Trim(),
                        Carta = row.Field<string>("CARTA") == null ? "" : row.Field<string>("CARTA").Trim(),
                        Nombre = row.Field<string>("NOMBRE") == null ? "" : row.Field<string>("NOMBRE").Trim(),
                        Direccion = row.Field<string>("DIRECCION") == null ? "" : row.Field<string>("DIRECCION").Trim(),
                        Telefono = row.Field<string>("TELEFONO") == null ? "" : row.Field<string>("TELEFONO").Trim(),
                        Sucursal = row.Field<string>("SUCURSAL") == null ? "" : row.Field<string>("SUCURSAL").Trim(),
                        Retencion = row.Field<string>("RETENCION") == null ? "" : row.Field<string>("RETENCION").Trim(),
                        Usuario = row.Field<string>("USUARIO") == null ? "" : row.Field<string>("USUARIO").Trim(),
                    });
                }
                return Response;
            }
            return null;
        }

        public async Task<List<CobrosDevolucionesModel>> GetCobrosDevoluciones(CobrosDevolucionesRequestModel Request)
        {
            List<CobrosDevolucionesModel> Response = new();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FEC_INI", Request.FechaInicio));
            parameters.Add(new SqlParameter("@FEC_FIN", Request.FechaFin));
            parameters.Add(new SqlParameter("@CANAL", Request.Canal));
            parameters.Add(new SqlParameter("@FUNC", Request.Funcionario));
            parameters.Add(new SqlParameter("@ESTADO", Request.Estado));
            parameters.Add(new SqlParameter("@TIPO", Request.Tipo));

            var dt = await _ServiciosSwampBD.ExecuteSP_DataTable("DEVOLICIONES_COBROS_LOG_list", parameters);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Response.Add(new CobrosDevolucionesModel
                    {
                        Fecha = row.Field<DateTime>("FECHA"),
                        IDC = row.Field<string>("IDC") == null ? "" : row.Field<string>("IDC").Trim(),
                        Nombre = row.Field<string>("NOMBRE") == null ? "" : row.Field<string>("NOMBRE").Trim(),
                        Tipo = row.Field<int>("TIPO"),
                        DescTipo = row.Field<string>("DESC_TIPO") == null ? "" : row.Field<string>("DESC_TIPO").Trim(),
                        Cuenta = row.Field<string>("CUENTA") == null ? "" : row.Field<string>("CUENTA").Trim(),
                        Importe = row.Field<decimal>("IMPORTE"),
                        Moneda = row.Field<string>("MONEDA") == null ? "" : row.Field<string>("MONEDA").Trim(),
                        Descmoneda = row.Field<string>("DESC_MONEDA") == null ? "" : row.Field<string>("DESC_MONEDA").Trim(),
                        IdCanal = row.Field<int>("ID_CANAL"),
                        DescCanal = row.Field<string>("DESC_CANAL") == null ? "" : row.Field<string>("DESC_CANAL").Trim(),
                        Funcionario = row.Field<string>("FUNCIONARIO") == null ? "" : row.Field<string>("FUNCIONARIO").Trim(),
                        Supervisor = row.Field<string>("SUPERVISOR") == null ? "" : row.Field<string>("SUPERVISOR").Trim(),
                        NroCaso = row.Field<string>("NRO_CASO") == null ? "" : row.Field<string>("NRO_CASO").Trim(),
                        Completado = row.Field<bool>("COMPLETADO"),
                        DescCompletado = row.Field<string>("DESC_COMPLETADO") == null ? "" : row.Field<string>("DESC_COMPLETADO").Trim(),
                        Agencia = row.Field<string>("AGENCIA") == null ? "" : row.Field<string>("AGENCIA").Trim(),
                        NroAgencia = row.Field<string>("NRO_AGENCIA") == null ? "" : row.Field<string>("NRO_AGENCIA").Trim(),
                        NombreAgencia = row.Field<string>("NOMBRE_AGENCIA") == null ? "" : row.Field<string>("NOMBRE_AGENCIA").Trim(),
                        Sucursal = row.Field<string>("SUCURSAL") == null ? "" : row.Field<string>("SUCURSAL").Trim(),
                    });
                }
                return Response;
            }
            return null;
        }
    }
}

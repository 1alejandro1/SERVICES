using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS;
using ASFI = BCP.CROSS.MODELS.Reportes.ASFI;
using BCP.CROSS.MODELS.Reportes;
using Analista = BCP.CROSS.MODELS.Reportes.Analista;
using Microsoft.Extensions.Options;
using Sarc.WebApp.Connectors;
using Sarc.WebApp.Contracts;
using Sarc.WebApp.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CRN=BCP.CROSS.MODELS.Reportes.CNR;
using Req = BCP.CROSS.MODELS.Reportes.Requests;
using BCP.CROSS.MODELS.Carta;

namespace Sarc.WebApp.Services
{
    public class ReportesService : IReportesService
    {
        private readonly IOptions<SarcApiSettings> _sarcApiSettings;
        private readonly SarcApiConnector _sarcApi;
        private HttpClient _httpClient;
        public ReportesService(IHttpClientFactory httpClientFactory, IOptions<SarcApiSettings> sarcApiSettings, SarcApiConnector sarcApi)
        {
            _sarcApiSettings = sarcApiSettings;
            _sarcApi = sarcApi;
            IHttpClientFactory _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("SARC_API");
        }

        public async Task<ServiceResponse<List<Analista.CantidadCasosModel>>> GetAnalistaCantidadCasos(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostV2Async<List<Analista.CantidadCasosModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.Analista.CantidadCasos, request);

            return List;
        }

        public async Task<ServiceResponse<List<Analista.CasosSolucionadosModel>>> GetAnalistaCasosSolucionados(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<Analista.CasosSolucionadosModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.Analista.CasosSolucionados, request);
            return List;
        }

        public async Task<ServiceResponse<List<Analista.EspecialidadModel>>> GetAnalistaEspecialidad(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<Analista.EspecialidadModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.Analista.Especialidad, request);

            return List;
        }

        public async Task<ServiceResponse<List<ASFI.RegistradosModel>>> GetASFIRegistrados(ReportesFormViewModel req)
        {
            string FechaIni = req.FechaIni.ToString("yyyyMMdd");
            string FechaFin = req.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<ASFI.RegistradosModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.ASFI.Registrados, request);

            return List;
        }

        public async Task<ServiceResponse<List<ASFI.SolucionadosModel>>> GetASFISoluciones(ReportesFormViewModel req)
        {
            string FechaIni = req.FechaIni.ToString("yyyyMMdd");
            string FechaFin = req.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<ASFI.SolucionadosModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.ASFI.Soluciones, request);
            return List;
        }

        public async Task<ServiceResponse<List<CapacidadEspecialidadModel>>> GetCapacidadEspecialidad(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var _List = await _sarcApi.PostAsync<List<CapacidadEspecialidadModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.CapacidadEspecialidad, request);
            return _List;
        }

        public async Task<ServiceResponse<string>> GetCartaFile(CartaFileRequest Request)
        {
            var data = await _sarcApi.PostAsync<string, CartaFileRequest>
            (_sarcApiSettings.Value.Reportes.GetCartaFile, Request);
            return data;
        }

        public async Task<ServiceResponse<List<CRN.DetalleModel>>> GetCNRDetalle(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            Req.CRNDetalleRequest request = new Req.CRNDetalleRequest(FechaIni, FechaFin,Request.CNRTipo);
            var List = await _sarcApi.PostAsync<List<CRN.DetalleModel>, Req.CRNDetalleRequest>
            (_sarcApiSettings.Value.Reportes.CNR.Detalle, request);
            return List;
        }

        public async Task<ServiceResponse<List<CRN.TotalModel>>> GetCNRTotal(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<CRN.TotalModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.CNR.Total, request);
            return List;
        }

        public async Task<ServiceResponse<List<CobrosDevolucionesModel>>> GetCobrosDevoluciones(CobrosDevolucionesViewModel Request)
        {
            Req.CobrosDevolucionesRequestModel req = new Req.CobrosDevolucionesRequestModel();
            req.Canal= Request.Canal;
            req.Estado= Request.Estado;
            req.Funcionario= Request.Funcionario;
            req.Tipo= Request.Tipo;
            req.FechaInicio=Request.FechaInicio.ToString("yyyyMMdd");
            req.FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            var List = await _sarcApi.PostAsync<List<CobrosDevolucionesModel>, Req.CobrosDevolucionesRequestModel>
            (_sarcApiSettings.Value.Reportes.CobrosDevoluciones, req);
            return List;
        }

        public async Task<ServiceResponse<List<DevolucionesATMPOSModel>>> GetDevolucionATMPOS(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<DevolucionesATMPOSModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.DevolucionATMPOS, request);
            return List;
        }

        public async Task<ServiceResponse<List<ExpedicionModel>>> GetExpedicion(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<ExpedicionModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.Expedicion, request);
            return List;
        }

        public async Task<ServiceResponse<List<ReporteBaseModel>>> GetReporteBase(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<ReporteBaseModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.ReporteBase, request);
            return List;
        }

        public async Task<ServiceResponse<List<ReposicionTarjetaModel>>> GetReposicionTarjeta(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<ReposicionTarjetaModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.ReposicionTarjeta, request);
            return List;
        }

        public async Task<ServiceResponse<List<TipoReclamoModel>>> GetTipoReclamo(ReportesFormViewModel Request)
        {
            string FechaIni = Request.FechaIni.ToString("yyyyMMdd");
            string FechaFin = Request.FechaFin.ToString("yyyyMMdd");
            ReporteRequest request = new ReporteRequest(FechaIni, FechaFin);
            var List = await _sarcApi.PostAsync<List<TipoReclamoModel>, ReporteRequest>
            (_sarcApiSettings.Value.Reportes.TipoReclamo, request);
            return List;
        }
    }
}

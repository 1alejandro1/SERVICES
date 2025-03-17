using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Client;
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
    public class ClienteRepository : IClienteRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public ClienteRepository(BD_SARC sarc_bd)
        {
            _sarc_Bd = sarc_bd;
        }

        public async Task<List<GetClienteResponse>> GetClienteByIdcAsync(GetClientByIdcRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IDC", request?.Idc.Trim().PadLeft(8, '0')));
            parametros.Add(new SqlParameter("@TIPO", request.TipoIdc.Trim().ToUpper()));
            parametros.Add(new SqlParameter("@EXT", request.Extension.Trim().ToUpper()));

            var datatable = await _sarc_Bd.ExecuteSP_DataTable("SARC.CLIENTE_ListByIDCCliente", parametros);

            if (datatable != null && datatable.Rows.Count > 0)
            {
                var response = new List<GetClienteResponse>();
                foreach (DataRow item in datatable.Rows)
                {
                    var cliente = new GetClienteResponse
                    {
                        Idc = item.Field<string>("IDC")?.Trim(),
                        NroIdc = item.Field<string>("NRO_IDC"),
                        IdcTipo = item.Field<string>("TIPO_IDC")?.Trim(),
                        IdcExtension = item.Field<string>("EXT_IDC")?.Trim(),
                        Nombres = item.Field<string>("NOMBRES")?.Trim(),
                        PaternoRazonSocial = item.Field<string>("AP_PATERNO")?.Trim(),
                        Materno = item.Field<string>("AP_MATERNO")?.Trim(),
                        NombreCompleto = item.Field<string>("NOMBRE_COMPLETO")?.Trim(),
                        Direccion = item.Field<string>("DIRECCION")?.Trim(),
                        Direccion1 = item.Field<string>("DIRECCION 1")?.Trim(),
                        Direccion2 = string.Empty,
                        Telefono1 = item.Field<string>("TELEFONO 1")?.Trim(),
                        Telefono2 = item.Field<string>("TELEFONO 2")?.Trim(),
                        Cleular1 = item.Field<string>("CELULAR 1")?.Trim(),
                        Cleular2 = item.Field<string>("CELULAR 2")?.Trim(),
                        Email = item.Field<string>("MAIL")?.Trim(),
                        TipoCliente = "N",
                        TipoPersona = item.Field<string>("TIPO_PERS")?.Trim(),
                    };
                    response.Add(cliente);
                }
                return response;
            }
            return null;
        }

        public async Task<List<GetClienteResponse>> GetClienteByDbcAsync(GetClientByDbcRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CLI_PATERNO", request.PaternoRazonSocial ?? ""));
            parametros.Add(new SqlParameter("@CLI_MATERNO", request.Materno ?? ""));
            parametros.Add(new SqlParameter("@CLI_NOMBRES", request.Nombres ?? ""));

            var datatable = await _sarc_Bd.ExecuteSP_DataTable("SARC.CLIENTE_ListByDBC", parametros);

            if (datatable != null && datatable.Rows.Count > 0)
            {
                var response = new List<GetClienteResponse>();
                foreach (DataRow item in datatable.Rows)
                {
                    var cliente = new GetClienteResponse
                    {
                        Idc = item.Field<string>("IDC")==null?"": item.Field<string>("IDC").Trim(),
                        NroIdc = item.Field<string>("NRO_IDC") == null ? "" : item.Field<string>("NRO_IDC").Trim(),
                        IdcTipo = item.Field<string>("TIPO_IDC") == null ? "" : item.Field<string>("TIPO_IDC").Trim(),
                        IdcExtension = item.Field<string>("EXT_IDC") == null ? "" : item.Field<string>("EXT_IDC").Trim(),
                        Nombres = item.Field<string>("NOMBRES") == null ? "" : item.Field<string>("NOMBRES").Trim(),
                        PaternoRazonSocial = item.Field<string>("AP_PATERNO") == null ? "" : item.Field<string>("AP_PATERNO").Trim(),
                        Materno = item.Field<string>("AP_MATERNO") == null ? "" : item.Field<string>("AP_MATERNO").Trim(),
                        NombreCompleto = item.Field<string>("NOMBRE_COMPLETO") == null ? "" : item.Field<string>("NOMBRE_COMPLETO").Trim(),
                        Direccion = item.Field<string>("DIRECCION") == null ? "" : item.Field<string>("DIRECCION").Trim(),
                        Direccion1 = item.Field<string>("DIRECCION 1") == null ? "" : item.Field<string>("DIRECCION 1").Trim(),
                        Direccion2 = item.Field<string>("DIRECCION 2") == null ? "" : item.Field<string>("DIRECCION 2").Trim(),
                        Telefono1 = item.Field<string>("TELEFONO 1") == null ? "" : item.Field<string>("TELEFONO 1").Trim(),
                        Telefono2 = item.Field<string>("TELEFONO 2") == null ? "" : item.Field<string>("TELEFONO 2").Trim(),
                        Cleular1 = item.Field<string>("CELULAR 1") == null ? "" : item.Field<string>("CELULAR 1").Trim(),
                        Cleular2 = item.Field<string>("CELULAR 2") == null ? "" : item.Field<string>("CELULAR 2").Trim(),
                        Email = item.Field<string>("MAIL") == null ? "" : item.Field<string>("MAIL").Trim(),
                        //TipoCliente = "N",
                        TipoPersona = item.Field<string>("TIPO_PERS") == null ? "" : item.Field<string>("TIPO_PERS").Trim(),
                    };
                    response.Add(cliente);
                    response.Last().TipoCliente = TipoCliente(response.Last().IdcTipo);
                }
                return response;
            }
            return null;
        }
        private string TipoCliente(string tipoIdc)//natural o juridico
        {
            if (string.IsNullOrEmpty(tipoIdc))
            {
                return "J";
            }
            string[] pn = { "Q","P","Y"};
            if (pn.Contains(tipoIdc))
            {
                return "N";
            }
            else
            {
                return "J";
            }
        }
        public async Task<List<GetClienteResponse>> GetClienteByIdcReclamosAsync(GetClientByIdcRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IDC", request?.Idc.Trim().PadLeft(8,'0')));
            parametros.Add(new SqlParameter("@TIPO", request.TipoIdc?.ToUpper()));

            var datatable = await _sarc_Bd.ExecuteSP_DataTable("SARC.CLIENTE_ListByIDCCliente_Reclamos", parametros);

            if (datatable != null && datatable.Rows.Count > 0)
            {
                var response = new List<GetClienteResponse>();
                foreach (DataRow item in datatable.Rows)
                {
                    var cliente = new GetClienteResponse
                    {
                        Idc = item.Field<string>("IDC")?.Trim(),
                        NroIdc = item.Field<string>("NRO_IDC")?.Trim(),
                        IdcTipo = item.Field<string>("TIPO_IDC")?.Trim(),
                        IdcExtension = item.Field<string>("EXT_IDC")?.Trim(),
                        Nombres = item.Field<string>("NOMBRES")?.Trim(),
                        PaternoRazonSocial = item.Field<string>("AP_PATERNO")?.Trim(),
                        Materno = item.Field<string>("AP_MATERNO")?.Trim(),
                        NombreCompleto = item.Field<string>("NOMBRE_COMPLETO")?.Trim(),
                        Direccion = item.Field<string>("DIRECCION")?.Trim(),
                        Direccion1 = item.Field<string>("DIRECCION 1")?.Trim(),
                        Direccion2 = string.Empty,
                        Telefono1 = item.Field<string>("TELEFONO 1")?.Trim(),
                        Telefono2 = item.Field<string>("TELEFONO 2")?.Trim(),
                        Cleular1 = item.Field<string>("CELULAR 1")?.Trim(),
                        Cleular2 = item.Field<string>("CELULAR 2")?.Trim(),
                        Email = item.Field<string>("MAIL")?.Trim(),
                        TipoCliente = "N",
                        TipoPersona = item.Field<string>("TIPO_PERS")?.Trim(),
                    };
                    cliente.NombreCompleto = $"{cliente.PaternoRazonSocial} {cliente.Materno} {cliente.Nombres}";
                    response.Add(cliente);
                }
                return response;
            }
            return null;
        }
    }
}

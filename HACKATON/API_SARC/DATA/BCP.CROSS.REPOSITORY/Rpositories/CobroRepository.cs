using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Cobro;
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
    public class CobroRepository: ICobroRepository
    {
        private readonly BD_SERVICIOS_SWAMP _bdSrvSwamp;

        public CobroRepository(BD_SERVICIOS_SWAMP bdSrvSwamp)
        {
            this._bdSrvSwamp = bdSrvSwamp;
        }

        public async Task<List<CobroProductoServicioResponse>> CobroByPRAsync(string productoId, string servicioId)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@SERV", servicioId));
            parametros.Add(new SqlParameter("@PROD", productoId));
            var listaDevolucionPR = await _bdSrvSwamp.ExecuteSP_DataTable("dbo.COBRO_listByPR_CUENTACONTABLE", parametros);
            if (listaDevolucionPR.Rows.Count > 0)
            {
                var response = new List<CobroProductoServicioResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new CobroProductoServicioResponse
                    {
                        IdCobro = row.Field<int>("ID_COBRO"),
                        DescripcionCobro = row.Field<string>("DESCRIPCION_CO"),
                        DescripcionFacturacion = row.Field<string>("DESCRIPCION_FC"),
                        CodigoFacturacion = row.Field<string>("CODIGO_FACTURACION"),
                        ServiciosCanales = row.Field<int>("SERV_CANALES"),
                        TipoFacturacion = row.Field<string>("TIPO"),
                        Importe = row.Field<decimal>("IMPORTE"),
                        Glosa = row.Field<string>("GLOSA"),
                        CuentaContable = row.Field<string>("CUENTA_CONTABLE")
                    });
                }
                return response;
            }
            return null;
        }
        public async Task<List<Cobro>> GetCobroListAllAsync()
        {
            var listaFacturacion = await _bdSrvSwamp.ExecuteSP_DataTable("COBRO_ListAll");
            if (listaFacturacion.Rows.Count > 0)
            {
                var response = new List<Cobro>();

                foreach (DataRow row in listaFacturacion.Rows)
                {
                    response.Add(new Cobro
                    {
                        IdCobro = row.Field<int>("ID_COBRO"),
                        DescripcionCobro = row.Field<string>("DESCRIPCION"),
                        IdPeriodo = row.Field<int>("PERIODO"),
                        DescripcionPeriodo = row.Field<string>("DESC_PER"),
                        IdViaEnvio = row.Field<int>("VIA_ENVIO"),
                        DescripcionViaEnvio = row.Field<string>("DESC_ENV"),
                        IdTarifario = row.Field<int>("TARIFARIO"),
                        DescripcionTarifario = row.Field<string>("DESC_TAR"),
                        IdServicio = row.Field<string>("SERV_PR"),
                        DescripcionServicio = row.Field<string>("DESC_SERV").Trim(),
                        IdProducto = row.Field<string>("PRODUCTO_PR"),
                        DescripcionProducto = row.Field<string>("DESC_PROD"),
                        esVisiblePR = row.Field<bool>("VISIBLE_PR"),
                        VisiblePR = row.Field<string>("VISIBLE_PR_DESC"),
                        esVisibleSwamp = row.Field<bool>("VISIBLE_SWAMP"),
                        VisibleSwamp = row.Field<string>("VISIBLE_SWAMP_DESC"),
                        esCobroPlataforma = row.Field<bool>("COBRO_PLATAFORMA"),
                        CobroPlataforma = row.Field<string>("COBRO_PLATAFORMA_DESC"),
                        esCobroPR = row.Field<bool>("COBRO_PR"),
                        CobroPR = row.Field<string>("COBRO_PR_DESC"),
                        esCierraSwamp = row.Field<bool>("CIERRA_SWAMP"),
                        CierraSwamp = row.Field<string>("CIERRA_SWAMP_DESC"),
                        Glosa = row.Field<string>("GLOSA"),
                        esFacturable = row.Field<bool>("FACTURABLE"),
                        Facturable = row.Field<string>("FACTURABLE_DESC"),
                        IdServicioFacturacion = row.Field<int>("SERV_FACTURACION"),
                        DescripcionServicioFacturacion = row.Field<string>("DESC_FAC"),
                        IdServiciosCanales = row.Field<int>("SERV_CANALES"),
                        DescripcionServiciosCanales = row.Field<string>("DESC_CANAL")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<bool> InsertCobroAsync(CobroRegistro request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CIERRA_SWAMP", request.esCierraSwamp ? "1" : "0"));
            parametros.Add(new SqlParameter("@COBRO_PLATAFORMA", request.esCobroPlataforma ? "1" : "0"));
            parametros.Add(new SqlParameter("@COBRO_PR", request.esCobroPR ? "1" : "0"));
            parametros.Add(new SqlParameter("@DESC_PROD", request.DescripcionProducto));
            parametros.Add(new SqlParameter("@DESC_SERV", request.DescripcionServicio));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.DescripcionCobro));
            parametros.Add(new SqlParameter("@FACTURABLE", request.esFacturable ? "1" : "0"));
            parametros.Add(new SqlParameter("@GLOSA", request.Glosa));
            parametros.Add(new SqlParameter("@PERIODO", request.IdPeriodo));
            parametros.Add(new SqlParameter("@PRODUCTO_PR", request.IdProducto));
            parametros.Add(new SqlParameter("@SERV_PR", request.IdServicio));
            parametros.Add(new SqlParameter("@SERV_CANALES", request.IdServiciosCanales));
            parametros.Add(new SqlParameter("@SERV_FACTURACION", request.IdServicioFacturacion));
            parametros.Add(new SqlParameter("@TARIFARIO", request.IdTarifario));
            parametros.Add(new SqlParameter("@VIA_ENVIO", request.IdViaEnvio));
            parametros.Add(new SqlParameter("@VISIBLE_PR", request.esVisiblePR ? "1" : "0"));
            parametros.Add(new SqlParameter("@VISIBLE_SWAMP", request.esVisibleSwamp ? "1" : "0"));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("COBRO_Insert", parametros);
        }

        public async Task<bool> UpdateCobroAsync(CobroModificacion request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", request.IdCobro));
            parametros.Add(new SqlParameter("@CIERRA_SWAMP", request.esCierraSwamp ? "1" : "0"));
            parametros.Add(new SqlParameter("@COBRO_PLATAFORMA", request.esCobroPlataforma ? "1" : "0"));
            parametros.Add(new SqlParameter("@COBRO_PR", request.esCobroPR ? "1" : "0"));
            parametros.Add(new SqlParameter("@DESC_PROD", request.DescripcionProducto));
            parametros.Add(new SqlParameter("@DESC_SERV", request.DescripcionServicio));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.DescripcionCobro));
            parametros.Add(new SqlParameter("@FACTURABLE", request.esFacturable ? "1" : "0"));
            parametros.Add(new SqlParameter("@GLOSA", request.Glosa));
            parametros.Add(new SqlParameter("@PERIODO", request.IdPeriodo));
            parametros.Add(new SqlParameter("@PRODUCTO_PR", request.IdProducto));
            parametros.Add(new SqlParameter("@SERV_PR", request.IdServicio));
            parametros.Add(new SqlParameter("@SERV_CANALES", request.IdServiciosCanales));
            parametros.Add(new SqlParameter("@SERV_FACTURACION", request.IdServicioFacturacion));
            parametros.Add(new SqlParameter("@TARIFARIO", request.IdTarifario));
            parametros.Add(new SqlParameter("@VIA_ENVIO", request.IdViaEnvio));
            parametros.Add(new SqlParameter("@VISIBLE_PR", request.esVisiblePR ? "1" : "0"));
            parametros.Add(new SqlParameter("@VISIBLE_SWAMP", request.esVisibleSwamp ? "1" : "0"));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("COBRO_Update", parametros);
        }
        public async Task<bool> DeshabilitarCobroAsync(CobroDeshabilitar request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", request.IdCobro));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("COBRO_Delete", parametros);
        }
    }
}

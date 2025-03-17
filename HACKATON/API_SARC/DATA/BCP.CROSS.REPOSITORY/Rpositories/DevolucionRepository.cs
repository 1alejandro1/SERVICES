using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Devolucion;
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
    public class DevolucionRepository: IDevolucionRepository
    {
        private readonly BD_SERVICIOS_SWAMP _bdSrvSwamp;

        public DevolucionRepository(BD_SERVICIOS_SWAMP bdSrvSwamp)
        {
            this._bdSrvSwamp = bdSrvSwamp;
        }
        public async Task<List<DevolucionProductoServicioResponse>> DevolucionByPRAsync(string productoId, string servicioId)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@SERV", servicioId));
            parametros.Add(new SqlParameter("@PROD", productoId));
            var listaDevolucionPR = await _bdSrvSwamp.ExecuteSP_DataTable("dbo.DEVOLUCION_listByPR_CUENTACONTABLE", parametros);
            if (listaDevolucionPR.Rows.Count == 1)
            {
                var response = new List<DevolucionProductoServicioResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new DevolucionProductoServicioResponse
                    {
                        IdDevolucion = row.Field<int>("ID_DEVOLUCION"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
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
        public async Task<List<Devolucion>> GetDevolucionListAllAsync()
        {
            var listaFacturacion = await _bdSrvSwamp.ExecuteSP_DataTable("DEVOLUCION_listAll");
            if (listaFacturacion.Rows.Count > 0)
            {
                var response = new List<Devolucion>();

                foreach (DataRow row in listaFacturacion.Rows)
                {
                    response.Add(new Devolucion
                    {
                        IdDevolucion = row.Field<int>("ID_DEVOLUCION"),
                        DescripcionDevolucion = row.Field<string>("DESCRIPCION"),
                        IdTarifario = row.Field<int>("TARIFARIO"),
                        DescripcionTarifario = row.Field<string>("DESC_TAR"),
                        IdServicio = row.Field<string>("SERVICIO_PR"),
                        DescripcionServicio = row.Field<string>("DESC_SERV"),
                        IdProducto = row.Field<string>("PRODUCTO_PR"),
                        DescripcionProducto = row.Field<string>("DESC_PROD"),
                        esVisiblePR = row.Field<bool>("VISIBLE_PR"),
                        VisiblePR = row.Field<string>("VISIBLE_PR_DESC"),
                        esVisibleSwamp = row.Field<bool>("VISIBLE_SWAMP"),
                        VisibleSwamp = row.Field<string>("VISIBLE_SWAMP_DESC"),
                        IdServiciosCanales = row.Field<int>("SERV_CANALES"),
                        DescripcionServiciosCanales = row.Field<string>("DESC_CANAL"),
                        Glosa = row.Field<string>("GLOSA")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<bool> InsertDevolucionAsync(DevolucionRegistro request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DESC_PROD", request.DescripcionProducto));
            parametros.Add(new SqlParameter("@DESC_SERV", request.DescripcionServicio));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.DescripcionDevolucion));
            parametros.Add(new SqlParameter("@GLOSA", request.Glosa));
            parametros.Add(new SqlParameter("@PRODUCTO_PR", request.IdProducto));
            parametros.Add(new SqlParameter("@SERV_PR", request.IdServicio));
            parametros.Add(new SqlParameter("@SERV_CANALES", request.IdServiciosCanales));
            parametros.Add(new SqlParameter("@TARIFARIO", request.IdTarifario));
            parametros.Add(new SqlParameter("@VISIBLE_PR", request.esVisiblePR ? "1" : "0"));
            parametros.Add(new SqlParameter("@VISIBLE_SWAMP", request.esVisibleSwamp ? "1" : "0"));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("DEVOLUCION_Insert", parametros);
        }

        public async Task<bool> UpdateDevolucionAsync(DevolucionModificacion request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", request.IdDevolucion));
            parametros.Add(new SqlParameter("@DESC_PROD", request.DescripcionProducto));
            parametros.Add(new SqlParameter("@DESC_SERV", request.DescripcionServicio));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.DescripcionDevolucion));
            parametros.Add(new SqlParameter("@GLOSA", request.Glosa));
            parametros.Add(new SqlParameter("@PRODUCTO_PR", request.IdProducto));
            parametros.Add(new SqlParameter("@SERV_PR", request.IdServicio));
            parametros.Add(new SqlParameter("@SERV_CANALES", request.IdServiciosCanales));
            parametros.Add(new SqlParameter("@TARIFARIO", request.IdTarifario));
            parametros.Add(new SqlParameter("@VISIBLE_PR", request.esVisiblePR ? "1" : "0"));
            parametros.Add(new SqlParameter("@VISIBLE_SWAMP", request.esVisibleSwamp ? "1" : "0"));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("DEVOLUCION_Update", parametros);
        }

        public async Task<bool> DeshabilitarDevolucionAsync(DevolucionDeshabilitar request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", request.IdDevolucion));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("DEVOLUCION_delete", parametros);
        }
    }
}

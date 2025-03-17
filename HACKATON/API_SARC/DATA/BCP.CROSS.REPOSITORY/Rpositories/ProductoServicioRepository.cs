using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.ProductoServicio;
using BCP.CROSS.MODELS.Sarc;
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
    public class ProductoServicioRepository: IProductoServicioRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public ProductoServicioRepository(BD_SARC sarc_Bd)
        {
            this._sarc_Bd = sarc_Bd;
        }
        public async Task<List<Producto>> GetProductoActivosAllAsync()
        {
            var listaProducto = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_PRODUCTOSSelectAll_Estado");//MODIFICADO
            if (listaProducto.Rows.Count > 0)
            {
                var response = new List<Producto>();
                foreach (DataRow row in listaProducto.Rows)
                {
                    response.Add(new Producto
                    {
                        IdProducto = row.Field<string>("ID_PROD").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Estado = row.Field<string>("ESTADO").Trim(),
                        ReporteAsfi = row.Field<bool?>("REPORTE_ASFI") ?? false
                    });

                }
                return response;
            }
            return null;
        }

        public async Task<List<Producto>> GetProductoAllAsync()
        {
            var listaProducto = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_PRODUCTOSSelectAll");
            if (listaProducto.Rows.Count > 0)
            {
                var response = new List<Producto>();
                foreach (DataRow row in listaProducto.Rows)
                {
                    response.Add(new Producto
                    {
                        IdProducto = row.Field<string>("ID_PROD").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Estado = row.Field<string>("ESTADO").Trim(),
                        ReporteAsfi = row.Field<bool?>("REPORTE_ASFI") ?? false
                    });

                }
                return response;
            }
            return null;
        }

        public async Task<List<ProductoServicio>> GetProductoServicioAllAsync()
        {
            var listaProductoServicio = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_PROD_SERVSelectAll");
            if (listaProductoServicio.Rows.Count > 0)
            {
                var response = new List<ProductoServicio>();
                foreach (DataRow row in listaProductoServicio.Rows)
                {
                    response.Add(new ProductoServicio
                    {
                        IdServicio = row.Field<string>("ID_SERV").Trim(),
                        IdProducto = row.Field<string>("ID_PROD").Trim(),
                        Funcionario = row.Field<string>("FUNC").Trim(),
                        DocumentacionRequerida = row.Field<string>("DOC_REQ").Trim(),
                        Tiempo = row.Field<int>("TIEMPO"),
                        Estado = row.Field<string>("ESTADO").Trim()
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<List<ProductoByTipoEstadoResponse>> GetProductoTipoEstadoAsync(TipoServicioEstadoRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO", request.TipoServicio));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado));
            var listaserviciosEstado = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_SERVICIOSByTipoSelectAll", parametros);
            if (listaserviciosEstado.Rows.Count > 0)
            {
                var response = new List<ProductoByTipoEstadoResponse>();
                foreach (DataRow row in listaserviciosEstado.Rows)
                {
                    response.Add(new ProductoByTipoEstadoResponse
                    {
                        IdServicio = row.Field<string>("ID_SERV").Trim(),
                        Tipo = row.Field<string>("TIPO").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Estado = row.Field<string>("ESTADO"),
                        TipoUsuario = row.Field<string>("TIPO_USUARIO").Trim(),
                        TipoAsignacion = row.Field<string>("TIPO_ASIGNACION")
                    });
                }

                return response;
            }
            return null;
        }

        public async Task<List<ServicioByProductoTipoEstadoResponse>> GetServicioByProductoTipoAsync(string producto, string tipoServicio)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO", tipoServicio));
            parametros.Add(new SqlParameter("@ID_PROD", producto));
            var listaTipoSolucion = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_SERV_PRODTipoEstadoSelect", parametros);
            if (listaTipoSolucion.Rows.Count > 0)
            {
                var response = new List<ServicioByProductoTipoEstadoResponse>();
                foreach (DataRow row in listaTipoSolucion.Rows)
                {
                    response.Add(new ServicioByProductoTipoEstadoResponse
                    {
                        ServicioId = row.Field<string>("ID_SERV").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim()
                    });
                }
                return response;
            }
            return null;
        }
        public async Task<bool> InsertProductoAsync(Producto request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_PROD", request.IdProducto));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@REPORTE_ASFI", request.ReporteAsfi));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_PRODUCTOSInsert", parametros);
        }
        public async Task<bool> UpdateProductoAsync(Producto request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_PROD", request.IdProducto));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@REPORTE_ASFI", request.ReporteAsfi));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_PRODUCTOSUpdate", parametros);
        }
        public async Task<bool> InsertFullServicioAsync(ServicioRegistroRequest request)
        {
            bool continuar = false;
            var servicioRequest = new Servicio
            {
                ServicioId = "",//request.IdServicio,
                Descripcion = request.Descripcion,
                Tipo = request.TipoServicio,
                Estado = request.Estado,
                TipoAsignacion = request.TipoAsignacion
            };
            var servicioResponse = await InsertServicioAsync(servicioRequest);
            if (string.IsNullOrEmpty(servicioResponse))
            {
                return continuar;
            }
            var productoServicioRequest = new ProductoServicio
            {
                IdProducto = request.IdProducto,
                IdServicio = servicioResponse,
                Estado = request.Estado,
                Tiempo = 0,
                Funcionario = request.FuncionarioRegistro,
                DocumentacionRequerida = request.DocumentacionRequerida
            };
            continuar = await InsertProductoServicioAsync(productoServicioRequest);
            if (!continuar)
            {
                return continuar;
            }
            var tiempoCasoRequest = new TiempoCaso
            {
                IdServicio = servicioResponse,
                UsuarioCreacion = "dbo",
                UsuarioModificacion = "dbo",
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CodTipoCaso = request.CodTipoCaso,
                TiempoResolucion = request.TiempoResolucion
            };
            return await InsertTiempoServicioAsync(tiempoCasoRequest);
        }
        public async Task<bool> UpdateFullServicioAsync(ServicioModificacionRequest request)
        {
            bool continuar = false;
            var servicioRequest = new Servicio
            {
                ServicioId = request.IdServicio,
                Descripcion = request.Descripcion,
                Tipo = request.TipoServicio,
                Estado = request.Estado,
                TipoAsignacion = request.TipoAsignacion
            };
            continuar = await UpdateServicioAsync(servicioRequest);
            if (!continuar)
            {
                return continuar;
            }
            var productoServicioRequest = new ProductoServicio
            {
                IdProducto = request.IdProducto,
                IdServicio = request.IdServicio,
                Estado = request.Estado,
                Tiempo = 0,
                Funcionario = request.FuncionarioRegistro,
                DocumentacionRequerida = request.DocumentacionRequerida
            };
            continuar = await UpdateProductoServicioAsync(productoServicioRequest);
            if (!continuar)
            {
                return continuar;
            }
            var tiempoCasoRequest = new TiempoCaso
            {
                IdServicio = request.IdServicio,
                UsuarioCreacion = "dbo",
                UsuarioModificacion = "dbo",
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                CodTipoCaso = request.CodTipoCaso,
                TiempoResolucion = request.TiempoResolucion
            };
            return await UpdateTiempoServicioAsync(tiempoCasoRequest);
        }
        private async Task<string> InsertServicioAsync(Servicio request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_SERV", request.ServicioId));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@TIPO", request.Tipo));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado));
            parametros.Add(new SqlParameter("@TIPO_ASIGNACION", request.TipoAsignacion));
            var response = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_SERVICIOSInsert", parametros);
            if (response.Rows.Count == 1)
            {
                return response.Rows[0][0].ToString().Trim();
            }
            return null;
        }

        private async Task<bool> InsertProductoServicioAsync(ProductoServicio request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_SERV", request.IdServicio));
            parametros.Add(new SqlParameter("@ID_PROD", request.IdProducto));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado.Equals("1")));
            parametros.Add(new SqlParameter("@DOC_REQ", request.DocumentacionRequerida));
            parametros.Add(new SqlParameter("@TIEMPO", request.Tiempo));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_PROD_SERVInsert", parametros);
        }

        private async Task<bool> InsertTiempoServicioAsync(TiempoCaso request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_SERV", request.IdServicio));
            parametros.Add(new SqlParameter("@COD_TIPOCASO", request.CodTipoCaso));
            parametros.Add(new SqlParameter("@TIEMPO_RESOL", request.TiempoResolucion));
            parametros.Add(new SqlParameter("@USER_CREA", request.UsuarioCreacion));
            parametros.Add(new SqlParameter("@FECHA_CREA", request.FechaCreacion));
            parametros.Add(new SqlParameter("@USER_MOD", request.UsuarioModificacion));
            parametros.Add(new SqlParameter("@FECHA_MOD", request.FechaModificacion));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_TIEMPOS_CASOSInsert", parametros);
        }




        private async Task<bool> UpdateServicioAsync(Servicio request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_SERV", request.ServicioId));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@TIPO", request.Tipo));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado));
            parametros.Add(new SqlParameter("@TIPO_ASIGNACION", request.TipoAsignacion));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_SERVICIOSUpdate", parametros);
        }

        private async Task<bool> UpdateProductoServicioAsync(ProductoServicio request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_SERV", request.IdServicio));
            parametros.Add(new SqlParameter("@ID_PROD", request.IdProducto));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            parametros.Add(new SqlParameter("@ESTADO", request.Estado.Equals("1")));
            parametros.Add(new SqlParameter("@DOC_REQ", request.DocumentacionRequerida));
            parametros.Add(new SqlParameter("@TIEMPO", request.Tiempo));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_PROD_SERVUpdate", parametros);
        }

        private async Task<bool> UpdateTiempoServicioAsync(TiempoCaso request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_SERV", request.IdServicio));
            parametros.Add(new SqlParameter("@COD_TIPOCASO", request.CodTipoCaso));
            parametros.Add(new SqlParameter("@TIEMPO_RESOL", request.TiempoResolucion));
            parametros.Add(new SqlParameter("@USER_MOD", request.UsuarioModificacion));
            parametros.Add(new SqlParameter("@FECHA_MOD", request.FechaModificacion));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_TIEMPOS_CASOSUpdate", parametros);
        }
    }
}

using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.ServiciosSwamp;
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
    public class ServiciosSwampRepository : IServiciosSwampRepository
    {
        private readonly BD_SERVICIOS_SWAMP _bdSrvSwamp;
        private readonly ISmartLinkRepository _smartLinkRepository;
        private readonly IDevolucionRepository _devolucionRepository;
        private readonly ICobroRepository _cobroRepository;

        public ServiciosSwampRepository(BD_SERVICIOS_SWAMP bdSrvSwamp, ISmartLinkRepository smartLinkRepository, IDevolucionRepository devolucionRepository,ICobroRepository cobroRepository)
        {
            this._bdSrvSwamp = bdSrvSwamp;
            this._smartLinkRepository = smartLinkRepository;
            this._devolucionRepository = devolucionRepository;
            this._cobroRepository=cobroRepository;
        }   
        public async Task<List<CuentaContableResponse>> GetAllCuentaContableAsync()
        {
            var listaDevolucionPR = await _bdSrvSwamp.ExecuteSP_DataTable("SERVICIO_CANAL_ListAll");
            if (listaDevolucionPR.Rows.Count > 0)
            {
                var response = new List<CuentaContableResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new CuentaContableResponse
                    {
                        IdCuentaContable = row.Field<int>("ID_SERVICIO"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        CuentaContable = row.Field<string>("DESCRIP"),
                        DebitoAbono = row.Field<bool>("DEB_ABO"),
                        ModeloServiciosCanalesBOL= row.Field<string>("CLAVE_SERV_CAN_BOB"),
                        ModeloServiciosCanalesUSD = row.Field<string>("CLAVE_SERV_CAN_USD")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<List<ParametrosResponse>> GetParametrosSWAsync(string lexico)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO", lexico));
            var listaDevolucionPR = await _bdSrvSwamp.ExecuteSP_DataTable("dbo.LEXICOS_listById", parametros);
            if (listaDevolucionPR.Rows.Count >0)
            {
                var response = new List<ParametrosResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new ParametrosResponse {
                        EstadoPrincipal = row.Field<string>("ESTADO_1"),
                        EstadoSecundario = row.Field<int>("ESTADO_2"),
                        Descripcion = row.Field<string>("DESCRIPCION")
                    }); 
                }
                return response;
            }
            return null;
        }

        public async Task<ServicioCanalResponse> GetServicioCanalByIdAsync(int idServicio)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", idServicio));
            var listaDevolucionPR = await _bdSrvSwamp.ExecuteSP_DataTable("SERVICIO_CANAL_listByID", parametros);
            if (listaDevolucionPR.Rows.Count == 1)
            {
                var response = new ServicioCanalResponse();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.IdServicioCanal = row.Field<int>("ID_SERVICIO");
                    response.Descripcion = row.Field<string>("DESCRIPCION"); 
                    response.IdModeloBOL = row.Field<string>("CLAVE_SERV_CAN_BOB");
                    response.IdModeloUSD = row.Field<string>("CLAVE_SERV_CAN_USD");
                    response.CuentaContable = row.Field<string>("CONTABLE_DESTINO");
                    response.Producto = row.Field<string>("PRODUCTO");
                    response.DebitoAbono = row.Field<bool>("DEB_ABO");
                }
                return response;
            }
            return null;
        }

        public async Task<List<ServicioCanalResponse>> GetServicioCanalByTipoAsync(bool debAbo)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DEB_ABO", debAbo));
            var listaDevolucionPR = await _bdSrvSwamp.ExecuteSP_DataTable("SERVICIO_CANAL_listByDEB_ABO", parametros);
            if (listaDevolucionPR.Rows.Count >0)
            {
                var response = new List<ServicioCanalResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new ServicioCanalResponse { 
                    IdServicioCanal = row.Field<int>("ID_SERVICIO"),
                    Descripcion = row.Field<string>("DESCRIPCION"),
                    IdModeloBOL = row.Field<string>("CLAVE_SERV_CAN_BOB"),
                    IdModeloUSD = row.Field<string>("CLAVE_SERV_CAN_USD"),
                    CuentaContable = row.Field<string>("CONTABLE_DESTINO"),
                    Producto = row.Field<string>("PRODUCTO"),
                    DebitoAbono = row.Field<bool>("DEB_ABO")
                });
            }
                return response;
            }
            return null;
        }

        public async Task<decimal> ValidarImporte(decimal importe,string idProducto,string idServicio,string moneda,bool cobro)
        {
            var tipoCambio = await _smartLinkRepository.GetTipoCambioAsync();
            decimal response = 0M;
            if(tipoCambio == null)
            {
                return -2M;
            }
            decimal maximoMontoBOL = 0M;
            decimal maximoMontoUSD = 0M;
            if (cobro)
            {
                var parametrocobro = await _cobroRepository.CobroByPRAsync(idProducto,idServicio);
                if (parametrocobro == null)
                {
                    return -1M;
                }
                maximoMontoBOL = parametrocobro[0].Importe;
                maximoMontoUSD = maximoMontoBOL / tipoCambio.Venta;
            }
            else
            {
                var parametrodevolucion = await _devolucionRepository.DevolucionByPRAsync(idProducto, idServicio);
                if (parametrodevolucion == null)
                {
                    return -1M;
                }
                maximoMontoBOL = parametrodevolucion[0].Importe;
                maximoMontoUSD = maximoMontoBOL / tipoCambio.Compra;
            }
            if (moneda.Equals("USD"))
            {
                if (importe > maximoMontoUSD)
                {
                    response = maximoMontoUSD;
                }
            }
            else
            {
                if (importe > maximoMontoBOL)
                {
                    response = maximoMontoBOL;
                }
            }
            return response;
        }       

        public async Task<List<Facturacion>> GetFacturacionAllAsync()
        {
            var listaFacturacion = await _bdSrvSwamp.ExecuteSP_DataTable("SERVICIO_FACTURACION_ListAll");
            if (listaFacturacion.Rows.Count > 0)
            {
                var response = new List<Facturacion>();

                foreach (DataRow row in listaFacturacion.Rows)
                {
                    response.Add(new Facturacion
                    {
                        IdFacturacion = row.Field<int>("ID_FACTURACION"),
                        CodigoFacturacion = row.Field<string>("CODIGO_FACTURACION"),
                        Descripcion = row.Field<string>("DESCRIPCION")
                    });
                }
                return response;
            }
            return null;
        }       

    }
}

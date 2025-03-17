using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.DTOs.Area;
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
    public class SarcRepository : ISarcRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public SarcRepository(BD_SARC sarc_Bd)
        {
            this._sarc_Bd = sarc_Bd;
        }

        public async Task<List<AreaResponse>> GetAreaAllAsync()
        {
            var listaArea = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_AREA_SelectAll");
            if (listaArea.Rows.Count > 0)
            {
                var response = new List<AreaResponse>();
                foreach (DataRow row in listaArea.Rows)
                {
                    response.Add(new AreaResponse
                    {
                        IdArea = row.Field<string>("AREA").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Email = row.Field<string>("EMAIL")?.Trim()
                    });

                }
                return response;
            }
            return null;
        }
        public async Task<List<SucursalResponse>> GetSucursalAsync()
        {
            var listaArea = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_SUCURSAL_SelectAll");
            if (listaArea.Rows.Count > 0)
            {
                var response = new List<SucursalResponse>();
                foreach (DataRow row in listaArea.Rows)
                {
                    response.Add(new SucursalResponse
                    {
                        Sucursal = row.Field<string>("SUCURSAL").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        SW_SARC = row.Field<string>("SW_SARC").Trim()
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<List<ParametrosResponse>> GetParametrosSarcAsync(string lexico)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID_PARAMETRO", lexico));
            var listaDevolucionPR = await _sarc_Bd.ExecuteSP_DataTable("SARC.PARAMETROSelectAllById", parametros);
            if (listaDevolucionPR.Rows.Count > 0)
            {
                var response = new List<ParametrosResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new ParametrosResponse
                    {
                        IdParametro = row.Field<int>("ID_LEX"),
                        Descripcion = row.Field<string>("DESCRIPCION")
                    });
                }
                return response;
            }
            return null;
        }

        

        public async Task<List<TipoSolucionResponse>> GetTipoSolucionAllAsync()
        {
            var listaTipoSolucion = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_TIPOSOLUCION_SelectAll_Estado");
            if (listaTipoSolucion.Rows.Count > 0)
            {
                var response = new List<TipoSolucionResponse>();
                foreach (DataRow row in listaTipoSolucion.Rows)
                {
                    response.Add(new TipoSolucionResponse
                    {
                        IdSolucion = row.Field<string>("SOLUCION").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION").Trim(),
                        Estado = row.Field<bool>("ESTADO")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<int> GetCountDevolucionesByAnalistaCuentaAsync(string NroCuenta,string Funcionario)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nro_cta", string.IsNullOrEmpty(NroCuenta)?null: NroCuenta));
            parametros.Add(new SqlParameter("@funcionario", string.IsNullOrEmpty(Funcionario) ? null : Funcionario));
            var listaArea = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_VALIDAR_DEVOLUCIONES",parametros);
            int response = 0;
            if (listaArea.Rows.Count == 1)
            {
                response = listaArea.Rows[0].Field<int>("CONTADOR");
            }
            return response;
        }
        #region validar
        public async Task<int> validarLimitesCuenta(string cuenta, string analista)
        {
            var limiteResponse = await GetParametrosSarcAsync("DEVOLUCION");
            if(limiteResponse == null)
            {
                return -99;
            }
            int limiteAna = int.Parse(limiteResponse[1].Descripcion);
            int limiteCta = int.Parse(limiteResponse[0].Descripcion);

            var cantidadCuenta = await GetCountDevolucionesByAnalistaCuentaAsync(cuenta, "");
            if (cantidadCuenta > limiteCta)
            {
                return -1;
            }
            var cantidadAnalista= await GetCountDevolucionesByAnalistaCuentaAsync("", analista);
            if (cantidadAnalista > limiteAna)
            {
                return 1;
            }
            return 0;
        }
        #endregion       
        public async Task<List<Cargo>> GetCargoAllAsync()
        {
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_TIPO_FUNC_SelectAll");
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = new List<Cargo>();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response.Add(new Cargo
                    {
                        IdCargo = row.Field<string>("COD_TIPO_FUNC"),
                        NombreCargo = row.Field<string>("TIPO")
                    });
                }
                return response;
            }
            return null;

        }
        public async Task<bool> InsertClienteAsync(Cliente request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@chrIDC_CLIE", request.ClienteIdc));
            parametros.Add(new SqlParameter("@chrIDC_TIPO", request.ClienteTipo));
            parametros.Add(new SqlParameter("@chrIDC_EXT", request.ClienteExtension));
            parametros.Add(new SqlParameter("@chrPATERNO", request.Paterno));
            parametros.Add(new SqlParameter("@chrMATERNO", request.Materno));
            parametros.Add(new SqlParameter("@chrNOMBRES", request.Nombres));
            parametros.Add(new SqlParameter("@chrNOMCLIENTE",(request.Paterno+" "+ request.Materno+" "+request.Nombres).Trim()));
            parametros.Add(new SqlParameter("@chrDIRECCION", request.Direccion));
            parametros.Add(new SqlParameter("@chrDIRECCION_ENVIO", request.DireccionEnvio));
            parametros.Add(new SqlParameter("@chrFAX", request.Fax));
            parametros.Add(new SqlParameter("@chrTELEFONO1", request.TelefonoPrincipal));
            parametros.Add(new SqlParameter("@chrTELEFONO2", request.TelefonoRespaldo));
            parametros.Add(new SqlParameter("@chrCELULAR1", request.CelularPrincipal));
            parametros.Add(new SqlParameter("@chrCELULAR2", request.CelularRespaldo));
            parametros.Add(new SqlParameter("@chrEMAIL", request.Email));
            return await _sarc_Bd.ExecuteSP("SARC.SARC_CLIENTE_Insert", parametros);
        }

        public async Task<List<ParametrosError>> GetErrorAllByTipoAsync(string tipo)
        {            
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO", tipo));
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_TIPO_ERRORSelectByTipo",parametros);
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = new List<ParametrosError>();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response.Add(new ParametrosError
                    {
                        ErrorId = row.Field<string>("ID_ERROR"),
                        ErrorDescripcion = row.Field<string>("DESCRIPCION")
                    });
                }
                return response;
            }
            return null;
        } 
    }
}

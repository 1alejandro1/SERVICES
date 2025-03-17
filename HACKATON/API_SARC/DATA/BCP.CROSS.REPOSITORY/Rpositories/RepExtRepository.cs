using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.RepExt;
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
    public class RepExtRepository : IRepExtRepository
    {
        private readonly BD_REPEXT _bdRepExt;

        public RepExtRepository(BD_REPEXT bdRepExt)
        {
            this._bdRepExt = bdRepExt;
        }

        public async Task<List<GetClienteByCuentaResponse>> GetDatosCuentaAsync(string cuentaRepExt)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CUENTA", cuentaRepExt));
            var listaDevolucionPR = await _bdRepExt.ExecuteSP_DataTable("Repxt.SP_SARC_listClienteByCuenta", parametros);
            if (listaDevolucionPR.Rows.Count > 0)
            {
                var response = new List<GetClienteByCuentaResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new GetClienteByCuentaResponse
                    {
                        TipoBanca = row.Field<string>("BANCA"),
                        CIC= row.Field<string>("CL_CIC")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<List<ValidaCuentaResponse>> ValidarCuentaAsync(ValidaCuentaRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CUENTA", request.NroCuenta));
            parametros.Add(new SqlParameter("@IDC", request.IDC?.Trim().PadLeft(8, '0')));
            parametros.Add(new SqlParameter("@TIPO", request.IdcTipo));
            parametros.Add(new SqlParameter("@EXT", request.IdcExtenion));
            var listaDevolucionPR = await _bdRepExt.ExecuteSP_DataTable("Repxt.SP_SARC_VALIDA_CUENTA", parametros);
            if (listaDevolucionPR.Rows.Count > 0)
            {
                var response = new List<ValidaCuentaResponse>();

                foreach (DataRow row in listaDevolucionPR.Rows)
                {
                    response.Add(new ValidaCuentaResponse
                    {
                        CuentaRepExt = row.Field<string>("CP_NROCUENTA")
                    });
                }
                return response;
            }
            return null;
        }
    }
}

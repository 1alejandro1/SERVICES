using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Tarifario;
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
    public class TarifarioRepository: ITarifarioRepository
    {
        private readonly BD_SERVICIOS_SWAMP _bdSrvSwamp;

        public TarifarioRepository(BD_SERVICIOS_SWAMP bdSrvSwamp)
        {
            this._bdSrvSwamp = bdSrvSwamp;
        }
        public async Task<List<Tarifario>> GetAllTarifarioAsync()
        {
            var listaTarifario = await _bdSrvSwamp.ExecuteSP_DataTable("TARIFARIO_listAll");
            if (listaTarifario.Rows.Count > 0)
            {
                var response = new List<Tarifario>();

                foreach (DataRow row in listaTarifario.Rows)
                {
                    response.Add(new Tarifario
                    {
                        TarifarioId = row.Field<int>("ID_TARIFARIO"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        TipoCodigo = row.Field<string>("TIPO"),
                        TipoDescripcion = row.Field<string>("DESC_TIPO"),
                        Importe = row.Field<decimal>("IMPORTE")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<bool> InsertTarifarioAsync(TarifarioRegistro request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@TIPO", request.TipoCodigo));
            parametros.Add(new SqlParameter("@IMPORTE", request.Importe));
            parametros.Add(new SqlParameter("@FUNC", request.FuncionarioRegistro));
            return await _bdSrvSwamp.ExecuteSP("TARIFARIO_Insert", parametros);
        }
        public async Task<bool> UpdateTarifarioAsync(TarifarioModificacion request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", request.TarifarioId));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@TIPO", request.TipoCodigo));
            parametros.Add(new SqlParameter("@IMPORTE", request.Importe));
            parametros.Add(new SqlParameter("@FUNC", request.FuncionarioRegistro));
            return await _bdSrvSwamp.ExecuteSP("TARIFARIO_Update", parametros);
        }
        public async Task<bool> DeshabilitarTarifarioAsync(TarifiarioDeshabilitar request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@ID", request.TarifarioId));
            parametros.Add(new SqlParameter("@FUNC", request.Funcionario));
            return await _bdSrvSwamp.ExecuteSP("TARIFARIO_Delete", parametros);
        }
    }
}

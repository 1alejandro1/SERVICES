using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Especialidad;
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
    public class EspecialidadRepository: IEspecialidadRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public EspecialidadRepository(BD_SARC sarc_Bd)
        {
            this._sarc_Bd = sarc_Bd;
        }
        public async Task<List<Especialidad>> GetTipoCasoAllAsync()
        {
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_TIPOCASOSelectAll");
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = new List<Especialidad>();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response.Add(new Especialidad
                    {
                        CodTipoCaso = row.Field<string>("COD_TIPOCASO"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        UsuarioCreacion = row.Field<string>("USER_CREA"),
                        FechaCreacion = row.Field<DateTime>("FECHA_CREA"),
                        UsuarioModificacion = row.Field<string>("USER_MOD"),
                        FechaModificacion = row.Field<DateTime>("FECHA_MOD")
                    });
                }
                return response;
            }
            return null;

        }

        public async Task<List<TipoCasoTiempo>> GetTipoCasoTiemposAllAsync()
        {
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_TIPOCASO_SelectAll");
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = new List<TipoCasoTiempo>();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response.Add(new TipoCasoTiempo
                    {
                        CodTipoCaso = row.Field<string>("COD_TIPOCASO"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        Tiempo = row.Field<int>("TIEMPO")
                    });
                }
                return response;
            }
            return null;

        }

        public async Task<bool> InsertTipoCasoAsync(TipoCasoRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CODIGO", request.CodTipoCaso));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@TIEMPO", request.Tiempo));
            parametros.Add(new SqlParameter("@USER", request.UsuarioCreacion));
            return await _sarc_Bd.ExecuteSP("SARC.SP_TIPO_CASO_Insert", parametros);
        }
        public async Task<bool> UpdateTipoCasoAsync(TipoCasoRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@CODIGO", request.CodTipoCaso));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@TIEMPO", request.Tiempo));
            parametros.Add(new SqlParameter("@USER", request.UsuarioCreacion));
            return await _sarc_Bd.ExecuteSP("SARC.SP_TIPO_CASO_Update", parametros);
        }
        public async Task<bool> InsertFuncionarioEspecialidadAsync(Especialidad request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@COD_TIPOCASO", request.CodTipoCaso));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@USER_CREA", request.UsuarioCreacion));
            parametros.Add(new SqlParameter("@FECHA_CREA", request.FechaCreacion));
            parametros.Add(new SqlParameter("@USER_MOD", request.UsuarioModificacion));
            parametros.Add(new SqlParameter("@FECHA_MOD", request.FechaModificacion));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_TIPOCASOInsert", parametros);
        }
        public async Task<bool> DeleteFuncionarioEspecialidadAsync(string codFuncionario)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@COD_FUNCIONARIO", codFuncionario));
            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_FUNC_TIPOCASODeleteByFUNC", parametros);
        }
    }
}

using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Funcionario;
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
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public UsuarioRepository(BD_SARC sarc_Bd)
        {
            this._sarc_Bd = sarc_Bd;
        }

        public async Task<List<Usuario>> GetUsuarioAllByCargoAsync(bool analista)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO", analista ? "0" : "1"));
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_FUNCIONARIOS_SelectAll", parametros);
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = new List<Usuario>();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response.Add(new Usuario
                    {
                        Matricula = row.Field<string>("CODIGO").Trim(),
                        NombreCompleto = row.Field<string>("NOMBRE").Trim(),
                        Area = row.Field<string>("AREA").Trim(),
                        Sucursal = row.Field<string>("SUCURSAL").Trim(),
                        Interno = row.Field<string>("INTERNO")==null?"": row.Field<string>("INTERNO").Trim(),
                        Casilla = row.Field<string>("CASILLA") == null ? "" : row.Field<string>("CASILLA").Trim(),
                        Email = row.Field<string>("MAIL").Trim(),
                        Celular = row.Field<string>("CELULAR") == null ? "" : row.Field<string>("CELULAR").Trim(),
                        Telefono = row.Field<string>("TELEFONO") == null ? "" : row.Field<string>("TELEFONO").Trim(),
                        Estado = row.Field<string>("ESTADO").Trim(),
                        Cargo = row.Field<string>("TIPO").Trim()
                    });
                }
                return response;
            }
            return null;

        }
        public async Task<bool> InsertUsuarioAsync(UsuarioRegistro request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@COD_FUNCIONARIO", request.Matricula));
            parametros.Add(new SqlParameter("@NOMBRE", request.NombreCompleto));
            parametros.Add(new SqlParameter("@COD_AREA", request.CodigoArea));
            parametros.Add(new SqlParameter("@SUCURSAL", request.CodigoSucursal));
            parametros.Add(new SqlParameter("@INTERNO", request.Interno));
            parametros.Add(new SqlParameter("@CASILLA", request.Casilla));
            parametros.Add(new SqlParameter("@EMAIL", request.Email));
            parametros.Add(new SqlParameter("@CELULAR", request.Celular));
            parametros.Add(new SqlParameter("@TELEFONO_DOM", request.Telefono));
            parametros.Add(new SqlParameter("@COD_TIPO_FUNC", request.CodigoCargo));
            parametros.Add(new SqlParameter("@USR_AD", request.CodigoFuncioanrio));
            return await _sarc_Bd.ExecuteSP("SARC.SP_INSERTAR_USUARIO", parametros);
        }
        public async Task<bool> UpdateUsuarioAsync(UsuarioModificacion request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@COD_FUNCIONARIO", request.Matricula));
            parametros.Add(new SqlParameter("@NOMBRE", request.NombreCompleto));
            parametros.Add(new SqlParameter("@COD_AREA", request.CodigoArea));
            parametros.Add(new SqlParameter("@SUCURSAL", request.CodigoSucursal));
            parametros.Add(new SqlParameter("@INTERNO", request.Interno));
            parametros.Add(new SqlParameter("@CASILLA", request.Casilla));
            parametros.Add(new SqlParameter("@EMAIL", request.Email));
            parametros.Add(new SqlParameter("@CELULAR", request.Celular));
            parametros.Add(new SqlParameter("@TELEFONO_DOM", request.Telefono));
            parametros.Add(new SqlParameter("@COD_TIPO_FUNC", request.CodigoCargo));
            parametros.Add(new SqlParameter("@USR_AD", request.CodigoFuncioanrio));
            parametros.Add(new SqlParameter("@ESTADO", request.CodigoEstado));
            return await _sarc_Bd.ExecuteSP("SARC.SP_ACTUALIZAR_USUARIO", parametros);
        }
        public async Task<List<FuncionarioArea>> GetDatosFuncionarioAsync(string Matricula)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@MATRICULA", Matricula));
            var consultaArea = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_FUNCIONARIOSelectByMatricula", parametros);
            if (consultaArea.Rows.Count > 0)
            {
                var response = new List<FuncionarioArea>();
                foreach (DataRow row in consultaArea.Rows)
                {
                    response.Add(new FuncionarioArea
                    {
                        Matricula = row.Field<string>("MATRICULA").Trim(),
                        NombreFuncionario = row.Field<string>("NOMBRE").Trim(),
                        CodigoArea = row.Field<string>("COD_AREA").Trim(),
                        DescripcionArea = row.Field<string>("DESCRIPCION_AREA").Trim(),
                        Email = row.Field<string>("EMAIL").Trim()
                    });
                    return response;
                }
            }
            return null;
        }
    }
}

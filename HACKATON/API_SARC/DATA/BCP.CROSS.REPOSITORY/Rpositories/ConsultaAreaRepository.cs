using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.ConsultaArea;
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
    public class ConsultaAreaRepository: IConsultaAreaRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public ConsultaAreaRepository(BD_SARC sarc_Bd)
        {
            this._sarc_Bd = sarc_Bd;
        }
        public async Task<List<AreaRegistro>> GetAreaByFuncionarioAsync(string funcionario)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@USR_CONSULTA", funcionario));
            var listaArea = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_CONSULTA_AREASelectByUsrConsulta", parametros);
            if (listaArea.Rows.Count > 0)
            {
                var response = new List<AreaRegistro>();
                foreach (DataRow row in listaArea.Rows)
                {
                    response.Add(new AreaRegistro
                    {
                        NRO_CARTA = row.Field<string>("NRO_CARTA").Trim(),
                        USR_CONSULTA = row.Field<string>("USR_CONSULTA"),
                        AREA_CONSULTA = row.Field<string>("AREA_CONSULTA"),
                        RESPONSABLE = row.Field<string>("RESPONSABLE"),
                        DESCRIPCION = row.Field<string>("DESCRIPCION"),
                        FEC_INI = row.Field<DateTime>("FEC_INI"),
                        PRIORIDAD = row.Field<string>("PRIORIDAD"),
                        RESPUESTA = row.Field<string>("RESPUESTA"),
                        NIVEL = row.Field<int>("NIVEL")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<List<AlertaArea>> GetAlertaAreaRespuestaAsync(string funcionario, bool respuesta)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@FUN_ATN", funcionario));
            parametros.Add(new SqlParameter("@RESPUESTA", respuesta ? "S" : "N"));
            var listaAlerta = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_CONSULTA_AREASelectByFUNC_ATN", parametros);
            if (listaAlerta.Rows.Count > 0)
            {
                var response = new List<AlertaArea>();
                foreach (DataRow row in listaAlerta.Rows)
                {
                    response.Add(new AlertaArea
                    {
                        NroCaso = row.Field<string>("NRO_CARTA").Trim(),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        Motivo = row.Field<string>("MOTIVO"),
                        TiempoAtencioArea = row.Field<int>("TIEMPO_ATN_AREA"),
                        TiempoAtencionSarc = row.Field<int>("TIEMPO_ATN_SARC")
                    });
                }
                return response;
            }
            return null;
        }

        public async Task<ConsultaArea> GetConsultaAreaByCartaAsync(string NroCarta)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NRO_CARTA", NroCarta));
            var consultaArea = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_CONSULTA_AREASelectByNROCARTA", parametros);
            if (consultaArea.Rows.Count == 1)
            {
                var response = new ConsultaArea();
                foreach (DataRow row in consultaArea.Rows)
                {
                    response.NroCarta = row.Field<string>("NRO_CARTA").Trim();
                    response.UsuarioConsulta = row.Field<string>("USR_CONSULTA").Trim();
                    response.AreaConsulta = row.Field<string>("AREA_CONSULTA").Trim();
                    response.Responsable = row.Field<string>("RESPONSABLE").Trim();
                    response.Descripcion = row.Field<string>("DESCRIPCION").Trim();
                    response.FechaInicio = row.Field<DateTime>("FEC_INI");
                    response.Prioridad = row.Field<string>("PRIORIDAD").Trim();
                    response.Respuesta = row.Field<string>("RESPUESTA").Trim();
                    response.Nivel = row.Field<int>("NIVEL");
                }
                return response;
            }
            return null;
        }

        public async Task<bool> UpdateCasoRespuestaAreaAsync(UpdateCasoRespuestaArea casoRequest)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@NRO_CARTA", casoRequest.NroCaso));
            parameters.Add(new SqlParameter("@DESC_RESP", casoRequest.RespuestaArea));

            return await _sarc_Bd.ExecuteSP("SARC.SP_SARC_UPDATE_AREASelectByNROCARTA", parameters);
        }
    }
}

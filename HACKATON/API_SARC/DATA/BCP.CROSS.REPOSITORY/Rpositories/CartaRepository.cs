using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Carta;
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
    public class CartaRepository: ICartaRepository
    {
        private readonly BD_SARC _sarc_Bd;
        public CartaRepository(BD_SARC sarc_Bd)
        {
            this._sarc_Bd = sarc_Bd;
        }
        public async Task<List<Carta>> GetCartaAllAsync()
        {
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_CARTA_SelectAll");
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = new List<Carta>();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response.Add(new Carta
                    {
                        CartaId = row.Field<string>("CARTA"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        Cuerpo = row.Field<string>("CUERPO")
                    });
                }
                return response;
            }
            return null;
        }
        public async Task<Carta> GetCartaByIdAsync(string cartaId)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO_CARTA", cartaId));
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_VALIDA_CARTA", parametros);
            if (listaTipoCaso.Rows.Count == 1)
            {
                var response = new Carta();
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response = new Carta
                    {
                        CartaId = row.Field<string>("TIPO_CARTA"),
                        Descripcion = row.Field<string>("DESCRIPCION"),
                        Cuerpo = row.Field<string>("CUERPO")
                    };
                }
                return response;
            }
            return null;
        }

        public async Task<string> GetCartaFileAsync(CartaFileRequest request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NRO_CARTA", request.NroCarta));
            parametros.Add(new SqlParameter("TIPO_CARTA", request.TipoCarta));
            var listaTipoCaso = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_CARTA_REMPLAZADA", parametros);
            if (listaTipoCaso.Rows.Count > 0)
            {
                var response = "";
                foreach (DataRow row in listaTipoCaso.Rows)
                {
                    response = row.Field<string>("CUERPO");
                }
                return response;
            }
            return null; //TODO: implementar y o modificar storage procedure
        }

        public async Task<bool> InsertCartaAsync(Carta request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO_CARTA", request.CartaId));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@CUERPO", request.Cuerpo));
            return await _sarc_Bd.ExecuteSP("SARC.SP_CARTA_Insert", parametros);
        }
        public async Task<bool> UpdateCartaAsync(Carta request)
        {
            var parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TIPO_CARTA", request.CartaId));
            parametros.Add(new SqlParameter("@DESCRIPCION", request.Descripcion));
            parametros.Add(new SqlParameter("@CUERPO", request.Cuerpo));
            return await _sarc_Bd.ExecuteSP("SARC.SP_CARTA_Update", parametros);
        }
    }
}

using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY
{
    public class ViasNotificacionRepository: IViasNotificacionRepository
    {
        private readonly BD_SARC _sarc_Bd;

        public ViasNotificacionRepository(BD_SARC sarc_bd)
        {
            _sarc_Bd = sarc_bd;
        }

        public async Task<List<ViasNotificacion>> GetViasNotificacionAsync()
        {

            var viasNotificacionTable = await _sarc_Bd.ExecuteSP_DataTable("SARC.SP_SARC_GetViasNotificacion");


            if (viasNotificacionTable.Rows.Count > 0)
            {
                var vias = new List<ViasNotificacion>();
                
                foreach (DataRow row in viasNotificacionTable.Rows)
                {
                    var via = new ViasNotificacion
                    {
                        Id = row.Field<int>("ID"),
                        Via = row.Field<string>("VIA") == null ? "" : row.Field<string>("VIA").Trim(),
                        Estado = row.Field<bool>("ESTADO"),
                    };
                    vias.Add(via);
                }
                return vias;
            }

            return null;
        }
    }
}

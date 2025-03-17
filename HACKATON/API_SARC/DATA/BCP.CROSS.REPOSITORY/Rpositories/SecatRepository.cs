using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Secat;
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
    public class SecatRepository : ISecatRepository
    {
        private readonly BD_SECAT _secat_Bd;
        public SecatRepository(BD_SECAT secat_Bd)
        {
            this._secat_Bd = secat_Bd;
        }

        public async Task<List<ATMResponse>> GetATMAsync()
        {
            var listaArea = await _secat_Bd.ExecuteSP_DataTable("dbo.SARC_ATM_List");
            if (listaArea.Rows.Count > 0)
            {
                var response = new List<ATMResponse>();
                foreach (DataRow row in listaArea.Rows)
                {
                    response.Add(new ATMResponse
                    {
                        Id = row.Field<string>("ATMID").Trim(),
                        Direccion = row.Field<string>("DIRECCIONATM").Trim(),
                        Sucursal = row.Field<string>("SUCURSAL").Trim(),
                        Ubicacion = row.Field<string>("UBICACION").Trim()
                    });

                }
                return response;
            }
            return null;
        }
    }
}

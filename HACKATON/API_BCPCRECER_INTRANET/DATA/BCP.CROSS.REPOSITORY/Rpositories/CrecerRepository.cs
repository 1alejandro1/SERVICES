using BCP.CROSS.DATAACCESS;
using BCP.CROSS.MODELS.Common;
using BCP.CROSS.MODELS.Crecer;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCP.CROSS.MODELS.DTOs.Crecer;

namespace BCP.CROSS.REPOSITORY.Rpositories
{
    public class CrecerRepository : ICrecerRepository
    {
        private readonly BD_BCPCRECER _crecer_Bd;
        public CrecerRepository(BD_BCPCRECER crecer_Bd)
        {
            this._crecer_Bd = crecer_Bd;
        }

        public async Task<List<CategoriasResponse>> GetCategoriasAsync()
        {
            var listaArea = await _crecer_Bd.ExecuteSP_DataTable("dbo.Categorias_list");
            if (listaArea.Rows.Count > 0)
            {
                var response = new List<CategoriasResponse>();
                foreach (DataRow row in listaArea.Rows)
                {
                    response.Add(new CategoriasResponse
                    {
                        codCategoria = row.Field<string>("codCategoria_VC").Trim(),
                        Descripcion = row.Field<string>("descripcion_VC").Trim(),
                        Color = row.Field<string>("color_VC").Trim(),
                        Imagen = row.Field<string>("imagen_VC").Trim()
                    });

                }
                return response;
            }
            return null;
        }
        public async Task<List<ObtieneEmpresasByCategoriaCiudadResponse>> ObtieneEmpresasByCategoriaCiudadAsync(ObtieneEmpresasByCategoriaCiudadRequest request)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@codCategoria_VC", request.CodCategoria));
            parameters.Add(new SqlParameter("@codCiudad_VC", request.CodCiudad));           

            var Cobros = await _crecer_Bd.ExecuteSP_DataTable("[dbo].[Empresas_listbycodCategoriacodCiudad]", parameters);

            if (Cobros.Rows.Count > 0)
            {
                List<ObtieneEmpresasByCategoriaCiudadResponse> response = new List<ObtieneEmpresasByCategoriaCiudadResponse>();
                foreach (DataRow row in Cobros.Rows)
                {
                    response.Add(new ObtieneEmpresasByCategoriaCiudadResponse
                    {
                        CodEmpresa = row.Field<int>("codEmpresa_IN"),
                        NombreEmpresa = row.Field<string>("nombreEmpresa_VC") == null ? "" : row.Field<string>("nombreEmpresa_VC").Trim(),
                        Descripcion = row.Field<string>("descripcion_VC") == null ? "" : row.Field<string>("descripcion_VC").Trim(),
                        Imagen = row.Field<string>("imagen_VC") == null ? "" : row.Field<string>("imagen_VC").Trim(),
                        Color = row.Field<string>("color_VC") == null ? "" : row.Field<string>("color_VC").Trim(),
                        CodCategoria = row.Field<string>("codCategoria_VC") == null ? "" : row.Field<string>("codCategoria_VC").Trim()                      
                    });
                }
                return response;
            }
            return null;
        }

    }
}

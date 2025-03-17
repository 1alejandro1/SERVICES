using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Crecer
{
    public class ObtieneEmpresasByCategoriaCiudadResponse
    {
        public int CodEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Color { get; set; }
        public string CodCategoria { get; set; }
    }
}

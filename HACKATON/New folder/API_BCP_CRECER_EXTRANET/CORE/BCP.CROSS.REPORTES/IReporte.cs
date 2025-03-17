using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPORTES
{
    public interface IReporte
    {
        Task<string> GetReporteAsync(object request, string tipoReporte);
    }
}

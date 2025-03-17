using BCP.CROSS.MODELS.ConsultaArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IConsultaAreaRepository
    {
        Task<List<AreaRegistro>> GetAreaByFuncionarioAsync(string funcionario);
        Task<List<AlertaArea>> GetAlertaAreaRespuestaAsync(string funcionario, bool respuesta);
        Task<ConsultaArea> GetConsultaAreaByCartaAsync(string NroCarta);
        Task<bool> UpdateCasoRespuestaAreaAsync(UpdateCasoRespuestaArea casoRequest);
    }
}

using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.SERVICES.Contracts
{
    public interface IUsuarioService
    {
        Task<ServiceResponse<List<Usuario>>> GetUsuarioAllByCargoAsync(CargoUsuarioRequest request, string responseId);
        Task<ServiceResponse<bool>> InsertUsuarioAsync(UsuarioRegistro request, string responseId);
        Task<ServiceResponse<bool>> UpdateUsuarioAsync(UsuarioModificacion request, string responseId);
        Task<ServiceResponse<List<FuncionarioArea>>> GetDatosFuncionarioAreaAsync(MatriculaArea request, string requestId);
    }
}

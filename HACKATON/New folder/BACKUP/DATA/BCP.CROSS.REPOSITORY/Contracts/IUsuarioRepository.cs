using BCP.CROSS.MODELS.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.REPOSITORY.Contracts
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetUsuarioAllByCargoAsync(bool analista);
        Task<bool> InsertUsuarioAsync(UsuarioRegistro request);
        Task<bool> UpdateUsuarioAsync(UsuarioModificacion request);
        Task<List<FuncionarioArea>> GetDatosFuncionarioAsync(string Matricula);
       
    }
}

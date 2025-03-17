using BCP.CROSS.COMMON;
using BCP.CROSS.MODELS.Funcionario;
using BCP.CROSS.REPOSITORY.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.SERVICES.Contracts;

namespace WEBAPI.SERVICES.Services
{
    internal class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _sarcRepository;
        public UsuarioService(IUsuarioRepository sarcRepository)
        {
            this._sarcRepository = sarcRepository;
        }
        public async Task<ServiceResponse<List<Usuario>>> GetUsuarioAllByCargoAsync(CargoUsuarioRequest request, string responseId)
        {
            var usuario = await _sarcRepository.GetUsuarioAllByCargoAsync(request.Analista);
            var response = new ServiceResponse<List<Usuario>>()
            {
                Data = usuario,
                Meta =
                {
                    Msj=usuario!=null ? "Success" : "No se encontraron registros de usuarios para ese tipo de cargo",
                    ResponseId=responseId,
                    StatusCode=usuario!=null?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> InsertUsuarioAsync(UsuarioRegistro request, string responseId)
        {
            var usuario = await _sarcRepository.InsertUsuarioAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = usuario,
                Meta =
                {
                    Msj=usuario ? "Success" : "No se registro al usuario",
                    ResponseId=responseId,
                    StatusCode=usuario?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateUsuarioAsync(UsuarioModificacion request, string responseId)
        {
            var usuario = await _sarcRepository.UpdateUsuarioAsync(request);
            var response = new ServiceResponse<bool>()
            {
                Data = usuario,
                Meta =
                {
                    Msj=usuario ? "Success" : "No se realizo la modificacion del usuario",
                    ResponseId=responseId,
                    StatusCode=usuario?200:404
                }
            };
            return response;
        }

        public async Task<ServiceResponse<List<FuncionarioArea>>> GetDatosFuncionarioAreaAsync(MatriculaArea request, string requestId)
        {
            var responseArea = await _sarcRepository.GetDatosFuncionarioAsync(request.Matricula);
            var response = new ServiceResponse<List<FuncionarioArea>>
            {
                Data = responseArea,
                Meta = {
                    Msj = responseArea != null ? "Success" : "Funcionario no encontrado",
                    ResponseId = requestId,
                    StatusCode = responseArea != null ? 200 : 404
                }
            };

            return response;
        }
    }
}

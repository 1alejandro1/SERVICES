using MICRE.ABSTRACTION.ENTITIES;
using MICRE.ABSTRACTION.ENTITIES.Response;
using MICRE.APPLICATION.CONNECTIONS.SERVICES;
using NETCORE.Models;

namespace MICRE_APP_EXCEPCIONES.Commons
{
    public interface IUserAutonomyVerification
    {
        public Task<bool> VerifyUserAutonomy(ExcepcionesFiltroUsuario _dato, int _idExcepcion, int _idProducto, decimal _importeSolicitado, List<ExcepcionesDetalleResponse> _excepcionesDetalle, List<ComboExcepcionesResponse> _productosDetalle);
        public bool VerifyRequestAmount(ExcepcionesFiltroUsuarioProducto _producto, decimal _importeSolicitud);
        public Task<List<ExcepcionCliente>> VerifyUserAutonomi(ExcepcionesFiltroUsuario _dato, List<ExcepcionCliente> _excepciones,string UsuarioRegistro);
    }
    public class UserAutonomyVerification : IUserAutonomyVerification
    {
        private readonly IExcepcionesConnectionServices _connectionService;
        public UserAutonomyVerification(IExcepcionesConnectionServices connectionService) { this._connectionService = connectionService; }

        public async Task<List<ExcepcionCliente>> VerifyUserAutonomi(ExcepcionesFiltroUsuario _dato, List<ExcepcionCliente> _excepciones, string UsuarioRegistro)
        {
            List<ExcepcionCliente> _response = new();
            try
            {
                if (_dato.TipoExcepcion.Count > 0)
                {
                    GenericResponse<List<ExcepcionesDetalleResponse>> _responseExcepciones = await this._connectionService.ObtenerExcepcionesList();
                    GenericResponse<List<ComboExcepcionesResponse>> _responseProductos = await this._connectionService.ObtenerProductosList();
                    foreach (var item in _excepciones)
                    {
                        item.Autonomia = await VerifyUserAutonomy(_dato, item.IdExcepcion, item.IdProducto, item.MontoSolicitadoUSD, _responseExcepciones.data ?? new List<ExcepcionesDetalleResponse>(), _responseProductos.data ?? new List<ComboExcepcionesResponse>());
                        if (item.UsuarioRegistro.Equals(UsuarioRegistro))
                        {
                            item.Autonomia = false;
                            item.AutonomiaPermiso = true;
                        }
                        else
                        {
                            item.Autonomia = false;
                            item.AutonomiaPermiso = false;
                        }
                    }
                    _response = _excepciones;
                }
                else
                {
                    _response = _excepciones;
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return _response;
        }

        public async Task<bool> VerifyUserAutonomy(ExcepcionesFiltroUsuario _dato, int _idExcepcion, int _idProducto, decimal _importeSolicitado, List<ExcepcionesDetalleResponse> _excepcionesDetalle, List<ComboExcepcionesResponse> _productosDetalle)
        {
            try
            {
                if (_dato.TipoExcepcion.Count > 0)
                {
                    var _tipoExcepcion = _excepcionesDetalle?.FirstOrDefault(x => x.IdDetalle == _idExcepcion);
                    var _tipoProducto = _productosDetalle?.FirstOrDefault(x => x.IdParametro == _idProducto);
                    if (_tipoExcepcion is not null && _tipoProducto is not null)
                    {
                        var _excepcionActivo = _dato.TipoExcepcion.FirstOrDefault(x => x.IdTipoExcepcion == _tipoExcepcion.IdTipo);
                        var _productoActivo = _excepcionActivo?.ProductoExcepcion[0].FirstOrDefault(x => x.CodigoProducto == _tipoProducto?.Codigo);
                        if (_tipoProducto?.Codigo == "04")
                        {
                            if ((_tipoProducto?.Descripcion?.Contains("(con garantía)") ?? false))
                                _productoActivo = _excepcionActivo?.ProductoExcepcion[0].FirstOrDefault(x => x.Grupo == _tipoProducto?.Codigo);
                            else
                                _productoActivo = _excepcionActivo?.ProductoExcepcion[0].FirstOrDefault(x => x.Grupo == "02");
                        }

                        if (_excepcionActivo is null || _productoActivo is null)
                            return true;
                        return await Task.Run(() => !VerifyRequestAmount(_productoActivo, _importeSolicitado));
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyRequestAmount(ExcepcionesFiltroUsuarioProducto _producto, decimal _importeSolicitud)
        {
            bool _response = false;
            try
            {

                if ((_importeSolicitud < _producto.Menor) && (_producto.Menor != 0 && _producto.MenorIgual == 0 && _producto.Mayor == 0 && _producto.MayorIgual == 0))
                    return true;

                if ((_importeSolicitud <= _producto.MenorIgual) && (_producto.Menor == 0 && _producto.MenorIgual != 0 && _producto.Mayor == 0 && _producto.MayorIgual == 0))
                    return true;

                if ((_importeSolicitud > _producto.Mayor) && (_producto.Menor == 0 && _producto.MenorIgual == 0 && _producto.Mayor != 0 && _producto.MayorIgual == 0))
                    return true;

                if ((_importeSolicitud >= _producto.MayorIgual) && (_producto.Menor == 0 && _producto.MenorIgual == 0 && _producto.Mayor == 0 && _producto.MayorIgual != 0))
                    return true;

                if ((_producto.Menor != 0 && _producto.MenorIgual == 0 && _producto.Mayor != 0 && _producto.MayorIgual == 0) && (_importeSolicitud > _producto.Mayor && _importeSolicitud < _producto.Menor))
                    return true;

                if ((_producto.Menor == 0 && _producto.MenorIgual != 0 && _producto.Mayor == 0 && _producto.MayorIgual != 0) && (_importeSolicitud >= _producto.MayorIgual && _importeSolicitud <= _producto.MenorIgual))
                    return true;

                if ((_producto.Menor != 0 && _producto.MenorIgual == 0 && _producto.Mayor == 0 && _producto.MayorIgual != 0) && (_importeSolicitud >= _producto.MayorIgual && _importeSolicitud < _producto.Menor))
                    return true;

                if ((_producto.Menor == 0 && _producto.MenorIgual != 0 && _producto.Mayor != 0 && _producto.MayorIgual == 0) && (_importeSolicitud > _producto.Mayor && _importeSolicitud <= _producto.MenorIgual))
                    return true;
            }
            catch (Exception)
            {
                throw;
            }
            return _response;
        }
    }
}

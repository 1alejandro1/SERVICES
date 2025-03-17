using AutoMapper;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.DTOs.Caso;

namespace BCP.CROSS.COMMON
{
    public class ProfileAutoMapperV2 : Profile
    {

        public ProfileAutoMapperV2()
        {
            CreateMap<CreateCasoDTOV2, Caso>()
            .ForMember(c => c.FuncionarioRegistro, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FechaRegistro, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.HoraAsignacion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.ClienteIdc, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.ClienteIdcTipo, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.ClienteIdcExtension, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.ProductoId, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.ServicioId, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.Empresa, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FuncinarioAtencion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FechaAsignacion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.HoraAsignacion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FechaIncioAtencion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.HoraIncioAtension, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FechaFinAtencion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.HoraFinAtencion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FechaDeathLine, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.HoraDeathLine, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.NroCuenta, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.NroTarjeta, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.Moneda, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.FechaTxn, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.HoraTxn, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.InformacionAdicional, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.AtmSucursal, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.AtmUbicacion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.DocumentosAdjuntoIn, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.DescripcionSolucion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.DescripcionSolucion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.SwDescentralizado, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.AreaResponsable, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.SucursalSolucion, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.DocumentosAdjuntoOu, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.ViaEnvioRespuesta, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.TipoCarta, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.SwGeneraCarta, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.NroCarta, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.SwRespuestaEnviada, opt => opt.NullSubstitute(string.Empty))
            .ForMember(c => c.SwComunicacionTelefono, opt => opt.NullSubstitute(string.Empty));
        }
    }
}

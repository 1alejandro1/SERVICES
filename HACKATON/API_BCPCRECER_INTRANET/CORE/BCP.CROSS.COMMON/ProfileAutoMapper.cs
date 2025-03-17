using AutoMapper;
using BCP.CROSS.MODELS;
using BCP.CROSS.MODELS.DTOs.Caso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.COMMON
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<CreateCasoDTO, Caso>()
                .ForMember(c => c.MonedaDevolucion, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Nombres, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Materno, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Sucursal, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Departamento, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Ciudad, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Agencia, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.NroCuenta, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Moneda, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.AtmSucursal, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.AtmUbicacion, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.InformacionAdicional, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.DocumentosAdjuntoIn, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.DireccionRespuesta, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.TelefonoRespuesta, opt => opt.NullSubstitute(string.Empty))
                .ForMember(c => c.Moneda, opt => opt.NullSubstitute(string.Empty));

        }
    }
}

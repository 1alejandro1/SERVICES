using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.MODELS.Client
{
    public class GetClienteResponse
    {
        public string NroIdc { get; set; }
        public string Idc { get; set; }
        public string IdcTipo{ get; set; }
        public string IdcExtension { get; set; }
        public string PaternoRazonSocial { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreCompleto { get; set; }
        public string TipoPersona { get; set; }
        public string TipoCliente { get; set; }
        public string Direccion { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string Fax { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Cleular1 { get; set; }
        public string Cleular2 { get; set; }
        public string Email { get; set; }

        public GetClienteResponse()
        {
            NroIdc = string.Empty;
            Idc = string.Empty;
            IdcTipo = string.Empty;
            IdcExtension = string.Empty;
            PaternoRazonSocial = string.Empty;
            Materno = string.Empty;
            Nombres = string.Empty;
            NombreEmpresa = string.Empty;
            NombreCompleto = string.Empty;
            TipoPersona = string.Empty;
            TipoCliente = string.Empty;
            Direccion = string.Empty;
            Direccion1 = string.Empty;
            Direccion2 = string.Empty;
            Fax = string.Empty;
            Telefono1 = string.Empty;
            Telefono2 = string.Empty;
            Cleular1 = string.Empty;
            Cleular2 = string.Empty;
            Email = string.Empty;
        }
    
    
    }
}

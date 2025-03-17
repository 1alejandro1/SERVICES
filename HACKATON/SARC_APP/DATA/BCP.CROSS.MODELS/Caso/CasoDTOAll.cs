using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BCP.CROSS.MODELS.Caso
{
    public class CasoDTOAll
    {
        public string FechaRegistro { get; set; }
        public string FuncionarioRegistro { get; set; }
        public string HoraRegistro { get; set; }

        public string Idc { get; set; }

        [Display(Name = "Nro IDC")]
        public string ClienteIdc { get; set; }

        
        [Display(Name = "Tipo IDC")]
        public string ClienteTipo { get; set; }

        
        [Display(Name = "Extensión IDC")]
        public string ClienteExtension { get; set; }

        
        [Display(Name = "Paterno/Razón Social")]
        public string ApellidoPaterno { get; set; }

        
        [Display(Name = "Materno")]
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }

        public string ProductoId { get; set; }
        public string Producto { get; set; }
        public string ServicioId { get; set; }
        public string Servicio { get; set; }
        public string TipoServicio { get; set; }

        public string Empresa { get; set; }
        public string Sucursal { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        public string Agencia { get; set; }

        [Display(Name = "Asignado")]
        public string FuncionarioAtencion { get; set; }
        public string FechaAsignacion { get; set; }
        public string HoraAsignacion { get; set; }
        public string Estado { get; set; }
        public string EstadoCaso { get; set; }

        public string FechaInicioAtencion { get; set; }
        public string HoraInicioAtencion { get; set; }
        public string FechaFinAtencion { get; set; }
        public string HoraFinAtencion { get; set; }
        public string FechaDeathLine { get; set; }
        public string HoraDeathLine { get; set; }

        public string Cuenta { get; set; }
        public string Tarjeta { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string FechaTxn { get; set; }
        public string HoraTxn { get; set; }

        public string InformacionAdicional { get; set; }

        public string ATMSucursal { get; set; }
        public string ATMUbicacion { get; set; }

        public string TipoSolucion { get; set; }
        public string DescripcionSolucion { get; set; }
        public string SWDescCenter { get; set; }
        public string AreaRespuesta { get; set; }
        public string SucursalSolucion { get; set; }

        [Display(Name = "Documentación")]
        public string DocumentacionAdjuntaEntrada { get; set; }
        public string DocumentacionAdjuntaSalida { get; set; }

        public string ViaEnvioRespuesta { get; set; }
        public string TipoCarta { get; set; }
        public int NumeroCartasEnviadas { get; set; }
        public string SWGeneracionCarta { get; set; }

        public string NroCaso { get; set; }

        public string SWRespuestaEnviada { get; set; }
        public string SWComTel { get; set; }
        public string SWComEmail { get; set; }
        public string SWComSMS { get; set; }

        public string DireccionRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        public string SMSRespuesta { get; set; }
        public string EmailRespuesta { get; set; }

        public decimal ImporteDev { get; set; }
        public string MonedaDev { get; set; }

        public string AntServ { get; set; }
        public string ClasObs { get; set; }

        public string AreaOrigen { get; set; }
        public string PaternoOrigen { get; set; }
        public string MaternoOrigen { get; set; }
        public string NombresOrigen { get; set; }
        public string UsuarioOrigen { get; set; }

        public string RutaSharepoint { get; set; }

        public string IdRegistroError { get; set; }
        public string DescripcionRegistroError { get; set; }
        public string IdRegistroErrorSolicitud { get; set; }
        public string DescripcionRegistroErrorSolicitud { get; set; }
    }

    public class GetCasoByNroCasoRequest
    {
        public string NroCaso { get; set; }
    }
}

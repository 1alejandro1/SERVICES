using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS
{
    public class Caso
    {
        public string FuncionarioRegistro { get; set; }
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string ClienteIdc { get; set; }
        public string ClienteIdcTipo { get; set; }
        public string ClienteIdcExtension { get; set; }
        public string ProductoId { get; set; }
        public string ServicioId { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string Empresa { get; set; }
        public string Sucursal { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        public string Agencia { get; set; }
        public string FuncinarioAtencion { get; set; }
        public string FechaAsignacion { get; set; }
        public string HoraAsignacion { get; set; }
        public string FechaIncioAtencion { get; set; }
        public string HoraIncioAtension { get; set; }
        public string FechaFinAtencion { get; set; }
        public string HoraFinAtencion { get; set; }
        public string FechaDeathLine { get; set; }
        public string HoraDeathLine { get; set; }
        public string NroCuenta { get; set; }
        public string NroTarjeta { get; set; }
        public Decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string FechaTxn { get; set; }
        public string HoraTxn { get; set; }
        public string InformacionAdicional { get; set; }
        public string AtmSucursal { get; set; }
        public string AtmUbicacion { get; set; }
        public string DocumentosAdjuntoIn { get; set; }
        public string TipoSolucion { get; set; }
        public string DescripcionSolucion { get; set; }
        public string SwDescentralizado { get; set; }
        public string AreaResponsable { get; set; }
        public string EstadoCaso { get; set; }
        public string SucursalSolucion { get; set; }
        public string DocumentosAdjuntoOu { get; set; }
        public int ViaEnvioCodigo { get; set; }
        public string ViaEnvioRespuesta { get; set; }
        public string TipoCarta { get; set; }
        public string SwGeneraCarta { get; set; }
        public string NroCarta { get; set; }
        public string SwRespuestaEnviada { get; set; }
        public string SwComunicacionTelefono { get; set; }
        public string SwComunicacionEmail { get; set; }
        public string SwComunicacionSms { get; set; }
        public string SwComunicacionEnOficina { get; set; }
        public string SwComunicacionWhatsapp { get; set; }
        public string DireccionRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        public string EmailRespuesta { get; set; }
        public string SmsRespuesta { get; set; }
        public Decimal ImporteDevolucion { get; set; }
        public string MonedaDevolucion { get; set; }
        public string DescripcionEstado { get; set; }
        public string SwErrorRegistro { get; set; }
        public string ANT_SERV { get; set; }
        public string CLAS_OBS { get; set; }
        public string AREA_OR { get; set; }
        public string PaternoOr { get; set; }
        public string MaternoOr { get; set; }
        public string NombresOr { get; set; }
        public string UsuarioOr { get; set; }
        public string RutaSharePoint { get; set; }
        public string Canal { get; set; }

        public Caso()
        {
            FechaRegistro = string.Empty;
            HoraRegistro = string.Empty;
            Paterno = string.Empty;
            Materno = string.Empty;
            Nombres = string.Empty;
            Empresa = string.Empty;
            Sucursal = string.Empty;
            Departamento = string.Empty;
            Ciudad = string.Empty;
            Agencia = string.Empty;
            FuncinarioAtencion = string.Empty;
            FechaAsignacion = string.Empty;
            HoraAsignacion = string.Empty;
            FechaIncioAtencion = string.Empty;
            HoraIncioAtension = string.Empty;
            FechaFinAtencion = string.Empty;
            HoraFinAtencion = string.Empty;
            FechaDeathLine = string.Empty;
            HoraDeathLine = string.Empty;
            NroCuenta = string.Empty;
            NroTarjeta = string.Empty;
            Moneda = string.Empty;
            FechaTxn = string.Empty;
            HoraTxn = string.Empty;
            InformacionAdicional = string.Empty;
            AtmSucursal = string.Empty;
            AtmUbicacion = string.Empty;
            DocumentosAdjuntoIn = string.Empty;
            TipoSolucion = string.Empty;
            DescripcionSolucion = string.Empty;
            SwDescentralizado = string.Empty;
            AreaResponsable = string.Empty;
            EstadoCaso = string.Empty;
            SucursalSolucion = string.Empty;
            DocumentosAdjuntoOu = string.Empty;
            ViaEnvioRespuesta = string.Empty;
            TipoCarta = string.Empty;
            SwGeneraCarta = string.Empty;
            NroCarta = string.Empty;
            SwRespuestaEnviada = "0";
            SwComunicacionTelefono = "0";
            SwComunicacionSms = "0";
            SwComunicacionEnOficina = string.Empty;
            SwComunicacionEmail = string.Empty;
            SwComunicacionWhatsapp = string.Empty;
            DireccionRespuesta = string.Empty;
            TelefonoRespuesta = string.Empty;
            EmailRespuesta = string.Empty;
            SmsRespuesta = string.Empty;
            MonedaDevolucion = string.Empty;
            DescripcionEstado = string.Empty;
            SwErrorRegistro = string.Empty;
            ANT_SERV = string.Empty;
            CLAS_OBS = string.Empty;
            AREA_OR = string.Empty;
            PaternoOr = string.Empty;
            MaternoOr = string.Empty;
            NombresOr = string.Empty;
            UsuarioOr = string.Empty;
            RutaSharePoint = string.Empty;
            Canal = string.Empty;
        }
    }
}

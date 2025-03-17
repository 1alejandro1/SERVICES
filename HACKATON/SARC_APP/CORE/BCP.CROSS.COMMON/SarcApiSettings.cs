using System;
using System.Collections.Generic;
using System.Text;

namespace BCP.CROSS.COMMON
{
    public class SarcApiSettings
    {
        public string NameApplication { get; set; }
        public string BaseUrl { get; set; }
        public string RutaRecursos { get; set; }
        public Segurinet Segurinet { get; set; }
        public Lexico Lexico { get; set; }
        public  Casos Casos { get; set; }
        public Clientes Clientes{ get; set; }
        public SmartLink SmartLink { get; set; }
        public WcfSwamp WcfSwamp { get; set; }
        public Reportes Reportes{ get; set; }
        public SharePoint SharePoint { get; set; }
    }

    public class Segurinet
    {
        public string Login { get; set; }
        public string ChangePassword { get; set; }
    }
    public class Casos
    {
        public string RegistrarCasoExpress { get; set; }
        public string RegistrarCaso { get; set; }
        public string ObtenerCasoAllByAnalista { get; set; }
        public string ObtenerCasoByNroCaso { get; set; }
        public string ActualizarAsignacionInfoAdicional { get; set; }
        public string ActualizarCasoOrigen { get; set; }
        public string ActualizarCasoSolucion { get; set; }
        public string ActualizarCasoDevolucionCobro { get; set; }
        public string ObtenerCasosByEstado { get; set; }
        public string CerrarCaso { get; set; }
        public string ObtenerCasoAll { get; set; }
        public string RechazarCasoAsignado { get; set; }
        public string RechazarCasoSolucionado { get; set; }
        public string ActualizaInfoRespuesta { get; set; }

    }

    public class Clientes
    {
        public string GetClienteByIdc { get; set; }
        public string GetClienteByDbc { get; set; }
    }

    public class SmartLink
    {
        public string GetTipoCambio { get; set; }
    }

    public class WcfSwamp
    {
      public string Debito {get; set;}
      public string Abono { get; set; }
    }

public class Lexico
    {
        public List<TipoIdc> TipoIdc { get; set; }
        public string ViasNotificacion { get; set; }
        public string Productos { get; set; }
        public string Servicios { get; set; }
        public string ValidaCuenta { get; set; }
        public string Sucursales { get; set; }
        public string ATMs { get; set; }
        public string ServiciosSwampLexico { get; set; }
        public string TipoSolucion { get; set; }
        public string DocumentacionAdjByProductoServicio { get; set; }
        public string ServicioDevolucionByPR { get; set; }
        public string ServicioCobroByPR { get; set; }
        public string CentasContableByServicioCanalId { get; set; }
        public string ParametroSacrByTipo { get; set; }
        public string GetCartaAll { get; set; }
        public string GetAreaAll { get; set; }
        public string TipoError { get; set; }
        public string LimiteDevolucion { get; set; }
    }

    public class SharePoint
    {
        public string InsertArchivosAdjuntos { get; set; }
    }

    public class TipoIdc
    {
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }

    public class Reportes
    {
        public EndPointASFI ASFI { get; set; }
        public EndPointAnalista Analista { get; set; }
        public string CapacidadEspecialidad { get; set; }
        public string ReporteBase { get; set; }
        public EndPointCNR CNR { get; set; }
        public string TipoReclamo { get; set; }
        public string ReposicionTarjeta { get; set; }
        public string DevolucionATMPOS { get; set; }
        public string Expedicion { get; set; }
        public string CobrosDevoluciones { get; set; }
        public string GetCartaFile { get; set; }
    }

    public class EndPointCNR
    {
        public string Total { get; set; }
        public string Detalle { get; set; }
    }

    public class EndPointAnalista
    {
        public string CantidadCasos { get; set; }
        public string CasosSolucionados { get; set; }
        public string Especialidad { get; set; }
    }

    public class EndPointASFI
    {
        public string Soluciones { get; set; }
        public string Registrados { get; set; }
    }
}

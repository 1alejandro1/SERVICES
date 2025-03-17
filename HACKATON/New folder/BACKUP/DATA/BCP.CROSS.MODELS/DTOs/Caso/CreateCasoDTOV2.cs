using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.DTOs.Caso
{
    public class CreateCasoDTOV2
    {
        public string NroTarjeta { get; set; }

        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string NroCuenta { get; set; }
        public string ClienteIdc { get; set; }
        public string ClienteIdcTipo { get; set; }
        public string ClienteIdcExtension { get; set; }


        //para pdf
        public string NombreUsuario { get; set; }
        public string TipoRegistro { get; set; }
        //??canal
        public string FuncionarioRegistro { get; set; }
        
        public string ProductoId { get; set; }
        public string ServicioId { get; set; }
        public string InformacionAdicional { get; set; }
        public int ViaEnvioCodigo { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public string Empresa { get; set; }
        //?public string Sucursal = "204";
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        //?public string Agencia = "201";
        //??public string FuncinarioAtencion = request.Caso.FuncionarioRegistro;//ver
        
        public string FechaTxn { get; set; }
        public string HoraTxn { get; set; }
        public string AtmSucursal { get; set; }
        public string AtmUbicacion  { get; set; }
        public string DocumentosAdjuntoIn { get; set; }
        public string ViaEnvioRespuesta { get; set; }
        public string DireccionRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        public string EmailRespuesta { get; set; }
        public string SmsRespuesta { get;set; }
        //?public string SwComunicacionEnOficina = request.Caso.ViaEnvioRespuesta.Equals("O")?"1":"0";
        //??public string SwComunicacionEmail = request.Caso.ViaEnvioRespuesta.Equals("E") ? "1" : "0";
        //??public string SwComunicacionWhatsapp = request.Caso.ViaEnvioRespuesta.Equals("W") ? "1" : "0";
        //?? public string ImporteDevolucion = request.Caso.Monto;
        //??public string MonedaDevolucion = request.Caso.Moneda;
        // public string RutaSharePoint = _sharepoint.GetRutaSharePoint();
        public List<ArchivoSharepoint> ArchivosAdjuntos { get; set; }
    }

    public class CreateandSolveCasoDTOV2: CreateCasoDTOV2
    {
        public string TipoSolucion { get; set; }//
        public string DescripcionSolucion { get; set; }//validar
        public string TipoCarta { get; set; }
    }

    public class ArchivoSharepoint
    {
        public string Nombre { get; set; }
        public string Archivo { get; set; }
    }
}

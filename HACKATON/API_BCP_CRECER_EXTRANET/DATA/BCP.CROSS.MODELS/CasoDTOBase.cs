namespace BCP.CROSS.MODELS
{
    public class CasoDTOBase
    {
        public string FechaRegistro { get; set; }//
        public string FuncionarioRegistro { get; set; }//
        public string HoraRegistro { get; set; }//IDC NUM,TIPO EXT Y IDC
        public string ApellidoPaterno { get; set; }//
        public string ApellidoMaterno { get; set; }//
        public string Nombres { get; set; } //
        public string ProductoId { get; set; }//
        public string Producto { get; set; }//
        public string ServicioId { get; set; }//
        public string Servicio { get; set; }//        
        public string Empresa { get; set; }//
        public string Sucursal { get; set; }//      
        public string Agencia { get; set; }//
        public string FuncionarioAtencion { get; set; }//
        public string FechaAsignacion { get; set; }//
        public string HoraAsignacion { get; set; }//
        public string Estado { get; set; }//
        public string EstadoCaso { get; set; }//
        public string FechaInicioAtencion { get; set; }//
        public string HoraInicioAtencion { get; set; }//
        public string FechaFinAtencion { get; set; }//
        public string HoraFinAtencion { get; set; }//
        public string FechaDeathLine { get; set; }//
        public string HoraDeathLine { get; set; }//
        public string Cuenta { get; set; }//
        public string Tarjeta { get; set; }//
        public decimal Monto { get; set; }//
        public string Moneda { get; set; }//
        public string FechaTxn { get; set; }//
        public string HoraTxn { get; set; }//
        public string InformacionAdicional { get; set; }//
        public string ATMSucursal { get; set; }//
        public string ATMUbicacion { get; set; } //
        public string DocumentacionAdjuntaEntrada { get; set; }//
        public string TipoSolucion { get; set; }//
        public string DescripcionSolucion { get; set; }//
        public string IdRegistroErrorSolucion { get; set; }//
        public string DescripcionRegistroErrorSolucion { get; set; }//SW ERROR SOLUCION
        public string SWDescCenter { get; set; }//
        public string AreaRespuesta { get; set; }//
        public string SucursalSolucion { get; set; }//        
        public string DocumentacionAdjuntaSalida { get; set; }//
        public string ViaEnvioRespuesta { get; set; }//
        public string TipoCarta { get; set; }//
        public string SWGeneracionCarta { get; set; }//
        public string NroCaso { get; set; }//
        public string SWRespuestaEnviada { get; set; }//
        public string SWComTel { get; set; }//
        public string SWComEmail { get; set; } //
        public string DireccionRespuesta { get; set; }//
        public string TelefonoRespuesta { get; set; }      // 
        public string EmailRespuesta { get; set; }//
        public decimal ImporteDev { get; set; }//
        public string MonedaDev { get; set; }//SWERROR REG
        public string IdRegistroError { get; set; }//
        public string DescripcionRegistroError { get; set; }//
        public string AntServ { get; set; }//
        public string ClasObs { get; set; }//
        public string AreaOrigen { get; set; }//
        public string PaternoOrigen { get; set; }//
        public string MaternoOrigen { get; set; }//
        public string NombresOrigen { get; set; }//
        public string UsuarioOrigen { get; set; }//
        public string RutaSharepoint { get; set; }//
        
        
        // public string Direccion { get; set; }   
        //public string Descripcion { get; set; }               
        //public string ClasificacionCaso { get; set; }
        //public string AtendidoPor { get; set; }
        //public string AsignadoPor { get; set; }
    }

    
}

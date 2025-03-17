using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MICREDITO.WebSite.Models
{
    public class SolicitudModel
    {
        public string nroDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public string ciudad { get; set; }
        public string complemento { get; set; }
        public string marcaTipoProceso { get; set; }
        public bool consultado { get; set; }
        public DatosPersonales Titular { get; set; }
    }

    public class DatosPersonales
    {
        public string cic { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string fechaNacimiento { get; set; }
        public string codSexo { get; set; }
        public string sexo { get; set; }
        public string codEstadoCivil { get; set; }
        public string estadoCivil { get; set; }
        public string codNacionalidad { get; set; }
        public string nacionalidad { get; set; }
        public string telefono { get; set; }
        public string correoElectronico { get; set; }
        public string celular { get; set; }
        public string direccion { get; set; }
        public string codTipoDireccion { get; set; }
        public string tipoDireccion { get; set; }
        public string numeroDireccion { get; set; }
        public string codlocalidad { get; set; }
        public string codInstruccion { get; set; }
        public string instruccion { get; set; }
        public string codSituacionLaboral { get; set; }
        public string situacionLaboral { get; set; }
        public string codProfesion { get; set; }
        public string profesion { get; set; }
        public string ocupacion { get; set; }
        public string nit { get; set; }
        public string nombreEmpresa { get; set; }
        public string codGiro { get; set; }
        public string giro { get; set; }
        public string codTipoDireccionLaboral { get; set; }
        public string tipoDireccionLaboral { get; set; }
        public string numeroDireccionLaboral { get; set; }
        public string direccionLaboral { get; set; }
        public string codLocalidadLaboral { get; set; }
        public string fechaIngreso { get; set; }
        public string codCargoActual { get; set; }
        public string cargoActual { get; set; }
        public double ingresoBruto { get; set; }
        public string telefonoLaboral { get; set; }
        public string codTipoVivienda { get; set; }
        public string tipoVivienda { get; set; }
        public string idcConyuge { get; set; }
        public string nombreConyuge { get; set; }
        public string paternoConyuge { get; set; }
        public string maternoConyuge { get; set; }
        public string codNacionalidadConyuge { get; set; }
        public string nacionalidadConyuge { get; set; }
        public string codActividadEconomica { get; set; }
        public string actividadEconomica { get; set; }
        public string fechaNacimientoConyuge { get; set; }
        public string codProfesionConyuge { get; set; }
        public string profesionConyuge { get; set; }
        public string codSituacionLaboralConyuge { get; set; }
        public string situacionLaboralConyuge { get; set; }
        public string nombreEmpleadorConyuge { get; set; }
        public string codCargoActualConyuge { get; set; }
        public decimal ingresoBrutoConyuge { get; set; }
        public string fechaIngresoConyuge { get; set; }
        public string telefonoConyuge { get; set; }
        public decimal otrosIngresosConyuge { get; set; }
    }

    public class TipoOperacionModel
    {
        public int codSolicitud { get; set; }
        public string nroSolicitud { get; set; }
        public string nroEvaluacion { get; set; }
        public string matricula { get; set; }
        public int codTipoOperacion { get; set; }
        public string moneda { get; set; }
        public decimal monto { get; set; }
        public List<RefinanciamientoModel> lstRefinanciamiento { get; set; }
    }

    public class RefinanciamientoModel
    {
        public string nroOperacion { get; set; }
        public decimal monto { get; set; }
    }
}
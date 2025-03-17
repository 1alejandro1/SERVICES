using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.CROSS.MODELS.Infocliente
{
    public class ClientePnByIdcResponse
    {
        public DatosInfoCliente InfoCliente { get; set; }

        public DatosInfoDatoAdicional InfoDatoAdicional { get; set; }

        public DatosInfoDatoLaboral InfoDatoLaboral { get; set; }

        public DatosInfoDatoPersonal InfoDatoPersonal { get; set; }

        public DatosInfoDatoLaboralExtendido InfoDatoLaboralExtendido { get; set; }

        public DatosInfoDatoConyuge InfoDatoConyuge { get; set; }

        public List<DatosInfoDireccion> InfoDireccion { get; set; }

        public DatosInfoDatoCorrespondencia InfoDatoCorrespondencia { get; set; }

        public DatosInfoCuenta InfoCuenta { get; set; }

        public List<DatosInfoRelacionSocioPEP> InfoRelacionSocioPEP { get; set; }

        public List<DatosInfoRelacionEmpresaPEP> InfoRelacionEmpresaPEP { get; set; }

        public bool exito { get; set; }
        public string mensaje { get; set; }
        public string codigo { get; set; }
        public string operacion { get; set; }
        public List<BCPError> error { get; set; }

    }
    public class BCPError
    {
        public string nivel { get; set; }
        public string error { get; set; }

        public BCPError()
        {
            this.nivel = string.Empty;
            this.error = string.Empty;
        }
        public BCPError(string nivel, string error)
        {
            this.nivel = nivel;
            this.error = error;
        }
    }

    public class DatosInfoCliente
    {

        public int clienteId { get; set; }

        public string paterno { get; set; }

        public string materno { get; set; }

        public string nombre { get; set; }

        public string idc { get; set; }

        public string estado { get; set; }

        public string usuario { get; set; }

        public DateTime fechaUltimaModificacion { get; set; }

        public DateTime fechaCreacion { get; set; }

        public string canal { get; set; }

        public string origen { get; set; }

        public string complemento { get; set; }

        public string alias { get; set; }

        public string cic { get; set; }
    }

    public class DatosInfoDatoAdicional
    {

        public int datoAdicionalId { get; set; }

        public int clienteId { get; set; }

        public int codTelefonoId { get; set; }

        public string telefono { get; set; }

        public string celular { get; set; }

        public string correoElectronico { get; set; }

        public string usuario { get; set; }

        public DateTime fechaUltimaModificacion { get; set; }

        public DateTime fechaCreacion { get; set; }

        public string canal { get; set; }
    }
    public class DatosInfoDatoLaboral
    {
        public int datoLaboralId { get; set; }
        public int clienteId { get; set; }
        public int situacionLaboralId { get; set; }
        public string situacionLaboralDes { get; set; }
        public string nombreEmpresa { get; set; }
        public string nit { get; set; }
        public int cargoId { get; set; }
        public string cargoDes { get; set; }
        public decimal salario { get; set; }
        public int codTelefonoId { get; set; }
        public string codTelefonoDes { get; set; }
        public string telefono { get; set; }
        public string anexoTelefono { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string canal { get; set; }
        public int actividadId { get; set; }
        public string actividadDes { get; set; }
        public int ciiuId { get; set; }
        public string ciiuDes { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
        public bool cargoPublico { get; set; }
        public string cargo { get; set; }
        public string periodo { get; set; }
        public bool negocioPropio { get; set; }
        public string fechaLugarTrabajo { get; set; }
        public bool funcionarioPublico { get; set; }
        public bool parientePep { get; set; }
        public bool asociadoPep { get; set; }
        public string institucionPublica { get; set; }
        public int paisCargoId { get; set; }
        public string paisCargoDes { get; set; }
    }

    public class DatosInfoDatoLaboralExtendido
    {
        public int datoLaboralExtendidoId { get; set; }
        public int clienteId { get; set; }
        public int situacionlaboralDosId { get; set; }
        public string situacionlaboralDosDes { get; set; }
        public int situacionlaboralTresId { get; set; }
        public string situacionlaboralTresDes { get; set; }
        public int situacionlaboralCuatroId { get; set; }
        public string situacionlaboralCuatroDes { get; set; }
        public int situacionlaboralCincoId { get; set; }
        public string situacionlaboralCincoDes { get; set; }
        public int actividadDosId { get; set; }
        public string actividadDosDes { get; set; }
        public int actividadTresId { get; set; }
        public string actividadTresDes { get; set; }
        public int actividadCuatroId { get; set; }
        public string actividadCuatroDes { get; set; }
        public int actividadCincoId { get; set; }
        public string actividadCincoDes { get; set; }
        public int ciiuDosId { get; set; }
        public string ciiuDosDes { get; set; }
        public int ciiuTresId { get; set; }
        public string ciiuTresDes { get; set; }
        public int ciiuCuatroId { get; set; }
        public string ciiuCuatroDes { get; set; }
        public int ciiuCincoId { get; set; }
        public string ciiuCincoDes { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public string canal { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
    }

    public class DatosInfoDatoConyuge
    {
        public int datoConyugeId { get; set; }
        public int clienteId { get; set; }
        public int situacionlaboralUnoId { get; set; }
        public string situacionlaboralUnoDes { get; set; }
        public int actividadUnoId { get; set; }
        public string actividadUnoDes { get; set; }
        public int ciiuUnoId { get; set; }
        public string ciiuUnoDes { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public string canal { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
    }

    public class DatosInfoDatoPersonal
    {
        public int datoPersonalId { get; set; }
        public int clienteId { get; set; }
        public string fechaNac { get; set; }
        public int sexoId { get; set; }
        public string sexoDes { get; set; }
        public int estadoCivilId { get; set; }
        public string estadoCivilDes { get; set; }
        public int nacionalidadId { get; set; }
        public string nacionalidadDes { get; set; }
        public int profesionId { get; set; }
        public string profesionDes { get; set; }
        public int gradoInstruccionId { get; set; }
        public string gradoInstruccionDes { get; set; }
        public int codTelefonoId { get; set; }
        public string codTelefonoDes { get; set; }
        public string telefono { get; set; }
        public string anexoTelefono { get; set; }
        public int codCelularId { get; set; }
        public string codCelularDes { get; set; }
        public string celular { get; set; }
        public string correoElectronico { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string canal { get; set; }
        public bool residente { get; set; }
        public string conyIdc { get; set; }
        public string conyNombre { get; set; }
        public string conyPaterno { get; set; }
        public string conyMaterno { get; set; }
        public int conyNacionalidadId { get; set; }
        public string conyNacionalidadDes { get; set; }
        public bool conyResidente { get; set; }
        public int banco1Id { get; set; }
        public string banco1Des { get; set; }
        public int producto1Id { get; set; }
        public string producto1Des { get; set; }
        public int banco2Id { get; set; }
        public string banco2Des { get; set; }
        public int producto2Id { get; set; }
        public string producto2Des { get; set; }
        public int banco3Id { get; set; }
        public string banco3Des { get; set; }
        public int producto3Id { get; set; }
        public string producto3Des { get; set; }
        public string refper1Nombre { get; set; }
        public string refper1Direccion { get; set; }
        public string refper1Telefono { get; set; }
        public string refper2Nombre { get; set; }
        public string refper2Direccion { get; set; }
        public string refper2Telefono { get; set; }
        public string refper3Nombre { get; set; }
        public string refper3Direccion { get; set; }
        public string refper3Telefono { get; set; }
        public string refcom1Nombre { get; set; }
        public string refcom1Direccion { get; set; }
        public string refcom1Telefono { get; set; }
        public string refcom2Nombre { get; set; }
        public string refcom2Direccion { get; set; }
        public string refcom2Telefono { get; set; }
        public string refcom3Nombre { get; set; }
        public string refcom3Direccion { get; set; }
        public string refcom3Telefono { get; set; }
        public string conyComplementoIdc { get; set; }
        public int segmentoSocialId { get; set; }
        public string segmentoSocialDes { get; set; }
        public int tipoCuentaId { get; set; }
        public string tipoCuentaDes { get; set; }
        public int tipoClienteId { get; set; }
        public string tipoClienteDes { get; set; }
        public int tipoBancaId { get; set; }
        public string tipoBancaDes { get; set; }
        public string correspondencia { get; set; }
        public int magnitudEmpresaId { get; set; }
        public string magnitudEmpresaDes { get; set; }
        public string nombreComercial { get; set; }
        public string codigoSbs { get; set; }
        public string funcionarioNegocios { get; set; }
        public int residenciaPais { get; set; }
        public string residenciaPaisDes { get; set; }
        public int residenciaConyPais { get; set; }
        public string residenciaConyPaisDes { get; set; }
        public string banco1Otro { get; set; }
        public string banco2Otro { get; set; }
        public string banco3Otro { get; set; }
        public string fax { get; set; }
        public bool limitacion { get; set; }
        public int tipoLimitacion { get; set; }
        public string desmaterializacion { get; set; }
        public int direccionDesmaterializacion { get; set; }
        public string conyFechaNac { get; set; }
        public int conyProfesionId { get; set; }
        public string conyProfesionDes { get; set; }
        public int conySituacionLabId { get; set; }
        public string conySituacionLabDes { get; set; }
        public string conyNombreEmp { get; set; }
        public int conyCargoId { get; set; }
        public string conyCargoDes { get; set; }
        public decimal conyIngreso { get; set; }
        public string conyFechaIng { get; set; }
        public string conyTelefono { get; set; }
        public decimal conyOtrosIngresos { get; set; }
        public string nroNitPersonal { get; set; }
        public string razonSocialPersonal { get; set; }
    }

    public class DatosInfoDireccion
    {
        public int direccionId { get; set; }
        public int clienteid { get; set; }
        public string direccion { get; set; }
        public int tipoDireccionId { get; set; }
        public string tipoDireccionDes { get; set; }
        public int tipoViviendaId { get; set; }
        public string tipoViviendaDes { get; set; }
        public int tipoDefinicionDireccionId { get; set; }
        public string tipoDefinicionDireccionDes { get; set; }
        public string numeroDireccion { get; set; }
        public string manzanaDireccion { get; set; }
        public string loteDireccion { get; set; }
        public int tipoDepartamentoId { get; set; }
        public string tipoDepartamentoDes { get; set; }
        public string numeroDepartamento { get; set; }
        public int urbanizacionId { get; set; }
        public string urbanizacionDes { get; set; }
        public string nombreUrbanizacion { get; set; }
        public int sectorUrbanizacionId { get; set; }
        public string sectorUrbanizacionDes { get; set; }
        public string nombreSectorUrbanizacion { get; set; }
        public int departamentoId { get; set; }
        public string departamentoDes { get; set; }
        public int provinciaId { get; set; }
        public string provinciaDes { get; set; }
        public int distritoId { get; set; }
        public string distritoDes { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string canal { get; set; }
        public decimal latitudDomi { get; set; }
        public decimal longitudDomi { get; set; }
    }

    public class DatosInfoDatoCorrespondencia
    {
        public int datoCorrespondenciaId { get; set; }
        public int clienteId { get; set; }
        public int cuentaId { get; set; }
        public int direccionId { get; set; }
        public int estado { get; set; }
        public int usuario { get; set; }
        public DateTime fechaUltimaModificacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int canal { get; set; }
    }

    public class DatosInfoCuenta
    {
        public DateTime fechaUltimaModificacion { get; set; }
        public DateTime fechaCreacion { get; set; }
    }


    public class DatosInfoRelacionSocioPEP
    {
        //[PAR_SOC_PEP_ID]
        public int empresaPepId { get; set; }
        //[CLIENTE_ID]
        public int clienteId { get; set; }
        //[TIPO_PEP_ID]
        public int tipoPepId { get; set; }
        public string tipoPepDes { get; set; }
        //[IDC_PAR_SOC]
        public string idcParSoc { get; set; }
        //[EXTIDC_PAR_SOC]
        public string extensionIdcParSoc { get; set; }
        //[TIPOIDC_PAR_SOC]
        public string tipoIdcParSoc { get; set; }
        //[COMPLEIDC_PAR_SOC]
        public string complementoParSoc { get; set; }
        //[AP_PAT_PAR_SOC]
        public string apPaternoParSoc { get; set; }
        //[AP_MAT_PAR_SOC]
        public string apMaternoParSoc { get; set; }
        //[NOMBRES_PAR_SOC]
        public string nombresParSoc { get; set; }
        //[NACIONALIDAD_PAR_SOC_ID]
        public int nacionalidadParSocId { get; set; }
        public string nacionalidadParSocDes { get; set; }
        //[PARENTESCO_PAR_SOC_ID]
        public int parentescoParSocId { get; set; }
        public string parentescoParSocDes { get; set; }
        //[TIPO_VINCULO_PAR_SOC_ID]
        public int tipoVinculoParSocId { get; set; }
        public string tipoVinculoParSocDes { get; set; }
        //[NOM_INSTI_ORGANI_PAR_SOC]
        public string nomInstiOrganiParSoc { get; set; }
        //[CARGO_OFICIO_PAR_SOC]
        public string cargoOficioParSoc { get; set; }
        //[PERIODO_PAR_SOC]
        public string periodoParSoc { get; set; }
        //[PAIS_CARGO_PAR_SOC_ID]
        public int paisCargoParSocId { get; set; }
        public string paisCargoParSocDes { get; set; }
        //[USUARIO]
        public string usuario { get; set; }
        //[FECHA_ULTIMA_MODIFICACION]
        public DateTime fechaUltimaModificacion { get; set; }
        //[FECHA_CREACION]
        public DateTime fechaCreacion { get; set; }
        //[CANAL]
        public string canal { get; set; }
    }


    public class DatosInfoRelacionEmpresaPEP
    {
        //[EMPRESA_PEP_ID]
        public int empresaPepId { get; set; }
        //[CLIENTE_ID]
        public int clienteId { get; set; }
        //[TIPO_PEP_ID]
        public int tipoPepId { get; set; }
        public string tipoPepDes { get; set; }
        //[NOMBRE_EMPRESA]
        public string nombreEmpresa { get; set; }
        //[NIT]
        public string nit { get; set; }
        //[USUARIO]
        public string usuario { get; set; }
        //[FECHA_ULTIMA_MODIFICACION]
        public DateTime fechaUltimaModificacion { get; set; }
        //[FECHA_CREACION]
        public DateTime fechaCreacion { get; set; }
        //[CANAL]
        public string canal { get; set; }
    }
}

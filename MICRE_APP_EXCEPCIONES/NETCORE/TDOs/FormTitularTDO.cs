using NETCORE.Models.APIResponse;

namespace NETCORE.TDOs
{
    public class FormTitularTDO
    {
        public string? matricula { get; set; }
        public string? tipoCliente { get; set; }
        public string? score {  get; set; } 
        public InformacionPersonalTitularTDO? InformacionPersonal { get; set; }
        public InfromacionLaboralTDO? InformacionLaboral { get; set; }
        public DeudaPersonaleTDO? DeudaPersonal { get; set; }
        public DeudaInmuebleTDO? DeudaInmueble { get; set; }
        public InformacionPersonalConyugueTDO? InformacionPersonalConyugue { get; set; }
        public DeudaPersonalConyugueTDO? DeudaPersonalConyugue { get; set; }
        public InformacionLaboralConyugueTDO? InformacionLaboralConyugue { get; set; }
        public List<ProductoTDO>? Productos { get; set; }
    }
}

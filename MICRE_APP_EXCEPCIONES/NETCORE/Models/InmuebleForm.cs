using Microsoft.AspNetCore.Mvc.Rendering;
using NETCORE.Models.APIResponse;
using NETCORE.TDOs;

namespace NETCORE.Models
{
    public class InmuebleForm
    {
        public string? matricula {  get; set; }
        public string? tipoCliente { get; set; }
        public string? score { get; set; }
        public InformacionPersonalTitularTDO? InformacionPersonalTitular { get; set; }
        public InfromacionLaboralTDO? InformacionLaboral { get; set; }
        public DeudaPersonaleTDO? DeudaPersonale { get; set; }
        public DeudaInmuebleTDO? DeudaInmueble { get; set; }
        public InformacionPersonalConyugueTDO? InformacionPersonalConyugue { get; set; }
        public DeudaPersonalConyugueTDO? DeudaPersonalConyugue { get; set; }
        public InformacionLaboralConyugueTDO? InformacionLaboralConyugue { get; set; }
        public List<ProductoTDO>? Productos { get; set; }
        public List<Parametro>? ProductosChecks {  get; set; } 
        public List<Parametro>? Laboral {  get; set; }
        public List<Parametro>? Finalidades_C { get; set; }
        public List<Parametro>? Finalidades_N { get; set; }
        public List<Parametro>? Finalidades_H { get; set; }
        public List<Parametro>? Antiguiedades { get; set; }
        public List<Parametro>? EstadoCivil { get; set; }
        public List<Parametro>? Profesiones { get; set; }
        public List<Parametro>? ActividadEconomica { get; set; }
        public List<SelectListItem> LstScore { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> LstTipoCliente { get; set; } = new List<SelectListItem>();
    }
}

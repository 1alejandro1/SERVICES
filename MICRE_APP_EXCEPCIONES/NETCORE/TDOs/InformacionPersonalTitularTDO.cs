using System.ComponentModel.DataAnnotations;

namespace NETCORE.TDOs
{
    public class InformacionPersonalTitularTDO
    {
        // informacion personal
        public string? nombres { get; set; }
        public string? paterno { get; set; }
        public string? materno { get; set; }
        public int idEstadoCivil { get; set; }
        public string? ci { get; set; }
        public string? complemento { get; set; }
        public string? extension { get; set; }
    }
}

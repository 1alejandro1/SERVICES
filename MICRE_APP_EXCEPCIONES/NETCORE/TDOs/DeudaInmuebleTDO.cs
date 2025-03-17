namespace NETCORE.TDOs
{
    public class DeudaInmuebleTDO
    {
        public string? inmuebleHipotecado { get; set; }
        public decimal valorInmuebleUSD { get; set; }
        public string? inmuebleLibreDeuda { get; set; }
        public decimal valorInmuebleLibreDeudaUSD { get; set; }
        public decimal totalGarantia { get; set; }
    }
}

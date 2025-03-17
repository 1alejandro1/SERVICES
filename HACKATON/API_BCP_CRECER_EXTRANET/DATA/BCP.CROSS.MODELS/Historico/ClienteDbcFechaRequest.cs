namespace BCP.CROSS.MODELS.Historico
{
    public class ClienteDbcFechaRequest: ClienteDbcRequest
    {
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
    }
}

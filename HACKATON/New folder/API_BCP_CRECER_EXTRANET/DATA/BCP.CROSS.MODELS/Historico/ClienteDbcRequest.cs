namespace BCP.CROSS.MODELS.Historico
{
    public class ClienteDbcRequest: ClienteIdcRequest
    {
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
    }
}

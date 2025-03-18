namespace BCP.CROSS.MODELS.Response
{
    public class StateResponse
    {
        public string? Idc { get; set; }
        public string? Phase { get; set; }
        public bool FinishedProcess { get; set; }
        public int IdDigitalSignature { get; set; }
    }
}

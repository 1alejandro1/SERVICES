namespace NETCORE.Models
{
    public class PersonaResponse
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public PersonaDatos? Data { get; set; }
    }
}

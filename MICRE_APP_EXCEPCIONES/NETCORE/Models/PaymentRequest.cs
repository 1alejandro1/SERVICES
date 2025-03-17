namespace NETCORE.Models
{
    public class PaymentRequest
    {
        public string Pan { get; set; }
        public ExpirationDate ExpirationDate { get; set; }
        public string Cvv2 { get; set; }
    }

    public class ExpirationDate
    {
        public string Month { get; set; }
        public string Year { get; set; }
    }

}

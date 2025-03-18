namespace BCP.CROSS.MODELS.Response
{
    public class BaseRekognitionResponse<T>
    {
        public string SessionId { get; set; }
        public T Data { get; set; }
    }
}

namespace BCP.CROSS.MODELS.Request
{
    public class VerifyRequest
    {
        public string? sessionId { get; set; }
        public string? image64 { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        public int areaLeft { get; set; }
        public int areaTop { get; set; }
        public int areaWidth { get; set; }
        public int areaHeight { get; set; }
        public int minFaceAreaPercent { get; set; }
        public int noseLeft { get; set; }
        public int noseTop { get; set; }
        public int noseWidth { get; set; }
        public int noseHeight { get; set; }
        public string? cumpleArea { get; set; }
    }
}

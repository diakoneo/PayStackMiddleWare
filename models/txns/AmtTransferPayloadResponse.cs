namespace PayStackMiddleWare.API.models.txns
{
    public class AmtTransferPayloadResponse
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public data? data { get; set; }
    }
}
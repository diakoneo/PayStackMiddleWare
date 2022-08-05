namespace PayStackMiddleWare.API.models.responses
{

    public class VerifyPayloadResponse{
        public string? status { get; set; }
        public string? message { get; set; }
        public VerifyTransactionData? data { get; set; }
    }

    public class TransactionPayloadResponse
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public List<VerifyTransactionData?> data { get; set; }
    }

}
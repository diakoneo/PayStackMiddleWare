namespace PayStackMiddleWare.API.models.txns
{
    public class AmtTransferPayload
    {
        public string? amount { get; set; }
        public string? email { get; set; }
        public string? currency { get; set; }
        public int? reference { get; set; }
        public string[] channels { get; set; }
        public AmtTransferPayloadMetaData? metadata { get; set; }
        public string? callback { get; set; }
    }
}
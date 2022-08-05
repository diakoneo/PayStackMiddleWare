namespace PayStackMiddleWare.API.models.utils{

    public class Settings{
        public string connection { get; set; }
        public string apiUrl { get; set; }
        public string secretKey { get; set; }
        public string contentType { get; set; }
        public string verifyTxnAPI { get; set; }

        public string logging { get; set; }

        public string TxnCurrency{get;set;}
        public string webHookURL { get; set; }
        public string gettransactions { get; set; }
        public string getcustomer { get; set; }
    }

}
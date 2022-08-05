using System.Net.Http.Headers;

namespace PayStackMiddleWare.API.models.utils{

    public class ConfigObject{
        public static string KONNECT { get; set; }
        public static string INITIALIZE_PAYMENT_ENDPOINT { get; set; }
        public static string VERIFY_TXN_URL { get; set; }
        public static string SECRET_API_KEY { get; set; }
        public static string CONTENT_TYPE { get; set; }

        public static string LOG_FILE { get; set; }
        public static string TXN_CURRENCY{get;set;}
        public static string WEBHOOK_URL { get; set; }
        public static string GET_TRANSACTIONS { get; set; }
        public static string GET_CUSTOMER { get; set; }

        public static async Task<HttpClient> buildClientHeader(string URIEndPoint)
        {
            //builds the client endpoint and returns it to the method making request
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(URIEndPoint)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigObject.CONTENT_TYPE));
            client.DefaultRequestHeaders.Add("Authorization", ConfigObject.SECRET_API_KEY);

            return client;
        }
    }

}
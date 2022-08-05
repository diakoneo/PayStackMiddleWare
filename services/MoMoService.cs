using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayStackMiddleWare.API.models.txns;
using PayStackMiddleWare.API.models.utils;
using PayStackMiddleWare.API.models.responses;

using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Diagnostics;

namespace PayStackMiddleWare.API.services{

    public class MoMoService : IMoMoService{

        #region Implementations

        public async Task<AmtTransferPayloadResponse> makePayment(AmtTransferPayload payLoad){
            //return payload response
            AmtTransferPayloadResponse o =new AmtTransferPayloadResponse();
            var client = await this.buildClientHeader(ConfigObject.INITIALIZE_PAYMENT_ENDPOINT);

            var content = new StringContent(JsonConvert.SerializeObject(payLoad),Encoding.UTF8,@"application/json");

            try
            {
                using(var response = await client.PostAsync(client.BaseAddress,content)){
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode){
                        var responseBody = await response.Content.ReadAsStringAsync();

                        var x = JObject.Parse(responseBody);

                        o.status = x["status"].ToString();
                        o.message = x["message"].ToString();
                        o.data = x["data"].ToObject<data>();
                    }
                    
                    return o;
                }
            }
            catch(Exception exc){
                Debug.Print($"error: {exc.Message}");
                return o;
            }
        }

        public async Task<VerifyPayloadResponse> VerifyTransaction(string txnReferenceID){
            //uses transaction reference id to verify transaction
            VerifyPayloadResponse obj = new VerifyPayloadResponse();
            var client = await buildClientHeader(string.Format("{0}/{1}",ConfigObject.VERIFY_TXN_URL,txnReferenceID));

            try
            {
                using(var response = await client.GetAsync(client.BaseAddress)){
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode){
                        var body = await response.Content.ReadAsStringAsync();

                        var x = JObject.Parse(body);
                        obj.status = x["status"].ToString();
                        obj.message = x["message"].ToString();
                        obj.data = x["data"].ToObject<VerifyTransactionData>();
                    }

                    return obj;
                }
            }
            catch(Exception error){
                Debug.Print($"error: {error.Message}");
                return obj;
            }
        }

        public async Task<TransactionPayloadResponse> listTransactions()
        {
            //list all transactions
            TransactionPayloadResponse obj = new TransactionPayloadResponse();
            HttpClient client = await buildClientHeader(ConfigObject.GET_TRANSACTIONS);

            try
            {
                using (var response = await client.GetAsync(client.BaseAddress))
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var body = await response.Content.ReadAsStringAsync();

                        obj = JsonConvert.DeserializeObject<TransactionPayloadResponse>(body);
                        
                    }

                    return obj;
                }
            }
            catch(Exception exc)
            {
                Debug.Print($"error: {exc.Message}");
                return obj;
            }
        }

        public async Task<VerifyPayloadResponse> listTransactions(decimal amount)
        {
            //list all transactions
            return new VerifyPayloadResponse() { };
        }

        public async Task<TransactionPayloadResponse> listTransactionWithAmount(decimal amount)
        {
            var dt = new TransactionPayloadResponse();
            var client = await buildClientHeader(string.Format("{0}/?amount={1}", ConfigObject.GET_TRANSACTIONS, amount.ToString()));

            try
            {
                using (var apiResponse = await client.GetAsync(client.BaseAddress))
                {
                    apiResponse.EnsureSuccessStatusCode();
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var apiResponseBody = await apiResponse.Content.ReadAsStringAsync();

                        dt = JsonConvert.DeserializeObject<TransactionPayloadResponse>(apiResponseBody);
                    }

                    return dt;
                }
            }
            catch(Exception x)
            {
                Debug.Print($"error: {x.Message}");
                return dt;
            }
        }

        public async Task<TransactionPayloadResponse> listTransactionWithinDateRange(DateTime dfrom, DateTime dTo)
        {
            var dta = new TransactionPayloadResponse();
            HttpClient client = await buildClientHeader(string.Format("{0}/?from={1}&to={2}", ConfigObject.GET_TRANSACTIONS, dfrom.ToString("yyyy-MM-dd"), dTo.ToString("yyyy-MM-dd")));

            try
            {
                using (var response = await client.GetAsync(client.BaseAddress))
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var body = await response.Content.ReadAsStringAsync();
                        dta = JsonConvert.DeserializeObject<TransactionPayloadResponse>(body);
                    }

                    return dta;
                }
            }
            catch(Exception x)
            {
                Debug.Print($"error: {x.Message}");
                return dta;
            }
        }

        public async Task<TransactionPayloadResponse> listTransactions(string status)
        {
            //list all transactions
            var dta = new TransactionPayloadResponse();
            var client = await buildClientHeader(String.Format("{0}/?status={1}",ConfigObject.GET_TRANSACTIONS,status));
            
            try
            {
                using (var apiResponse = await client.GetAsync(client.BaseAddress))
                {
                    apiResponse.EnsureSuccessStatusCode();
                    var body = await apiResponse.Content.ReadAsStringAsync();

                    dta = JsonConvert.DeserializeObject<TransactionPayloadResponse>(body);
                }

                return dta;
            }
             catch(Exception xerror)
            {
                Debug.Print($"error: {xerror.Message}");
                return dta;
            }
        }

        public async Task<TransactionPayloadResponse> listTransactions(int CustomerID)
        {
            TransactionPayloadResponse dt = new TransactionPayloadResponse();
            HttpClient client = await buildClientHeader(string.Format("{0}/?customer={1}", ConfigObject.GET_TRANSACTIONS, CustomerID));

            try
            {
                using (var response = await client.GetAsync(client.BaseAddress))
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        dt = JsonConvert.DeserializeObject<TransactionPayloadResponse>(json);
                    }

                    return dt;
                }
            }
            catch(Exception x)
            {
                Debug.Print($"error: {x.Message}");
                return dt;
            }
        }

        #endregion

        #region Private methods
        public async Task<HttpClient> buildClientHeader(string URIEndPoint){
            //builds the client endpoint and returns it to the method making request
            HttpClient client = new HttpClient(){
                BaseAddress = new Uri(URIEndPoint)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ConfigObject.CONTENT_TYPE));
            client.DefaultRequestHeaders.Add("Authorization",ConfigObject.SECRET_API_KEY);

            return client;
        }
        #endregion

        
    }

}
using System;
using PayStackMiddleWare.API.models.responses;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading;
using PayStackMiddleWare.API.models.utils;
using System.Text;

namespace PayStackMiddleWare.API.services
{
    public class CustomerResponsePayLoad
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public CustomerResponse? data { get; set; }
    }

    public class CustomerResponse
    {
        public string? email { get; set; }
        public string? integration { get; set; }
        public string? domain { get; set; }
        public string? customer_code { get; set; }
        public int? id { get; set; }
        public string? identified { get; set; }
        public List<Identification>? identifications { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }

    public class Identification
    {

    }

    public interface ICustomerService
    {
        Task<CustomerResponsePayLoad> CreateCustomer(Customer customr);
        //Task<CustomerResponsePayLoad> GetCustomer(string email);
        //Task<CustomerResponsePayLoad> GetCustomerUsingCode(string customer_code);
    }

    public class CustomerService: ICustomerService
    {
        public async Task<CustomerResponsePayLoad> CreateCustomer(Customer customer)
        {
            CustomerResponsePayLoad obj = new CustomerResponsePayLoad();
            HttpClient client = await buildCustomerClientHeaderAsync(ConfigObject.GET_CUSTOMER);

            var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, @"application/json");

            try
            {
                using (var apiResponse = await client.PostAsync(client.BaseAddress, content))
                {
                    apiResponse.EnsureSuccessStatusCode();
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var json = await apiResponse.Content.ReadAsStringAsync();

                        obj = JsonConvert.DeserializeObject<CustomerResponsePayLoad>(json);
                    }

                    return obj;
                }    
            }
            catch(Exception x)
            {
                Debug.Print($"error: {x.Message}");
                return obj;
            }
        }

        private async Task<HttpClient> buildCustomerClientHeaderAsync(string URIEndPoint)
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


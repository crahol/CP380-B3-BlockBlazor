using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;
using System.Text;
using Newtonsoft.Json;

namespace CP380_B3_BlockBlazor.Data
{
    public class PendingTransactionService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        public List<Payload> payloads { get; set; }
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;

        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        // 
        public PendingTransactionService() { }
        public PendingTransactionService(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("PayloadService");
        }

        //
        // TODO: Add an async method that returns an IEnumerable<Payload> (list of Payloads)
        //       from the web service
        //
        public async Task<IEnumerable<Payload>> ListPayloads()
        {
            IEnumerable<Payload> payloadList = null;
            
            var response = await _httpClient.GetAsync("http://localhost:46688/PendingPayloads");
            if (response.IsSuccessStatusCode)
            {
                string responseStream = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject(responseStream).ToString();
                payloadList = JsonConvert.DeserializeObject<IEnumerable<Payload>>(jsonResult);
            }
            return payloadList;
        }

        //
        // TODO: Add an async method that returns an HttpResponseMessage
        //       and accepts a Payload object.
        //       This method should POST the Payload to the web API server
        //
        public async Task<HttpResponseMessage> AddPayload(Payload payload)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri("http://localhost:46688/AddPayload"),
                Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"),
            };
            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
            
            return response;
        }
    }
}

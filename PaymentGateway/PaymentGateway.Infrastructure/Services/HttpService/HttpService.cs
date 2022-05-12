using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Application.Common.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services.HttpService
{
    public class HttpService : IHttpService
    {

        private readonly HttpClient _client;
        private readonly ILogger<HttpService> _logger;

        public HttpService(HttpClient client, ILogger<HttpService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<T> Post<T>(string url, object body)
        {

            try
            {
             
                var data = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(url, data);

                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.ToString());
                throw;
            }

        }
    }
}

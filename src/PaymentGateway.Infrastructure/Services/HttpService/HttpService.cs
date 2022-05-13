using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services.HttpService
{
    public class HttpService : IHttpService
    {


        private readonly ILogger<HttpService> _logger;

        public HttpService(ILogger<HttpService> logger)
        {
            _logger = logger;
        }
        public async Task<HttpResponseMessage> Post(string url, object body, Dictionary<string, string> headers = null)
        {

            try
            {
                using (var _client = new HttpClient())
                {

                    _client.DefaultRequestHeaders.Clear();

                    if (headers != null)
                    {
                        foreach (var header in headers)
                        {
                            _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }
                  
                    var data = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                    var response = await _client.PostAsync(url, data);
                 
                    return response;
                }

            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.ToString());
                throw;
            }

        }
    }
}

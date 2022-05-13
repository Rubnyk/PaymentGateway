using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Interfaces.Companies;
using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Domain.ValueObjects;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static PaymentGateway.Domain.Constants.CreditCardConstants;

namespace PaymentGateway.Infrastructure.Services.CreditCompanies
{
    public class VisaCompanyService : IVisaCompanyService
    {
        public string BaseUrl { get; set; }
        public string PayApi { get; set; }
        public readonly IHttpService _httpService;
        public VisaCompanyService(IConfiguration configuration, IHttpService httpService)
        {
            BaseUrl = configuration["Companies:Visa:BaseUrl"];
            PayApi = configuration["Companies:Visa:PayApi"];
            _httpService = httpService;
        }

        public async Task<object> Pay(PayCommand request)
        {

            var name = new FullName(request.FullName);

            var url = BaseUrl + PayApi;

            var body = new
            {
                fullName = request.FullName,
                number = request.CreditCardNumber,
                expiration = request.ExpirationDate,
                cvv = request.Cvv,
                totalAmount = request.Amount,

            };

            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "identifier", name.FirstName },
            };

            var response =  await _httpService.Post(url, body, headers);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:

                    await ValidateResponse(response);

                    return new 
                    {

                    };
               
                default:
                    throw new Exception("ServerError");
            }

            return null;
        }

        private async Task ValidateResponse(HttpResponseMessage response)
        {
            var result = JsonConvert.DeserializeObject<VisaCompanyResponse>(await response.Content.ReadAsStringAsync());

            if (result.ChargeResult == VisaResponses.Failure)
            {
                throw new ChargeDeclineException(result.ResultReason);
            }

        }
    }

    public class VisaCompanyResponse
    {
        public string ChargeResult { get; set; }
        public string ResultReason { get; set; }

    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Interfaces.Companies;
using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Domain.Constants;
using PaymentGateway.Domain.Models.Companies.MasterCard;
using PaymentGateway.Domain.ValueObjects;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services.CreditCompanies
{
    public class MasterCardCompanyService : IMasterCardCompanyService
    {
        public string BaseUrl { get; set; }
        public string PayApi { get; set; }
        public readonly IHttpService _httpService;
        public MasterCardCompanyService(IConfiguration configuration, IHttpService httpService)
        {
            BaseUrl = configuration["Companies:MasterCard:BaseUrl"];
            PayApi = configuration["Companies:MasterCard:PayApi"];
            _httpService = httpService;
        }

        public async Task<PayResponse> Pay(PayCommand request)
        {

            var name = new FullName(request.FullName);

            var url = BaseUrl + PayApi;

            var body = new
            {

                first_name = name.FirstName,
                last_name = name.LastName,
                card_number = request.CreditCardNumber,
                expiration = ParseDate(request.ExpirationDate),
                cvv = request.Cvv,
                charge_amount = request.Amount,

            };

            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "identifier", name.FirstName },
            };

            var response = await _httpService.Post(url, body, headers);


            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return new PayResponse
                    {
                        ErrorCode = 0,
                        ErrorMessage = "Ok"
                    };
                case System.Net.HttpStatusCode.BadRequest:
                    var res = await ReadResponse(response);
                    throw new ChargeDeclineException(res.error ?? res.decline_reason);
                default:
                    throw new Exception("ServerError");
            }


        }

        private async Task<MasterCardCompanyResponse> ReadResponse(HttpResponseMessage response)
        {

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MasterCardCompanyResponse>(result);
        }

        private string ParseDate(string requestDate)
        {
            return DateTime.ParseExact("01/" + requestDate, "dd/MM/yy", null).ToString("MM-yy");
        }
    }


    public class MasterCardCompanyResponse
    {
        public string error { get; set; }
        public string decline_reason { get; set; }

    }
}

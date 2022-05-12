using Microsoft.Extensions.Configuration;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Interfaces.Companies;
using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Domain.Models.Companies.MasterCard;
using PaymentGateway.Domain.ValueObjects;
using System.Threading.Tasks;

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

        public async Task<PayResponse> Pay(PayCommand request)
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

            return null;
        }
    }
}

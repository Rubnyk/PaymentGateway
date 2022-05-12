using Microsoft.Extensions.Configuration;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Domain.Models.Companies.MasterCard;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services.CreditCompanies
{
    public class MasterCardCompanyService
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
            return await _httpService.Post<PayResponse>(BaseUrl + PayApi, new
            {
                first_name = request.FullName.FirstName,
                last_name= request.FullName.LastName,
                card_number = request.CreditCardNumber,
                expiration = request.ExperationDate,
                cvv= request.Cvv,
                charge_amount =  request.Amount,

            });
        }
    }
}

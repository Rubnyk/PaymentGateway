using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Interfaces.Companies;
using PaymentGateway.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Payments.Commands
{
    public class PayCommand : IRequest<object>
    {      
        public string FullName { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardCompany { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
    }

    public class PayCommandHandler : IRequestHandler<PayCommand, object>
    {
        private readonly IServiceProvider _serviceProvider;

        public PayCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<object> Handle(PayCommand request, CancellationToken cancellationToken)
        {
            ICompanyService _service;
            switch (request.CreditCardCompany)
            {
                case "visa":
                    _service = _serviceProvider.GetService<IVisaCompanyService>();
                    break;
                case "mastercard":
                    _service = _serviceProvider.GetService<IMasterCardCompanyService>();
                    break;
                default:
                    throw new Exception("Invalid provider");
            }


            try
            {
                await _service.Pay(request);


            }
            catch (ChargeDeclineException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw;
            }

            return new
            {

            };

        }
    }
}

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Common.Interfaces;
using PaymentGateway.Application.Common.Interfaces.Companies;
using PaymentGateway.Application.Retry.Commands;
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
        private readonly IMediator _mediator;

        public PayCommandHandler(IServiceProvider serviceProvider, IMediator mediator)
        {
            _serviceProvider = serviceProvider;
            _mediator = mediator;
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
                await _mediator.Send(new RetryCommand
                {
                    action = new Action(() => { _service.Pay(request).GetAwaiter().GetResult(); })
                });
            }
            catch (ChargeDeclineException ex)
            {
                await _mediator.Send(new CreatePaymentCommand
                {
                    TransactionDate = request.ExpirationDate,
                    Amount = request.Amount,
                    ErrorMessage = ex.Message
                });

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

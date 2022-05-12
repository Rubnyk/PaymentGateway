using MediatR;
using PaymentGateway.Domain.ValueObjects;
using System;

namespace PaymentGateway.Application.Payments.Commands
{
    public class PayCommand : IRequest<PayCommandDto>
    {
        public FullName FullName { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardCompany { get; set; }
        public DateTime ExperationDate { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
    }
}

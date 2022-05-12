using FluentValidation;

namespace PaymentGateway.Application.Payments.Commands
{


    public class PayCommandValidator : AbstractValidator<PayCommand>
    {
        public PayCommandValidator()
        {
            RuleFor(req => req.FullName).NotEmpty().WithMessage("Please enter name");
            RuleFor(req => req.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0"); 
            RuleFor(req => req.CreditCardNumber).NotEmpty().WithMessage("Please enter credit card");
        }
       
    }
}

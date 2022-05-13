using MediatR;
using PaymentGateway.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<object>
    {        
        public string TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, object>
    {
        private readonly IRepositoryContext _db;
        private readonly IUserService _userService;
        private static object LockObject = new object();

        public CreatePaymentCommandHandler(IRepositoryContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }
        public async Task<object> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {

            await _db.Transactions.AddAsync(new Domain.Entities.Transaction
            {
                MerchantId = _userService.MerchantId,
                Amount = request.Amount,
                ErrorMessage = request.ErrorMessage,
                EventTime = DateTime.UtcNow,
                TransactionDate = request.TransactionDate
            });

            await _db.SaveChangesAsync();


            return null;


        }
    }
}

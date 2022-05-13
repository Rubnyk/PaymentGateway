using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Payments.Queries
{
    public class GetChargeStatusQuery : IRequest<List<GetChargeStatusQueryDto>>
    {

    }

    public class GetChargeStatusQueryHandler : IRequestHandler<GetChargeStatusQuery, List<GetChargeStatusQueryDto>>
    {
        private readonly IRepositoryContext _db;
        private readonly IUserService _userService;
        public GetChargeStatusQueryHandler(IRepositoryContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<List<GetChargeStatusQueryDto>> Handle(GetChargeStatusQuery request, CancellationToken cancellationToken)
        {
            return await _db.Transactions
                .Where(itm => itm.MerchantId == _userService.MerchantId)
                .GroupBy(itm => itm.ErrorMessage)               
                .Select(itm => new GetChargeStatusQueryDto
                {
                    Count = itm.Count(),
                    Reason = itm.First().ErrorMessage
                }).ToListAsync();

        }
    }
}

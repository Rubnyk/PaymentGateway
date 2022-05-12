using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Domain.Models.Companies.MasterCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface ICompanyService
    {
        Task<PayResponse> Pay(PayCommand request);
    }
}

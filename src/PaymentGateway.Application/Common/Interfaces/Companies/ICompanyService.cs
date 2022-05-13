using PaymentGateway.Application.Payments.Commands;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface ICompanyService
    {
        Task<object> Pay(PayCommand request);
    }
}

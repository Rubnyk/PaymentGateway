using System.Threading.Tasks;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface IHttpService
    {
        Task<T> Post<T>(string url, object data);
    }
}

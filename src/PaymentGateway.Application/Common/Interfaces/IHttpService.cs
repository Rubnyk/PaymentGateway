using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Post(string url, object data, Dictionary<string, string> headers = null);
    }
}

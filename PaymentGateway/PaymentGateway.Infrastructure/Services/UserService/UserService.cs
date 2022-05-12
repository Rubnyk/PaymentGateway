using Microsoft.AspNetCore.Http;
using PaymentGateway.Application.Common.Interfaces;

namespace PaymentGateway.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string UserName { get => _httpContext.HttpContext.Items["UserName"]?.ToString(); set => _httpContext.HttpContext.Items["UserName"] = value; }
    }
}

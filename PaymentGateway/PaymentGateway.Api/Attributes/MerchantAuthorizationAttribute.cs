using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Common.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Attributes
{
    public class MerchantAuthorizationAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
            var _userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            if (context.HttpContext.Request.Headers.ContainsKey("merchant-identifier")){
                _userService.MerchantId = context.HttpContext.Request.Headers["merchant-identifier"];
               
            } else
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,                   
                    ContentType = "text/plain"
                };
                return;
            }
            
          

            await next();
        }
    }
}

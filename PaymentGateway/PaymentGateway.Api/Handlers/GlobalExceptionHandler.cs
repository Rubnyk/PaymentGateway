
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PaymentGateway.Application.Common.Exceptions;
using System.Linq;
using System.Net;
using ValidationException = FluentValidation.ValidationException;

namespace PaymentGateway.Api.Handlers
{
    public static class GlobalExceptionHandler
    {

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;
                string result = "";
                result = JsonConvert.SerializeObject(new
                {
                   
                });
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result).ConfigureAwait(false);

            }));

            return app;
        }
    }
}

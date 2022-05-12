
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
                if (exceptionHandlerPathFeature?.Error is ValidationException)
                {
                    ValidationException ex = (ValidationException)exceptionHandlerPathFeature?.Error;
                    var err = ex.Errors.LastOrDefault();
                    result = JsonConvert.SerializeObject(new
                    {
                        ReturnCode = err.ErrorCode,
                        Description = err.ErrorMessage
                    });
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                 if (exceptionHandlerPathFeature?.Error is TimeoutException)
                {
                    TimeoutException ex = (TimeoutException)exceptionHandlerPathFeature?.Error;
                    result = JsonConvert.SerializeObject(new
                    {
                        Title = "Timeout.",
                        Detail = ex.Message
                    }); ;
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                }
                else
                 if (exceptionHandlerPathFeature?.Error is NotFoundException)
                {
                    NotFoundException ex = (NotFoundException)exceptionHandlerPathFeature?.Error;
                    result = JsonConvert.SerializeObject(new
                    {
                        Title = "Entity not found",
                        Detail = ex.Message
                    }); ;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                else
                {
                    throw exceptionHandlerPathFeature?.Error;
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result).ConfigureAwait(false);

            }));

            return app;
        }
    }
}

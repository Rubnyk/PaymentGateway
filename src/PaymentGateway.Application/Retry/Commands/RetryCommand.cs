using MediatR;
using Microsoft.Extensions.Options;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Retry.Commands
{
    public class RetryCommand : IRequest<object>
    {
        public Action action { get; set; }
    }

    public class RetryCommandHandler : IRequestHandler<RetryCommand, object>
    {
        private readonly RetryConfig _retryConfig;
        public RetryCommandHandler(IOptionsMonitor<RetryConfig> retryConfig)
        {
            _retryConfig = retryConfig.CurrentValue;
        }
        public async Task<object> Handle(RetryCommand request, CancellationToken cancellationToken)
        {
            for (var i = 0; i < _retryConfig.Limit; i++)
            {
                try
                {
                    request.action.Invoke();
                    return new { };
                }
                catch (ChargeDeclineException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    var wait = (int)Math.Pow(i+1, _retryConfig.Grow) * 1000;
                    await Task.Delay(wait);
                }
            }

            throw new Exception("Retry Ended");
        }

    }
}

    


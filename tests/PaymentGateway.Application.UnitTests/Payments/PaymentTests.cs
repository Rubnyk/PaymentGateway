using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using PaymentGateway.Application.Common.Interfaces.Companies;
using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Application.Retry.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.Application.UnitTests.Payments
{
    public class PaymentTests
    {

        [Fact]
        public async Task CheckInvalidProviderThrowsException()
        {

            var _mediator = new Mock<IMediator>();
            var _service = new Mock<IVisaCompanyService>();
            var _serviceProvider = new Mock<IServiceProvider>();

            //setup
            _mediator.Setup(m => m.Send(It.IsAny<RetryCommand>(), default(CancellationToken))).Returns(Task.FromResult(new object()));

            _service.Setup(m => m.Pay(It.IsAny<PayCommand>())).Returns(Task.FromResult(new object()));

            _serviceProvider.Setup(m => m.GetService(typeof(IVisaCompanyService))).Returns(_service.Object);

            var handler = new PayCommandHandler(null, null);


            //test
            //assert

            var action = handler.Handle(new PayCommand(), new CancellationToken());

            
            //TODO: complete test
            
        }
    }
}

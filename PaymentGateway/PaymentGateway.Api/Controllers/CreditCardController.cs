using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Payments.Commands;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CreditCardController : ApiController
    {

        [HttpPost("v1/pay")]
        public async Task<ActionResult<PayCommandDto>> Pay(PayCommand request)
        {
            return Ok(Mediator.Send(request));
        }
    }
}

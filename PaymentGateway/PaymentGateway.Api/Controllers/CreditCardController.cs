using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Payments.Commands;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CreditCardController : ApiController
    {

        [HttpPost("charge")]
        public async Task<IActionResult> Pay(PayCommand request)
        {
            try
            {
                return Ok(await Mediator.Send(request));
            }
            catch (ChargeDeclineException ex)
            {
                return BadRequest(new { });
            }
        }
    }
}

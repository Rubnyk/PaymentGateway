using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Attributes;
using PaymentGateway.Application.Common.Exceptions;
using PaymentGateway.Application.Payments.Commands;
using PaymentGateway.Application.Payments.Queries;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    [MerchantAuthorizationAttribute]
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

        [HttpGet("chargeStatuses")]
        public async Task<ActionResult<GetChargeStatusQuery>> GetStatus([FromQuery] GetChargeStatusQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}

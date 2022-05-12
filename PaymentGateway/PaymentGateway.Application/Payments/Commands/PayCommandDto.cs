using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Payments.Commands
{
    public class PayCommandDto
    {
        public string ChargeResult { get; set; }
        public string ResultReason { get; set; }
    }
}

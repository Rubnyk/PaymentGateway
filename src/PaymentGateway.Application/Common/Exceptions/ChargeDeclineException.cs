using System;

namespace PaymentGateway.Application.Common.Exceptions
{
    public class ChargeDeclineException : Exception
    {
        public ChargeDeclineException(string message) : base(message)
        {

        }
    }
}

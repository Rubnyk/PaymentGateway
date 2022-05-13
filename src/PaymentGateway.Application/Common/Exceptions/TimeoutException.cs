using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Application.Common.Exceptions
{
    public class TimeoutException : Exception
    {
        public TimeoutException()
            : base()
        {
        }

        public TimeoutException(string message)
            : base(message)
        {
        }

        public TimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


    }
}

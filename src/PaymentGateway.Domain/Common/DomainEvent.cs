using System;

namespace PaymentGateway.Domain.Common
{
    public abstract class DomainEvent
    {

        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; } = false;
    }
}

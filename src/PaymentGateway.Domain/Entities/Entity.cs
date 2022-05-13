using PaymentGateway.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentGateway.Domain.Entities
{
    public class Entity : IEntity
    {
        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}

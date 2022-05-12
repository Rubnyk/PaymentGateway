using System;

namespace PaymentGateway.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

using System;

namespace PaymentGateway.Domain.Entities
{
    public class Transaction : Entity
    {
        public long Id { get; set; }
        public string MerchantId { get; set; }
        public DateTime EventTime { get; set; }
        public string TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string ErrorMessage { get; set; }
    }
}

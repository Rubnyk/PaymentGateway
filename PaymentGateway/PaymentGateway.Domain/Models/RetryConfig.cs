namespace PaymentGateway.Domain.Models
{
    public class RetryConfig
    {
        public int Limit { get; set; }
        public int Grow { get; set; }
    }
}

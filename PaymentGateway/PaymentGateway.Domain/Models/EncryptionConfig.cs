namespace PaymentGateway.Domain.Models
{
    public class EncryptionConfig
    {
        public string ObfuscationSalt { get; set; }
        public string IV { get; set; }
        public string Password { get; set; }
    }
}

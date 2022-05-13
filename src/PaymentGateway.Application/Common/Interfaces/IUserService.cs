namespace PaymentGateway.Application.Common.Interfaces
{
    /// <summary>
    /// Http context - get claims from token
    /// </summary>
    public interface IUserService
    {
        string MerchantId { get; set; }
    }
}

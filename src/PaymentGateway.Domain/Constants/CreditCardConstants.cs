namespace PaymentGateway.Domain.Constants
{
    public class CreditCardConstants
    {
        public struct MasterCardResponses
        {
            public const string Failure = "Failure";
            public const string Success = "Success";
        }
        public struct VisaResponses
        {
            public const string Failure = "Failure";
            public const string Success = "Success";
        }
    }
}

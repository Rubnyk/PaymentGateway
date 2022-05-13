namespace PaymentGateway.Domain.ValueObjects
{
    public class FullName
    {
        private string _firstName;
        private string _lastName;
        public FullName(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public FullName(string name)
        {
            var parts = name?.Split(" ") ?? new string[] { "" };
            _firstName = parts[0];
            _lastName = parts.Length > 0 ? parts[1] : "";
        }

        public string Name => string.IsNullOrEmpty(_lastName) ? _firstName : $"{_firstName} {_lastName}";
        public string FirstName => _firstName;
        public string LastName => _lastName;


    }
}

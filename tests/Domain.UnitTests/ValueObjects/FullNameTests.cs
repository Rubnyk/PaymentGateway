using PaymentGateway.Domain.ValueObjects;
using Xunit;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects
{
    public class FullNameTests
    {
        [Fact]
        public void ShouldReturnFirstNameTest()
        {
            var name = new FullName("Ilan Hofshi");
            name.FirstName.Should().Be("Ilan");
        }

        [Fact]
        public void ShouldReturnLastNameTest()
        {
            var name = new FullName("Ilan Hofshi");
            name.LastName.Should().Be("Hofshi");
        }
    }
}
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using Xunit;

namespace OrderManagement.CustomerContext.Domain.Tests.Customers
{
    public class CustomerTests
    {

        [Fact]
        public void Id_NotSetToEmpty()
        {
            var customer = CreateDefaultCustomer();

            Assert.NotEqual(Guid.Empty, customer.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void FirstNameIsRequired_ThrowException(string _firstName)
        {
            Assert.Throws<FirstNameIsRequiredException>(() => CreateDefaultCustomer(firstName: _firstName));
        }

        [Theory]
        [InlineData("jsdhfs")]
        public void SetFirstName(string _lastName)
        {
            var customer = CreateDefaultCustomer(lastName: _lastName);

            Assert.Equal(_lastName, customer.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void LastNameIsRequired_ThrowException(string _lastName)
        {
            Assert.Throws<LastNameIsRequiredException>(() => CreateDefaultCustomer(lastName: _lastName));
        }

        [Theory]
        [InlineData("jsdhfs")]
        public void SetLastName(string _lastName)
        {
            var customer = CreateDefaultCustomer(lastName: _lastName);

            Assert.Equal(_lastName, customer.LastName);
        }



        private Customer CreateDefaultCustomer(string firstName = "Neda",
            string lastName = "Gholipour")
        {
            return new Customer(firstName, lastName);
        }
    }
}

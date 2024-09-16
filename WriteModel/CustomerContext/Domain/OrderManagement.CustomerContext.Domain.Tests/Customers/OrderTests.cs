using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;

namespace OrderManagement.CustomerContext.Domain.Tests.Customers
{
    public class OrderTests
    {
        [Fact]
        public void Id_NotSetToEmpty()
        {
            var order = new Order(new Guid());

            Assert.NotEqual(Guid.Empty, order.Id);
        }

        [Fact]
        public void CustomerId_NotSetToEmpty()
        {
            var order = new Order(Guid.NewGuid());

            Assert.NotEqual(Guid.Empty, order.CustomerId);
        }

        [Fact]
        public void CustomerIdIsEmpty_ThrowException()
        {
            Assert.Throws<CustomerIsRequiredException>(() => new Order(new Guid()));
        }

        //[Fact]
        //public void CustomerId_ShouldSetCorrectly()
        //{
        //    var order = new Order();

        //    Assert.NotEqual(Guid.Empty, order.Id);
        //}

        //[Fact]
        //public void CustomerId_ShouldBeValid()
        //{
        //    var order = new Order();

        //    Assert.NotEqual(Guid.Empty, order.Id);
        //}
    }
}

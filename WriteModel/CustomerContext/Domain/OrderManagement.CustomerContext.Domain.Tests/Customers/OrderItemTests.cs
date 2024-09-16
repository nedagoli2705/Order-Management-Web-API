

using OrderManagement.CustomerContext.Domain.Customers;

namespace OrderManagement.CustomerContext.Domain.Tests.Customers
{
    public class OrderItemTests
    {
       [Fact]
        public void SetProductNameOfOrderItem()
        {
            var orderItem = new OrderItem("product1", 5);

            Assert.Equal("product1", orderItem.ProductName);
        }

        [Fact]
        public void SetPriceOfOrderItem()
        {
            var orderItem = new OrderItem("product1", 5);

            Assert.Equal(5, orderItem.Price);
        }

    }
}

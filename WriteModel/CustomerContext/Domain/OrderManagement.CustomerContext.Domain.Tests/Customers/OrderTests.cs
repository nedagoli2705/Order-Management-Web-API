using Moq;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using OrderManagement.CustomerContext.Domain.Customers.Services;

namespace OrderManagement.CustomerContext.Domain.Tests.Customers
{
    public class OrderTests
    {
        [Fact]
        public void Id_NotSetToEmpty()
        {
            var order = CreateDefaultOrder(customerId: Guid.NewGuid());

            Assert.NotEqual(Guid.Empty, order.Id);
        }

        [Fact]
        public void CustomerId_NotSetToEmpty()
        {
            var order = CreateDefaultOrder(customerId: Guid.NewGuid());

            Assert.NotEqual(Guid.Empty, order.CustomerId);
        }

        [Fact]
        public void CustomerIdIsEmpty_ThrowException()
        {
            Assert.Throws<CustomerIsRequiredException>(() => CreateDefaultOrder(customerId: new Guid()));
        }

        [Fact]
        public void CustomerIdIsNotValid_ThrowException()
        {
            var mockChecker = new Mock<ICustomerExistanceChecker>();
            mockChecker.Setup(x => x.IsCustomerExisted(Guid.NewGuid())).Returns(false);

            Assert.Throws<CustomerISNotExistException>(() => 
                CreateDefaultOrder(customerExistanceChecker: mockChecker.Object, customerId: Guid.NewGuid()));
        }

        [Fact]
        public void SetOrderDate()
        {
            var order = CreateDefaultOrder(customerId: Guid.NewGuid(), orderdate: new DateTime(2024,12,10));

            Assert.Equal(new DateTime(2024, 12, 10), order.OrderDate);
        }

        [Theory]
        [InlineData("0001-01-01T00:00:00")]
        [InlineData("9999-12-31T23:59:59.9999999")]
        public void OrderDateIsEmptyOrNotValid_ThrowException(DateTime orderDate)
        {

            Assert.Throws<OrderDateIsNotValidException>(() =>
                CreateDefaultOrder(customerId: Guid.NewGuid(), orderdate: orderDate));
        }

        [Fact]
        public void SetTotalAmountCorrectly()
        {
            var order = CreateDefaultOrder(customerId: Guid.NewGuid());

            Assert.Equal(12, order.TotalAmount);
        }

        [Fact]
        public void SetOrderItems()
        {
            var orderItems = new List<OrderItem>() {
                new OrderItem("Product1", 5)
            };
            var order = CreateDefaultOrder(customerId: Guid.NewGuid(), orderItems: orderItems);

            Assert.Equal(orderItems, order.Items);
        }

        [Fact]
        public void ThereIsNoOrderItem_ThrowException()
        {

            Assert.Throws<OrderShouldHaveAtLeastOneItemException>(() =>
                CreateDefaultOrder(customerId: Guid.NewGuid(), orderItems: new List<OrderItem>()));
        }


        private Order CreateDefaultOrder(ICustomerExistanceChecker customerExistanceChecker = null,
            Guid customerId = new Guid(),
            DateTime? orderdate = null,
            decimal totalAmount = 10,
            List<OrderItem> orderItems = null)
        {
            customerExistanceChecker ??= MockCustomerExistanceChecker();
            var orderDate = orderdate ?? DateTime.Now;

            orderItems ??= new List<OrderItem>() {
                new OrderItem("Product1", 5),
                new OrderItem("Product2", 7)
            };

            return new Order(customerExistanceChecker, customerId, orderDate, orderItems);
        }

        private ICustomerExistanceChecker MockCustomerExistanceChecker()
        {
            var mockChecker = new Mock<ICustomerExistanceChecker>();
            mockChecker.Setup(x => x.IsCustomerExisted(It.IsAny<Guid>())).Returns(true);
            return mockChecker.Object;
        }
    }
}

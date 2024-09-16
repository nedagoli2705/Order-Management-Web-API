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

        private Order CreateDefaultOrder(ICustomerExistanceChecker customerExistanceChecker = null,
            Guid customerId = new Guid())
        {
            customerExistanceChecker ??= MockCustomerExistanceChecker();

            return new Order(customerExistanceChecker, customerId);
        }

        private ICustomerExistanceChecker MockCustomerExistanceChecker()
        {
            var mockChecker = new Mock<ICustomerExistanceChecker>();
            mockChecker.Setup(x => x.IsCustomerExisted(It.IsAny<Guid>())).Returns(true);
            return mockChecker.Object;
        }
    }
}

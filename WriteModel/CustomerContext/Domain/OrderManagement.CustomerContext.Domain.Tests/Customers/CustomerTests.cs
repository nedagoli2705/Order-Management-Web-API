using Moq;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using OrderManagement.CustomerContext.Domain.Customers.Services;
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
        [InlineData("trtrtyhrty")]
        public void SetLastName(string _lastName)
        {
            var customer = CreateDefaultCustomer(lastName: _lastName);

            Assert.Equal(_lastName, customer.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void NationalCodeIsRequired_ThrowException(string _nationalCode)
        {
            Assert.Throws<NationalCodeIsRequiredException>(() => CreateDefaultCustomer(nationalCode: _nationalCode));
        }

        [Theory]
        [InlineData("1010101010")]
        public void SetNationalCode(string _nationalCode)
        {
            var customer = CreateDefaultCustomer(nationalCode: _nationalCode);

            Assert.Equal(_nationalCode, customer.NationalCode);
        }

        [Theory]
        [InlineData("101010101")]
        public void NationalCodeLengthShouldBeTenCharacters_ThrowException(string _nationalCode)
        {
            Assert.Throws<NationalCodeLengthShouldBeTenCharactersException>(() => 
                CreateDefaultCustomer(nationalCode: _nationalCode));
        }

        [Theory]
        [InlineData("101010101a")]
        public void NationalCodeShouldBeDigitCharacters_ThrowException(string _nationalCode)
        {
            Assert.Throws<NationalCodeShouldBeDigitCharactersException>(() =>
                CreateDefaultCustomer(nationalCode: _nationalCode));
        }

        [Fact]
        public void NationalCodeIsDuplicated_ThrowException()
        {
            var mockChecker = new Mock<INationalCodeDuplicationChecker>();
            mockChecker.Setup(x => x.IsDuplicated("1212312123")).Returns(true);

            Assert.Throws<DuplicatedNationalCodeException>(() =>
                CreateDefaultCustomer(mockChecker.Object, nationalCode: "1212312123"));
        }

        [Fact]
        public void UpdateCustomerWithValidData_ShouldUpdated()
        {
            // Arrange
            var customer = CreateDefaultCustomer();

            // Act
            customer.Update(MockDefaultNationalCodeDuplicationChecker(), "Jane", "Doe", "1234567890");

            // Assert
            Assert.Equal("Jane", customer.FirstName);
            Assert.Equal("Doe", customer.LastName);
            Assert.Equal("1234567890", customer.NationalCode);
        }

        [Theory]
        [InlineData("1010101010")]
        public void NationalCodeIsDuplicatedWhenUpdating_ThrowException(string _nationalCode)
        {
            var mockChecker = new Mock<INationalCodeDuplicationChecker>();
            mockChecker.Setup(x => x.IsDuplicated(_nationalCode)).Returns(true);

            Assert.Throws<DuplicatedNationalCodeException>(() =>
                CreateDefaultCustomer(mockChecker.Object, nationalCode: _nationalCode));
        }

        [Fact]
        public void UpdateCustomerWithPreviousNationalCode_ShouldNotChangeWithoutDuplicatedExecption()
        {
            var customer = CreateDefaultCustomer(nationalCode: "1010101010");

            customer.Update(MockDefaultNationalCodeDuplicationChecker(), "Jane", "Doe", "1010101010");

            Assert.Equal("1010101010", customer.NationalCode);
        }

        [Fact]
        public void AddOrder_OrderIsAddedToList()
        {
            // Arrange
            var customer = CreateDefaultCustomer();


            var mockCustomerExistanceChecker = new Mock<ICustomerExistanceChecker>();
            mockCustomerExistanceChecker.Setup(x => x.IsCustomerExisted(It.IsAny<Guid>())).Returns(true);

            var order = new Order(mockCustomerExistanceChecker.Object, Guid.NewGuid(), DateTime.Now, new List<OrderItem>() {
                new OrderItem("Product1", 5)
            });

            // Act
            customer.AddOrder(order);

            // Assert
            Assert.Contains(order, customer.Orders);
        }

        [Fact]
        public void AddNullOrder_ThrowException()
        {
            // Arrange
            var customer = CreateDefaultCustomer();


            var mockCustomerExistanceChecker = new Mock<ICustomerExistanceChecker>();
            mockCustomerExistanceChecker.Setup(x => x.IsCustomerExisted(It.IsAny<Guid>())).Returns(true);

            var order = new Order(mockCustomerExistanceChecker.Object, Guid.NewGuid(), DateTime.Now, new List<OrderItem>() {
                new OrderItem("Product1", 5)
            });

            // Act
            customer.AddOrder(order);

            // Assert
            Assert.Throws<UnableToAddNullOrderToCustomerException>(() =>
                customer.AddOrder(null));
        }


        private Customer CreateDefaultCustomer(
            INationalCodeDuplicationChecker nationalCodeDuplicationChecker = null,
            string firstName = "Neda",
            string lastName = "Gholipour",
            string nationalCode = "1361486858")
        {
            nationalCodeDuplicationChecker ??= MockDefaultNationalCodeDuplicationChecker();

            return new Customer(nationalCodeDuplicationChecker, firstName, lastName, nationalCode);
        }

        private INationalCodeDuplicationChecker MockDefaultNationalCodeDuplicationChecker()
        {
            var mockChecker = new Mock<INationalCodeDuplicationChecker>();
            mockChecker.Setup(x => x.IsDuplicated(It.IsAny<string>())).Returns(false);
            return mockChecker.Object;
        }
    }
}

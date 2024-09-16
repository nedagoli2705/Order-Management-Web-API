using Framework.Core.Domain;
using Framework.Domain;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using OrderManagement.CustomerContext.Domain.Customers.Services;
using System.Linq.Expressions;

namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class Customer : EntityBase<Customer>, IAggregateRoot<Customer>
    {
        private readonly INationalCodeDuplicationChecker nationalCodeDuplicationChecker;

        public Customer()
        {
            
        }

        public Customer(INationalCodeDuplicationChecker nationalCodeDuplicationChecker, 
            string firstName,
            string lastName,
            string nationalCode)
        {
            this.nationalCodeDuplicationChecker = nationalCodeDuplicationChecker;

            SetId();
            SetFirstName(firstName);
            SetLastName(lastName);
            SetNationalCode(nationalCode);
            Orders = new List<Order>();
        }



        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }
        public ICollection<Order> Orders { get; private set; }


        public void Update(INationalCodeDuplicationChecker nationalCodeDuplicationChecker, 
            string firstName,
            string lastName,
            string nationalCode)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            UpdateNationalCode(nationalCode, nationalCodeDuplicationChecker);
        }

        public void AddOrder(Order order)
        {
            if (order == null)
            {
                throw new UnableToAddNullOrderToCustomerException();
            }
            Orders.Add(order);
        }

        public void RemoveOrder(Guid orderId)
        {
            var order = Orders.FirstOrDefault(o => o.Id == orderId);
            Orders.Remove(order);
        }

        public IEnumerable<Expression<Func<Customer, object>>> GetAggregateExpressions()
        {
            yield return c => c.Orders;
        }

        private void UpdateNationalCode(string nationalCode, INationalCodeDuplicationChecker nationalCodeDuplicationChecker)
        {
            if (NationalCode != nationalCode)
            {
                if (nationalCodeDuplicationChecker.IsDuplicated(nationalCode))
                    throw new DuplicatedNationalCodeException();

                NationalCode = nationalCode;
            }
        }

        private void SetNationalCode(string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
            {
                throw new NationalCodeIsRequiredException();
            }
            if (nationalCode.Length != 10)
            {
                throw new NationalCodeLengthShouldBeTenCharactersException();
            }
            if (!nationalCode.All(char.IsDigit))
            {
                throw new NationalCodeShouldBeDigitCharactersException();
            }
            if (nationalCodeDuplicationChecker.IsDuplicated(nationalCode))
            {
                throw new DuplicatedNationalCodeException();
            }

            NationalCode = nationalCode;
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new LastNameIsRequiredException();

            LastName = lastName;
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new FirstNameIsRequiredException();

            FirstName = firstName;
        }
    }
}

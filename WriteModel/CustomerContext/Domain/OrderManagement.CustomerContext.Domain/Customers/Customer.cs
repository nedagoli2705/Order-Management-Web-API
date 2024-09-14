using Framework.Core.Domain;
using Framework.Domain;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using System.Linq.Expressions;

namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class Customer : EntityBase<Customer>, IAggregateRoot<Customer>
    {

        public Customer(string firstName,
            string lastName)
        {
            SetId();
            SetFirstName(firstName);
            SetLastName(lastName);
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

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public IEnumerable<Expression<Func<Customer, object>>> GetAggregateExpressions()
        {
            yield return null;
        }
    }
}

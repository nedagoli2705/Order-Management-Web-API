using Framework.Core.Domain;
using Framework.Domain;
using System.Linq.Expressions;

namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class Customer : EntityBase<Customer>, IAggregateRoot<Customer>
    {

        public IEnumerable<Expression<Func<Customer, object>>> GetAggregateExpressions()
        {
            yield return null;
        }
    }
}

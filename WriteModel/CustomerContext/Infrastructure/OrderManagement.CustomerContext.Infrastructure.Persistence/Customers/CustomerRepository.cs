using Framework.Core.Persistence;
using Framework.Persistence;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;
using System.Linq.Expressions;


namespace OrderManagement.CustomerContext.Infrastructure.Persistence.Customers
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateCustomer(Customer customer)
        {
            Create(customer);
        }

        public bool Contains(Expression<Func<Customer, bool>> predicate)
        {
            return Set.Any(predicate);
        }

        public Customer GetById(Guid customerId)
        {
            return Set.SingleOrDefault(a => a.Id == customerId);
        }

        public void UpdateCustomer(Customer customer)
        {
            Update(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }

        public void AddOrder(Order order) 
        {
            DbContext.Set<Order>().Add(order);
        }
    }
}

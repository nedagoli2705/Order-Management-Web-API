using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.Domain.Customers.Services
{
    public interface ICustomerRepository
    {
        void CreateCustomer(Customer customer);
        bool Contains(Expression<Func<Customer, bool>> predicate);
        Customer GetById(Guid customerId);
        void UpdateCustomer(Customer customer);
    }
}

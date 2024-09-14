using Framework.Core.Persistence;
using Framework.Persistence;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.Infrastructure.Persistence.Customers
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        protected CustomerRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}

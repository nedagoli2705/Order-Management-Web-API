﻿using Framework.Core.Persistence;
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
    }
}

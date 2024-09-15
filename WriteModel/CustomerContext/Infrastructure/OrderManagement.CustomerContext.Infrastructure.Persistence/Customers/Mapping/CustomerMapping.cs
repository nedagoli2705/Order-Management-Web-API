using Framework.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.CustomerContext.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.Infrastructure.Persistence.Customers.Mapping
{
    public class CustomerMapping : EntityMappingBase<Customer>
    {
        public override void Initiate(EntityTypeBuilder<Customer> builder)
        {
            
        }
    }
}

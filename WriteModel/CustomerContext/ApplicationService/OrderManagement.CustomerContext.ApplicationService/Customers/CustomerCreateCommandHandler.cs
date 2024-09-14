using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class CustomerCreateCommandHandler : ICommandHandler<CustomerCreateCommand>
    {
        public void Execute(CustomerCreateCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

using Framework.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.ApplicationService.Contract.Customers
{
    public class CustomerDeleteCommand : Command
    {
        public Guid Id { get; set; }
    }
}

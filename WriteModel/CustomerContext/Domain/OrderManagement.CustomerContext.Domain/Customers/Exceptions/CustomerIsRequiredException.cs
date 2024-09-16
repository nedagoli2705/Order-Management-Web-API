using Framework.Domain;
using OrderManagement.CustomerContext.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.Domain.Customers.Exceptions
{
    public class CustomerIsRequiredException : DomainException
    {
        public override string Message => ExceptionResource.CustomerIsRequired;
    }
}

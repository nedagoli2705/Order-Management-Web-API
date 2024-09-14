using Framework.Domain;
using OrderManagement.CustomerContext.Resources;

namespace OrderManagement.CustomerContext.Domain.Customers.Exceptions
{
    public class FirstNameIsRequiredException : DomainException
    {
        public override string Message => ExceptionResource.FirstNameIsRequired;
    }
}

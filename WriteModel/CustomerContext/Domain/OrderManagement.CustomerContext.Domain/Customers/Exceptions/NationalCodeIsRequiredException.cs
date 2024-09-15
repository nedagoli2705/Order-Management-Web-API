using Framework.Domain;
using OrderManagement.CustomerContext.Resources;

namespace OrderManagement.CustomerContext.Domain.Customers.Exceptions
{
    public class NationalCodeIsRequiredException : DomainException
    {
        public override string Message => ExceptionResource.NationalCodeIsRequired;
    }

}

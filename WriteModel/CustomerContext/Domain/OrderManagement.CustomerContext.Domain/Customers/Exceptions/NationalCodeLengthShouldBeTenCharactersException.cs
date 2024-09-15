using Framework.Domain;
using OrderManagement.CustomerContext.Resources;

namespace OrderManagement.CustomerContext.Domain.Customers.Exceptions
{
    public class NationalCodeLengthShouldBeTenCharactersException : DomainException
    {
        public override string Message => ExceptionResource.NationalCodeLengthShouldBeTenCharacters;
    }

}

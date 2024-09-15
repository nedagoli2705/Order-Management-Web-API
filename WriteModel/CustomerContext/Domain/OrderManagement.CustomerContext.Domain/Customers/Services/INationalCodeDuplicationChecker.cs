using Framework.Core.Domain;

namespace OrderManagement.CustomerContext.Domain.Customers.Services
{
    public interface INationalCodeDuplicationChecker : IDomainService
    {
        bool IsDuplicated(string nationalCode);
    }
}

using Framework.Core.Domain;


namespace OrderManagement.CustomerContext.Domain.Customers.Services
{
    public interface ICustomerExistanceChecker : IDomainService
    {
        bool IsCustomerExisted(Guid customerId);
    }
}

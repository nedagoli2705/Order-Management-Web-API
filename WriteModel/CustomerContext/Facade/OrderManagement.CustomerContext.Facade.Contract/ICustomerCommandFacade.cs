using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;

namespace OrderManagement.CustomerContext.Facade.Contract
{
    public interface ICustomerCommandFacade 
    {
        void CreateCustomer(CustomerCreateCommand command);
    }
}

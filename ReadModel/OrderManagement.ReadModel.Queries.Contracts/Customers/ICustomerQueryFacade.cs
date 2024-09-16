using OrderManagement.ReadModel.Queries.Contracts.Customers.DataContracts;

namespace OrderManagement.ReadModel.Queries.Contracts.Customers
{
    public interface ICustomerQueryFacade
    {
        IList<CustomerDto> GetCustomers();
    }
}

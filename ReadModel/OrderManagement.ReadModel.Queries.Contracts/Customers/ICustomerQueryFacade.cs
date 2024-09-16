using OrderManagement.ReadModel.Context.Models;
using OrderManagement.ReadModel.Queries.Contracts.Customers.DataContracts;

namespace OrderManagement.ReadModel.Queries.Contracts.Customers
{
    public interface ICustomerQueryFacade
    {
        IList<CustomerDto> GetCustomers();
        IList<Order> GetOrders();
    }
}

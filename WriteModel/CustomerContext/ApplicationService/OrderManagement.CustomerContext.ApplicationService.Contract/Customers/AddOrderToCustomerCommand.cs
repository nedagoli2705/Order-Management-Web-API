using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers.CommandDataItem;

namespace OrderManagement.CustomerContext.ApplicationService.Contract.Customers
{
    public class AddOrderToCustomerCommand : Command
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemData> Items { get; set; }
    }
}

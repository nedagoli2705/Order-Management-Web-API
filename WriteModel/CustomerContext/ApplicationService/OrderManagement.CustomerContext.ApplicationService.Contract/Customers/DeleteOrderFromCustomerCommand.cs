using Framework.Core.Application;


namespace OrderManagement.CustomerContext.ApplicationService.Contract.Customers
{
    public class DeleteOrderFromCustomerCommand : Command
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}

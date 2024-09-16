using Framework.Core.Application;

namespace OrderManagement.CustomerContext.ApplicationService.Contract.Customers
{
    public class CustomerDeleteCommand : Command
    {
        public Guid Id { get; set; }
    }
}

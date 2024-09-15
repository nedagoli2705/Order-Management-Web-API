using Framework.Core.Application;

namespace OrderManagement.CustomerContext.ApplicationService.Contract.Customers
{
    public class CustomerCreateCommand : Command
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}

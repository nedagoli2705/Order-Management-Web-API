using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class CustomerDeleteCommandHandler : ICommandHandler<CustomerDeleteCommand>
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerDeleteCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public void Execute(CustomerDeleteCommand command)
        {
            var customer = customerRepository.GetById(command.Id);
            customerRepository.DeleteCustomer(customer);
        }
    }
}


using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class DeleteOrderFromCustomerCommandHandler : ICommandHandler<DeleteOrderFromCustomerCommand>
    {
        private readonly ICustomerRepository customerRepository;

        public DeleteOrderFromCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public void Execute(DeleteOrderFromCustomerCommand command)
        {
            var customer = customerRepository.GetById(command.CustomerId);

            customer.RemoveOrder(command.OrderId);
            customerRepository.UpdateCustomer(customer);
        }
    }
}

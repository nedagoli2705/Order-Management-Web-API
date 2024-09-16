
using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class UpdateOrderOfCustomerCommandHandler : ICommandHandler<UpdateOrderOfCustomerCommand>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateOrderOfCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public void Execute(UpdateOrderOfCustomerCommand command)
        {
            var customer = customerRepository.GetById(command.CustomerId);

            var order = customer.Orders.FirstOrDefault(a => a.Id == command.OrderId);

            var updatedItems = new List<OrderItem>();
            foreach (var item in command.Items)
            {
                var updatedOrderItem = new OrderItem(item.ProductName, item.Price);
                updatedItems.Add(updatedOrderItem);
            }

            // Update the order (create a method in the Order class to update properties safely)
            order.UpdateOrder(command.OrderDate, updatedItems);

            // Persist the changes
            customerRepository.UpdateCustomer(customer);
        }
    }
}

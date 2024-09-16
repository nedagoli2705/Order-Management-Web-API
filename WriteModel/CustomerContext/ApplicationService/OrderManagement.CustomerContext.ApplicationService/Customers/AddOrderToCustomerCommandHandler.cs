using Framework.Core.Application;
using Framework.Core.Mapper;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers.CommandDataItem;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class AddOrderToCustomerCommandHandler : ICommandHandler<AddOrderToCustomerCommand>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerExistanceChecker customerExistanceChecker;


        public AddOrderToCustomerCommandHandler(ICustomerRepository customerRepository,
            ICustomerExistanceChecker customerExistanceChecker)
        {
            this.customerRepository = customerRepository;
            this.customerExistanceChecker = customerExistanceChecker;
        }

        public void Execute(AddOrderToCustomerCommand command)
        {
            var customer = customerRepository.GetById(command.CustomerId);

            var items = new List<OrderItem>();
            foreach (var item in command.Items)
            {
                var orderItem = new OrderItem(item.ProductName, item.Price);
                items.Add(orderItem);
            }

            var order = new Order(customerExistanceChecker, customer.Id, command.OrderDate, items);
            customer.AddOrder(order);

            customerRepository.AddOrder(order);
            customerRepository.UpdateCustomer(customer);
        }
    }
}

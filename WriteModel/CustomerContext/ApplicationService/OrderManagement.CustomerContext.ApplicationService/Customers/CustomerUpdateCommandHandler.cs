using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class CustomerUpdateCommandHandler : ICommandHandler<CustomerUpdateCommand>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly INationalCodeDuplicationChecker nationalCodeDuplicationChecker;

        public CustomerUpdateCommandHandler(ICustomerRepository customerRepository,
            INationalCodeDuplicationChecker nationalCodeDuplicationChecker)
        {
            this.customerRepository = customerRepository;
            this.nationalCodeDuplicationChecker = nationalCodeDuplicationChecker;
        }

        public void Execute(CustomerUpdateCommand command)
        {
            var customer = customerRepository.GetById(command.Id);

            customer.Update(nationalCodeDuplicationChecker, command.FirstName, command.LastName, command.NationalCode);
            customerRepository.UpdateCustomer(customer);

        }
    }
}

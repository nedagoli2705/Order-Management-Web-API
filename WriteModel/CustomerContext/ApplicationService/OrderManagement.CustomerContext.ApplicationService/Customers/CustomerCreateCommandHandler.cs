using Framework.Core.Application;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.Domain.Customers;
using OrderManagement.CustomerContext.Domain.Customers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.CustomerContext.ApplicationService.Customers
{
    public class CustomerCreateCommandHandler : ICommandHandler<CustomerCreateCommand>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly INationalCodeDuplicationChecker nationalCodeDuplicationChecker;

        public CustomerCreateCommandHandler(ICustomerRepository customerRepository,
            INationalCodeDuplicationChecker nationalCodeDuplicationChecker)
        {
            this.customerRepository = customerRepository;
            this.nationalCodeDuplicationChecker = nationalCodeDuplicationChecker;
        }
        public void Execute(CustomerCreateCommand command)
        {
            var customer = new Customer(nationalCodeDuplicationChecker, command.FirstName, command.LastName, command.NationalCode);
        }
    }
}

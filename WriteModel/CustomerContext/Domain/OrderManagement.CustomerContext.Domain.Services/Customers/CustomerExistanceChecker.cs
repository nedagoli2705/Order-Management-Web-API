using OrderManagement.CustomerContext.Domain.Customers.Services;

namespace OrderManagement.CustomerContext.Domain.Services.Customers
{
    public class CustomerExistanceChecker : ICustomerExistanceChecker
    {
        private readonly ICustomerRepository customerRepository;


        public CustomerExistanceChecker(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public bool IsCustomerExisted(Guid customerId)
        {
            return customerRepository.Contains(c => c.Id == customerId);
        }
    }
}

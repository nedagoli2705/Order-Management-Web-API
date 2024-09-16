using Framework.Domain;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using OrderManagement.CustomerContext.Domain.Customers.Services;


namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class Order : EntityBase<Order>
    {
        private readonly ICustomerExistanceChecker customerExistanceChecker;


        public Order(ICustomerExistanceChecker customerExistanceChecker,
            Guid customerId)
        {
            this.customerExistanceChecker = customerExistanceChecker;

            SetId();
            SetCustomerId(customerId);
        }

        private void SetCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new CustomerIsRequiredException();
            }
            if (!customerExistanceChecker.IsCustomerExisted(customerId))
            {
                throw new CustomerISNotExistException();
            }

            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }
    }
}

using Framework.Domain;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;


namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class Order : EntityBase<Order>
    {
        public Order(Guid customerId)
        {
            SetId();
            SetCustomerId(customerId);
        }

        private void SetCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new CustomerIsRequiredException();
            }

            CustomerId = customerId;
        }

        public Guid CustomerId { get; private set; }
    }
}

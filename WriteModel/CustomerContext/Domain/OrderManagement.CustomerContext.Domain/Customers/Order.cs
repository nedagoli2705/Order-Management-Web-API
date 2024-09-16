using Framework.Domain;
using OrderManagement.CustomerContext.Domain.Customers.Exceptions;
using OrderManagement.CustomerContext.Domain.Customers.Services;
using System.Linq;

namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class Order : EntityBase<Order>
    {
        private readonly ICustomerExistanceChecker customerExistanceChecker;

        public Order()
        {
            
        }
        public Order(ICustomerExistanceChecker customerExistanceChecker,
            Guid customerId,
            DateTime orderDate,
            List<OrderItem> items)
        {
            this.customerExistanceChecker = customerExistanceChecker;
            TotalAmount = 0;

            SetId();
            SetCustomerId(customerId);
            SetOrderDate(orderDate);
            SetItems(items);

        }

        

        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> Items { get; private set; }

        private void SetItems(List<OrderItem> items)
        {
            if (items.Count == 0)
            {
                throw new OrderShouldHaveAtLeastOneItemException();
            }

            Items = items;
            foreach (var item in items)
            {
                TotalAmount += item.Price;
            }
            //or this : TotalAmount = items.Sum(a => a.Price);
        }

        private void SetOrderDate(DateTime orderDate)
        {
            if (orderDate == DateTime.MinValue || orderDate == DateTime.MaxValue)
            {
                throw new OrderDateIsNotValidException();
            }
            OrderDate = orderDate;
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
    }
}

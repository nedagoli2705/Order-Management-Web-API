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

            SetId();
            SetCustomerId(customerId);
            SetOrderDate(orderDate);
            SetItems(items);

        }

        

        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public void UpdateOrder(DateTime orderDate, List<OrderItem> items)
        {
            SetOrderDate(orderDate);

            
            SetItems(items);
        }

        private void SetItems(List<OrderItem> items)
        {
            TotalAmount = 0;
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

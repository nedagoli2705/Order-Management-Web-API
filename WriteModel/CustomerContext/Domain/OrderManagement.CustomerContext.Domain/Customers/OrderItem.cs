

namespace OrderManagement.CustomerContext.Domain.Customers
{
    public class OrderItem
    {
        public OrderItem(string productName, decimal price)
        {
            ProductName = productName;
            Price = price;
        }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
    }
}

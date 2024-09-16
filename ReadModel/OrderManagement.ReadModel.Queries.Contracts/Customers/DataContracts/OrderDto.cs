namespace OrderManagement.ReadModel.Queries.Contracts.Customers.DataContracts
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}

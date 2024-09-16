
namespace OrderManagement.ReadModel.Queries.Contracts.Customers.DataContracts
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}

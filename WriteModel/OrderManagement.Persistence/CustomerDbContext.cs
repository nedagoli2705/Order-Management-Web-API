using Framework.Persistence;
using Microsoft.EntityFrameworkCore;
namespace OrderManagement.Persistence
{
    public class CustomerDbContext : DbContextBase
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
    }
}

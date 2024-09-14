using Framework.Persistence;
using Microsoft.EntityFrameworkCore;
namespace OrderManagementWebApi.Persistence
{
    public class CustomerDbContext : DbContextBase
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
    }
}

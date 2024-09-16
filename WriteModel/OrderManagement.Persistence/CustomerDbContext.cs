using Framework.AssemblyHelper;
using Framework.Persistence;
using Microsoft.EntityFrameworkCore;
namespace OrderManagement.Persistence
{
    public class CustomerDbContext : DbContextBase
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entityMapping = DetectEntityMapping();

            entityMapping.ForEach(a => { modelBuilder.ApplyConfiguration(a); });

        }


        protected List<dynamic> DetectEntityMapping()
        {
            var assemblyHelper = new AssemblyHelper("OrderManagement.");
            return assemblyHelper.GetTypes(typeof(EntityMappingBase<>))
                .Select(Activator.CreateInstance)
                .Cast<dynamic>()
                .ToList();
        }
    }
}

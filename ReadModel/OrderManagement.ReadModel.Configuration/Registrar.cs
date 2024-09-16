using Framework.Core.Mapper;
using Framework.Mapper;
using Framework.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.ReadModel.Queries.Contracts.Customers;
using OrderManagement.ReadModel.Queries.Facade.Customers;

namespace OrderManagement.ReadModel.Configuration
{
    public class Registrar : RegistrarBase<Registrar>, IRegistrar
    {
        public void Register(IServiceCollection services, string writeConnectionString, string readConnectionString)
        {
            services.AddDbContext<Context.Models.CustomerContext>(op =>
            {
                op.UseSqlServer(writeConnectionString);
                op.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<ICustomerQueryFacade, CustomerQueryFacade>();


            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = readConnectionString;
                options.InstanceName = "OrderManagementApp_"; 
            });
        }
    }
}

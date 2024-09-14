using System.Linq;
using OrderManagement.Persistence;
using Framework.Application;
using Framework.AssemblyHelper;
using Framework.Core.Application;
using Framework.Core.DependencyInjection;
using Framework.Core.Domain;
using Framework.Core.Persistence;
using Framework.Facade;
using Framework.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.DependencyInjection
{
    public abstract class RegistrarBase<TRegister> : IRegistrar
    {
        private readonly AssemblyHelper.AssemblyHelper assemblyHelper;


        protected RegistrarBase()
        {
            var nameSpaceSpell = typeof(TRegister).Namespace.Split('.');
            var schemaName = nameSpaceSpell[0] + "." + nameSpaceSpell[1];
            assemblyHelper = new AssemblyHelper.AssemblyHelper(schemaName);
        }


        public virtual void Register(IServiceCollection services, string connectionString)
        {
            RegisterPersistence(services, connectionString);
            RegisterFramework(services);
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterCommandHandlers(services);
            RegisterCommandFacade(services);
        }


        private void RegisterPersistence(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IDbContext, CustomerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });
        }


        private void RegisterFramework(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDiContainer, DiContainer>();
            services.AddScoped<ICommandBus, CommandBus>();
        }


        private void RegisterRepositories(IServiceCollection services)
        {
            var repositories = assemblyHelper.GetTypes(typeof(RepositoryBase<>));
            foreach (var repository in repositories)
            {
                var baseInterfaces = repository.GetInterfaces().Where(a => a.IsGenericType == false);
                foreach (var baseInterface in baseInterfaces)
                    services.AddScoped(baseInterface, repository);
            }
        }


        private void RegisterServices(IServiceCollection services)
        {
            var domainServices = assemblyHelper.GetClassByInterface(typeof(IDomainService))
                .Where(a => a.IsInterface == false);

            foreach (var service in domainServices)
            {
                var baseInterface = service.GetInterfaces().Single(a => a.GetMembers().Any());
                services.AddTransient(baseInterface, service);
            }
        }


        


        private void RegisterCommandHandlers(IServiceCollection services)
        {
            var commandHandlers = assemblyHelper.GetClassByInterface(typeof(ICommandHandler<>));
            foreach (var commandHandler in commandHandlers)
            {
                var baseInterface = commandHandler.GetInterfaces()[0];
                services.AddScoped(baseInterface, commandHandler);
            }
        }


        private void RegisterCommandFacade(IServiceCollection services)
        {
            var repositories = assemblyHelper.GetTypes(typeof(FacadeCommandBase)).Distinct();
            foreach (var repository in repositories)
            {
                var baseInterfaces = repository.GetInterfaces().Where(a => a.IsGenericType == false && a.Namespace.StartsWith(nameof(OrderManagement)));
                foreach (var baseInterface in baseInterfaces)
                    services.AddScoped(baseInterface, repository);
            }
        }
    }
}
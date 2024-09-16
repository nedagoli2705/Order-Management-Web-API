using Microsoft.Extensions.DependencyInjection;

namespace Framework.DependencyInjection
{
    public interface IRegistrar
    {
        void Register(IServiceCollection services, string writeConnectionString, string readConnectionString);
    }
}
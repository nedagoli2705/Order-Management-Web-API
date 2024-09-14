using Framework.Core.Application;
using Framework.Facade;
using OrderManagement.CustomerContext.Facade.Contract;

namespace OrderManagement.CustomerContext.Facade
{
    public class CustomerCommandFacade : FacadeCommandBase, ICustomerCommandFacade
    {
        public CustomerCommandFacade(ICommandBus commandBus) : base(commandBus)
        {
        }
    }
}

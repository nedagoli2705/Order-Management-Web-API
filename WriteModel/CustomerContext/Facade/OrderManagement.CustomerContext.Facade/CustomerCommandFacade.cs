using Framework.Core.Application;
using Framework.Facade;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.CustomerContext.ApplicationService.Contract.Customers;
using OrderManagement.CustomerContext.Facade.Contract;

namespace OrderManagement.CustomerContext.Facade
{
    [Route("api/Customer/[action]")]
    [ApiController]
    public class CustomerCommandFacade : FacadeCommandBase, ICustomerCommandFacade
    {
        public CustomerCommandFacade(ICommandBus commandBus) : base(commandBus)
        {
        }

        [HttpPost]
        public void CreateCustomer(CustomerCreateCommand command)
        {
            CommandBus.Dispatch(command);
        }

        [HttpPatch]
        public void UpdateCustomer(CustomerUpdateCommand command)
        {
            CommandBus.Dispatch(command);
        }

        [HttpDelete]
        public void DeleteCustomer(CustomerDeleteCommand command)
        {
            CommandBus.Dispatch(command);
        }

        [HttpPost]
        public void AddOrderToCustomer(AddOrderToCustomerCommand command)
        {
            CommandBus.Dispatch(command);
        }

        [HttpPatch]
        public void UpdateOrderOfCustomer(UpdateOrderOfCustomerCommand command)
        {
            CommandBus.Dispatch(command);
        }

        [HttpDelete]
        public void DeleteOrderFromCustomer(DeleteOrderFromCustomerCommand command)
        {
            CommandBus.Dispatch(command);
        }
    }
}

using System.Text.Json;
using Framework.Core.Mapper;
using Framework.Facade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OrderManagement.ReadModel.Context.Models;
using OrderManagement.ReadModel.Queries.Contracts.Customers;
using OrderManagement.ReadModel.Queries.Contracts.Customers.DataContracts;

namespace OrderManagement.ReadModel.Queries.Facade.Customers
{
    [Route("ordermanagement/api/Customers/[action]")]
    [ApiController]
    public class CustomerQueryFacade : FacadeQueryBase, ICustomerQueryFacade
    {
        private readonly CustomerContext db;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;
        private readonly ILogger<CustomerQueryFacade> logger;

        public CustomerQueryFacade(CustomerContext db, IMapper mapper, IDistributedCache cache, ILogger<CustomerQueryFacade> logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.cache = cache;
            this.logger = logger;
        }

        [HttpGet]
        public IList<CustomerDto> GetCustomers()
        {
            var cacheKey = "all_customers";
            IList<Customer> customers;

            var cachedCustomers = cache.GetString(cacheKey);
            if (cachedCustomers != null)
            {
                logger.LogInformation("Customers retrieved from cache.");
                customers = JsonSerializer.Deserialize<List<Customer>>(cachedCustomers);
            }
            else
            {
                // Fetch the customers from SQL Server if not found in cache
                customers = db.Customers.ToList();
                if (customers.Any())
                {
                    logger.LogInformation("Customers retrieved from database.");
                    // Cache the customer data with an expiration time of 5 minutes
                    var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(customers), cacheEntryOptions);
                }
            }

            return mapper.Map<CustomerDto, Customer>(customers);
        }
    }
}

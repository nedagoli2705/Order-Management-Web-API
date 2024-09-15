using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Persistence
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var basePath = Path.GetFullPath(Path.Combine(currentDirectory, "../..", "Api/OrderManagement.Api"));
            Console.WriteLine($"Using base path: {basePath}");

            var configurationBuilder = new ConfigurationBuilder().SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", true);

            var configuration = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<CustomerDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new CustomerDbContext(builder.Options);
        }
    }
}

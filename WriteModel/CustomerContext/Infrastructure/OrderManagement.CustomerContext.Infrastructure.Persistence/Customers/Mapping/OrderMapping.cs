using OrderManagement.CustomerContext.Domain.Customers;
using Framework.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace OrderManagement.CustomerContext.Infrastructure.Persistence.Customers.Mapping
{
    public class OrderMapping : EntityMappingBase<Order>
    {
        public override void Initiate(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id); // Primary key for Order

            // Mapping OrderItems as a JSON column
            builder.Property(o => o.Items)
                .HasConversion(
                    items => JsonSerializer.Serialize(items, (JsonSerializerOptions)null), // Serialize to JSON
                    json => JsonSerializer.Deserialize<List<OrderItem>>(json, (JsonSerializerOptions)null)) // Deserialize back
                .HasColumnType("nvarchar(max)");
        }
    }
}

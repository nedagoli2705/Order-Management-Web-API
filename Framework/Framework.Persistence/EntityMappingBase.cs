using Framework.Core.Domain;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Framework.Persistence
{
    public abstract class EntityMappingBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntityBase
    {
        public abstract void Initiate(EntityTypeBuilder<TEntity> builder);


        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var a = typeof(TEntity);
            var baseClassName = typeof(EntityBase<>).Name;
            if (a.BaseType != null && a.BaseType.Name == baseClassName && a.IsClass)
            {
                builder.Property("Id")
                    .HasColumnType(nameof(SqlDbType.UniqueIdentifier));

                builder.HasKey("Id");
            }
            builder.ToTable(typeof(TEntity).Name, typeof(TEntity).Namespace?.Split('.')[1]);
            Initiate(builder);
        }

        
    }
}

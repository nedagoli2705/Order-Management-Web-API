using Framework.Core.Domain;
using System;

namespace Framework.Domain
{
    public abstract class EntityBase<TEntity> : IEntityBase where TEntity : class
    {

        public EntityBase()
        {
        }

        public Guid Id { get; set; }

        protected void SetId()
        {
            Id = Guid.NewGuid();
        }

        protected void SetId(Guid id)
        {
            Id = id;
        }
    }
}

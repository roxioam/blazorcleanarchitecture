using Melnikov.Blazor.Clean.Domain.Common.Interfaces;

namespace Melnikov.Blazor.Clean.Domain.Common;

public abstract class EntityBase : IEntity;

public abstract class EntityBase<TId> : EntityBase, IEntity<TId> 
    where TId : IEquatable<TId>
{
    public virtual TId Id { get; set; }
}
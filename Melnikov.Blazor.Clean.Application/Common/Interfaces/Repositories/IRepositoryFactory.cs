using Melnikov.Blazor.Clean.Domain.Common.Interfaces;

namespace Melnikov.Blazor.Clean.Application.Common.Interfaces.Repositories;

public interface IRepositoryFactory<TEntity, in TId> : IRepositoryFactory<TEntity>
    where TEntity : class, IEntity<TId>
    where TId : IEquatable<TId>;

public interface IRepositoryFactory<TEntity>
    where TEntity : class, IEntity
{
    public IRepository<TEntity> CreateRepository();
}
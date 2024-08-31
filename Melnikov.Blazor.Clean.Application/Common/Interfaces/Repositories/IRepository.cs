using Ardalis.Specification;
using Melnikov.Blazor.Clean.Domain.Common.Interfaces;

namespace Melnikov.Blazor.Clean.Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity, TId> : IRepository<TEntity>
    where TEntity : class, Domain.Common.Interfaces.IEntity<TId>
    where TId : IEquatable<TId>
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetByIdsAsync(IList<TId>? ids, CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> ListAsync(int? pageNumber = null, int? pageSize = null,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}
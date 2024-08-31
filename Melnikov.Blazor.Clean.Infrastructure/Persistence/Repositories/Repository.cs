using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Melnikov.Blazor.Clean.Application.Common.Interfaces.Repositories;
using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TId>(IDbContextFactory<ApplicationDbContext> contextFactory)
    : Repository<TEntity>(contextFactory), IRepository<TEntity, TId>
    where TEntity : class, Domain.Common.Interfaces.IEntity<TId>
    where TId : IEquatable<TId>
{
    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<List<TEntity>> GetByIdsAsync(IList<TId>? ids, CancellationToken cancellationToken = default)
    {
        if (ids == null || ids.Count == 0)
        {
            return [];
        }
        
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().Where(e => ids.Contains(e.Id)).AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}

public class Repository<TEntity>(IDbContextFactory<ApplicationDbContext> contextFactory)
    : IRepository<TEntity>
    where TEntity : class, IEntity
{
    public async Task<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().AsNoTracking().WithSpecification(specification)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(int? pageNumber = null, int? pageSize = null,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (pageNumber is > 0 && pageSize is > 0)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().AsNoTracking().WithSpecification(specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().WithSpecification(specification)
            .CountAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().AnyAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> AnyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Set<TEntity>().WithSpecification(specification).AnyAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        
        context.Attach(entity);
        await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        
        context.Update(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IList<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.RemoveRange(entities);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.RemoveRange(await ListAsync(specification, cancellationToken: cancellationToken));
        await context.SaveChangesAsync(cancellationToken);
    }
}
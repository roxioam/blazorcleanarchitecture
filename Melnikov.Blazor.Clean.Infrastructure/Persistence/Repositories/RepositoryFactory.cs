using Melnikov.Blazor.Clean.Application.Common.Interfaces.Repositories;
using Melnikov.Blazor.Clean.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Melnikov.Blazor.Clean.Infrastructure.Persistence.Repositories;

public class RepositoryFactory<TEntity, TId>(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : RepositoryFactory<TEntity>(dbContextFactory), IRepositoryFactory<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : IEquatable<TId>
{
    public new IRepository<TEntity, TId> CreateRepository()
    {
        var args = new object[] { dbContextFactory };
        return (IRepository<TEntity, TId>)Activator.CreateInstance(typeof(Repository<TEntity, TId>), args)!;
    }
}

public class RepositoryFactory<TEntity>(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    : IRepositoryFactory<TEntity> 
    where TEntity : class, IEntity
{
    public IRepository<TEntity> CreateRepository()
    {
        var args = new object[] { dbContextFactory };
        return (IRepository<TEntity>)Activator.CreateInstance(typeof(Repository<TEntity>), args)!;
    }
}
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Api.TodoList.Data.Context.Contract
{
    public interface IDbContext
    {
        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

        EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity) where TEntity : class;
    }
}
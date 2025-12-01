using APLTechnical.Infrastructure.DataStorage.Context;
using APLTechnical.Infrastructure.DataStorage.Entities;
using APLTechnical.Infrastructure.DataStorage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLTechnical.Infrastructure.DataStorage;

public abstract class IdentityRepositoryBase<TEntity>(AplContext dbContext) : RepositoryBase<TEntity>(dbContext), IIdentityRepositoryBase<TEntity>
    where TEntity : IdentityEntity
{
    public async Task<TEntity> GetByIdAsync(long id)
    {
        // Ensure the DbSet (source) is not null so we don't pass a null IQueryable to EF methods.
        IQueryable<TEntity> query = DbSet ?? throw new InvalidOperationException("Repository DbSet is not initialized.");

        // Execute the query. The result is nullable because the entity may not exist.
        var entity = await query.FirstOrDefaultAsync(e => e.Id == id);

        // If not found, raise so we never return a null TEntity (method contract is non-nullable).
        return entity ?? throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with id {id} was not found.");
    }
}

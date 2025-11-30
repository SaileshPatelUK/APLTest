using APLTechnical.Infrastructure.DataStorage.Context;
using APLTechnical.Infrastructure.DataStorage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLTechnical.Infrastructure.DataStorage;

public abstract class RepositoryBase<TEntity>(AplContext context) : IRepositoryBase<TEntity>
    where TEntity : class
{
    private readonly AplContext _context = context;
    private readonly DbSet<TEntity>? _dbSet;

    protected DbSet<TEntity>? DbSet => _dbSet;

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        return entity is null
            ? throw new InvalidOperationException(
                $"Entity of type {typeof(TEntity).Name} with id {id} was not found.")
            : entity;
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}

namespace APLTechnical.Infrastructure.DataStorage.Interfaces;

public interface IRepositoryBase<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entityToUpdate);
    Task DeleteAsync(TEntity entityToDelete);
}

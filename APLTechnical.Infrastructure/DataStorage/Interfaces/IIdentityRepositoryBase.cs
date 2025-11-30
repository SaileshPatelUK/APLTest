using APLTechnical.Infrastructure.DataStorage.Entities;

namespace APLTechnical.Infrastructure.DataStorage.Interfaces;

public interface IIdentityRepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : IdentityEntity
{
    Task<TEntity> GetByIdAsync(long id);
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Entities;

namespace CoreTemplate.Infrastructure.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        //todo Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> Get(TKey id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TKey id);
    }
}

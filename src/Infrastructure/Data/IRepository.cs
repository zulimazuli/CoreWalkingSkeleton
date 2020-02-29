using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Specifications;

namespace CoreTemplate.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : AuditableEntity
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(IQueryParameters<TEntity> parameters);
        Task<TEntity> Get(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(object id);
        Task<int> CountAsync();
    }
}

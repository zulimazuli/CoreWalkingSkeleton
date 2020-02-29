using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreTemplate.ApplicationCore.Entities;
using CoreTemplate.ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Infrastructure.Data
{
    public class EfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : AuditableEntity
    {
        private readonly ApplicationDbContext _context;

        public EfCoreRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(IQueryParameters<TEntity> parameters)
        {
            var query = BuildQueryFromParameters(parameters);
            return await query.ToListAsync();
        }

        public async Task<TEntity> Get(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(object id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return null;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        private IQueryable<TEntity> BuildQueryFromParameters(IQueryParameters<TEntity> parameters)
        {
            return QueryBuilder<TEntity>.Build(_context.Set<TEntity>().AsQueryable(), parameters);
        }

    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;
using System.Linq.Dynamic.Core;

namespace TPHunter.WebServices.Shared.MainData.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity Entity)
        {
            Entity.CreateTime = DateTime.Now;
            Entity.LastChangeTime = Entity.CreateTime.Value;
            Entity.IsDeleted = false;
            await _dbSet.AddAsync(Entity);
            return Entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> Entities)
        {
            foreach (var entity in Entities)
            {
                entity.CreateTime = DateTime.Now;
                entity.LastChangeTime = entity.CreateTime.Value;
                entity.IsDeleted = false;
            }
            await _dbSet.AddRangeAsync(Entities);
            return Entities;
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> Predicate)
        {

            return await _dbSet.Where(Predicate).CountAsync(x => !x.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync(x => !x.IsDeleted);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Order order)
        {
            return await _dbSet.Where(x => !x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid ID)
        {
            return await _dbSet.FindAsync(ID);
        }

        public void Remove(TEntity Entity)
        {
            _dbSet.Remove(Entity);
        }

        public void RemoveRange(IEnumerable<TEntity> Entities)
        {
            _dbSet.RemoveRange(Entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> Predicate)
        {
            return await _dbSet.Where(x => !x.IsDeleted).FirstOrDefaultAsync(Predicate);
        }

        public TEntity Update(TEntity Entity)
        {
            Entity.LastChangeTime = DateTime.Now;
            _context.Entry(Entity).State = EntityState.Modified;
            return Entity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> Predicate, Order order)
        {
            return await _dbSet.Where(Predicate).Where(x => !x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllPrivateRemovedAsync(Order order)
        {
            return await _dbSet.Where(x => x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllPaginateAsync(Expression<Func<TEntity, bool>> Predicate, Order order, int page, int size)
        {
            page = (page < 1) ? 1 : page;
            size = (size < 1) ? 1 : size;
            return await _dbSet.Where(Predicate).Where(x => !x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(Guid[] IDs, Order order)
        {
            return await _dbSet.Where(x => IDs.Contains(x.ID)).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }

        public async Task<bool> Remove(Guid ID)
        {
            var model = await _dbSet.FindAsync(ID);
            if (model is null)
                return false;
            _dbSet.Remove(model);
            return true;
        }

        public void PrivateRemove(TEntity Entity)
        {
            Entity.IsDeleted = true;
            Entity.LastChangeTime = DateTime.Now;
            _context.Entry(Entity).State = EntityState.Modified;
        }
    }
}

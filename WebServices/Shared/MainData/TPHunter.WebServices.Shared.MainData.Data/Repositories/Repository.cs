using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;
using System.Linq.Dynamic.Core;

namespace TPHunter.WebServices.Shared.MainData.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.LastChangeTime = entity.CreateTime.Value;
            entity.IsDeleted = false;
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var addRangeAsync = entities.ToList();
            foreach (var entity in addRangeAsync)
            {
                entity.CreateTime = DateTime.Now;
                entity.LastChangeTime = entity.CreateTime.Value;
                entity.IsDeleted = false;
            }
            await _dbSet.AddRangeAsync(addRangeAsync);
            return addRangeAsync;
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {

            return await _dbSet.Where(predicate).CountAsync(x => !x.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync(x => !x.IsDeleted);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Order order=default)
        {
            order ??= new Order();
            return await _dbSet.Where(x => !x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid ıd)
        {
            return await _dbSet.FindAsync(ıd);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(x => !x.IsDeleted).FirstOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            entity.LastChangeTime = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, Order order=default)
        {
            order ??= new Order();
            return await _dbSet.Where(predicate).Where(x => !x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllPrivateRemovedAsync(Order order = default)
        {
            order ??= new Order();
            return await _dbSet.Where(x => x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllPaginateAsync(Expression<Func<TEntity, bool>> predicate, int page, int size,Order order= default)
        {
            order ??= new Order();
            page = (page < 1) ? 1 : page;
            size = (size < 1) ? 1 : size;
            return await _dbSet.Where(predicate).Where(x => !x.IsDeleted).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(Guid[] ds, Order order = default)
        {
            order ??= new Order();
            return await _dbSet.Where(x => ds.Contains(x.Id)).AsQueryable().OrderBy($"{order.OrderColumnName} {order.OrderColumnDirection}").ToListAsync();
        }

        public async Task<bool> Remove(Guid ıd)
        {
            var model = await _dbSet.FindAsync(ıd);
            if (model is null)
                return false;
            _dbSet.Remove(model);
            return true;
        }

        public void PrivateRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.LastChangeTime = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> IsDataExists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public IQueryable<TEntity> AsNoTracking => _dbSet.AsNoTracking();
    }
}

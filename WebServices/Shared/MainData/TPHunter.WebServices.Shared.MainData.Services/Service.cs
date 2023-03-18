using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;
using TPHunter.WebServices.Shared.MainData.Core.Services;
using TPHunter.WebServices.Shared.MainData.Core.UnitOfWorks;

namespace TPHunter.WebServices.Shared.MainData.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : EntityBase
    {
        private readonly IRepository<TEntity> _repository;
        public readonly IUnitOfWork UnitOfWork;
        public Service(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            _repository = repository;
        }
        public IQueryable<TEntity> AsNoTracking => _repository.AsNoTracking;
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await UnitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var addRangeAsync = entities.ToList();
            await _repository.AddRangeAsync(addRangeAsync);
            await UnitOfWork.CommitAsync();
            return addRangeAsync;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Order order=default)
        {
            return await _repository.GetAllAsync(order);
        }

        public async Task<IEnumerable<TEntity>> GetAllPaginateAsync(Expression<Func<TEntity, bool>> predicate, int page, int size,Order order=default)
        {
            return await _repository.GetAllPaginateAsync(predicate,page, size,order);
        }

        public async Task<IEnumerable<TEntity>> GetAllPrivateRemovedAsync(Order order = default)
        {
            return await _repository.GetAllPrivateRemovedAsync(order);
        }

        public async Task<TEntity> GetByIdAsync(Guid ıd)
        {
            return await _repository.GetByIdAsync(ıd);
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(Guid[] ds, Order order=default)
        {
            return await _repository.GetByIdsAsync(ds, order);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetCountAsync(predicate);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repository.GetCountAsync();
        }

        public void PrivateRemove(TEntity entity)
        {
            _repository.PrivateRemove(entity);
            UnitOfWork.Commit();
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            UnitOfWork.Commit();
        }

        public async Task<bool> Remove(Guid ıd)
        {
            var response = await _repository.Remove(ıd);
            await UnitOfWork.CommitAsync();
            return response;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            UnitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            var response = _repository.Update(entity);
            UnitOfWork.Commit();
            return response;

        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, Order order = default)
        {
            return await _repository.Where(predicate, order);
        }

        public async Task<bool> IsDataExists(Expression<Func<TEntity, bool>> predicate)
        {
            return await(_repository.IsDataExists(predicate));
        }
    }
}

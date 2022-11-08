using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        public readonly IUnitOfWork _unitOfWork;
        public Service(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<TEntity> AddAsync(TEntity Entity)
        {
            await _repository.AddAsync(Entity);
            await _unitOfWork.CommitAsync();
            return Entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> Entities)
        {
            await _repository.AddRangeAsync(Entities);
            await _unitOfWork.CommitAsync();
            return Entities;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Order Order)
        {
            return await _repository.GetAllAsync(Order);
        }

        public async Task<IEnumerable<TEntity>> GetAllPaginateAsync(Expression<Func<TEntity, bool>> Predicate, Order Order, int Page, int Size)
        {
            return await _repository.GetAllPaginateAsync(Predicate, Order, Page, Size);
        }

        public async Task<IEnumerable<TEntity>> GetAllPrivateRemovedAsync(Order Order)
        {
            return await _repository.GetAllPrivateRemovedAsync(Order);
        }

        public async Task<TEntity> GetByIdAsync(Guid ID)
        {
            return await _repository.GetByIdAsync(ID);
        }

        public async Task<IEnumerable<TEntity>> GetByIdsAsync(Guid[] IDs, Order Order)
        {
            return await _repository.GetByIdsAsync(IDs, Order);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> Predicate)
        {
            return await _repository.GetCountAsync(Predicate);
        }

        public async Task<int> GetCountAsync()
        {
            return await _repository.GetCountAsync();
        }

        public void PrivateRemove(TEntity Entity)
        {
            _repository.PrivateRemove(Entity);
            _unitOfWork.Commit();
        }

        public void Remove(TEntity Entity)
        {
            _repository.Remove(Entity);
            _unitOfWork.Commit();
        }

        public async Task<bool> Remove(Guid ID)
        {
            var response = await _repository.Remove(ID);
            await _unitOfWork.CommitAsync();
            return response;
        }

        public void RemoveRange(IEnumerable<TEntity> Entities)
        {
            _repository.RemoveRange(Entities);
            _unitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> Predicate)
        {
            return await _repository.SingleOrDefaultAsync(Predicate);
        }

        public TEntity Update(TEntity Entity)
        {
            var response = _repository.Update(Entity);
            _unitOfWork.Commit();
            return response;

        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> Predicate, Order Order)
        {
            return await _repository.Where(Predicate, Order);
        }
    }
}

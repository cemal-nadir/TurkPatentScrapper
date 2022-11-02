//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TPHunter.WebServices.Identity.API.Data;
//using TPHunter.WebServices.Identity.API.Services.Abstract;

//namespace TPHunter.WebServices.Identity.API.Services
//{
//    public class Service<TEntity>:IService<TEntity> where TEntity : class
//    {
//        private readonly ApplicationDbContext _dbContext;
//        private readonly DbSet<TEntity> _dbSet;
//        public Service(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//            _dbSet = _dbContext.Set<TEntity>();
//        }
//        public async Task<IEnumerable<TEntity>> GetAll()
//        {
//            return await _dbSet.ToListAsync();
//        }
//        public async Task<TEntity> GetById(int id)
//        {
//            return await _dbSet.FindAsync(id);
//        }
//        public async Task<TEntity> Insert(TEntity entity)
//        {
//            await _dbSet.AddAsync(entity);
//            await _dbContext.SaveChangesAsync();
//            return entity;
//        }
//        public async Task<TEntity> Update(TEntity entity)
//        {
//            _dbContext.Entry(entity).State = EntityState.Modified;
//            await _dbContext.SaveChangesAsync();
//            return entity;
//        }
//        public async Task Remove(TEntity entity)
//        {
//            _dbSet.Remove(entity);
//            await _dbContext.SaveChangesAsync();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsNoTracking { get;}

        /// <summary>
        /// Veritabanından ilgili id'ye ait verileri getirmek için kullanılır
        /// </summary>
        /// <param name="ıd">ID</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid ıd);
        /// <summary>
        /// Veritabanından verilen ID dizisindeki tüm verileri getirir
        /// </summary>
        /// <param name="ds">ID dizisi</param>
        /// <param name="order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByIdsAsync(Guid[] ds, Order order = default);

        /// <summary>
        /// Veritabanından ilgili bütün verileri çekmek için kullanılır
        /// </summary>
        /// <param name="order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Order order = default);
        /// <summary>
        /// Veritabanından verilen koşula uyan tüm verileri pagination tekniği ile getirir
        /// </summary>
        /// <param name="predicate">Koşul</param>
        /// <param name="order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <param name="page">Paginationda Gerekli Olan Sayfa Numarası</param>
        /// <param name="size">Paginationda Gerekli Olan Getirilecek Veri Adedi</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllPaginateAsync(Expression<Func<TEntity, bool>> predicate, int page, int size, Order order = default);

        /// <summary>
        /// Veritabanından verilen koşula uyan tüm verileri getirir
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, Order order = default);
        /// <summary>
        /// Veritabanından verilen koşula uyan ilk veriyi getirir
        /// </summary>
        /// <param name="predicate">Koşul</param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Veritabanına veri eklemek için kullanılır
        /// </summary>
        /// <param name="entity">Veri</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// Veritabanına çoklu veri eklemek için kullanılır
        /// </summary>
        /// <param name="entities">Veriler</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Veritabanından ilgili kaydın silinmesi sağlar
        /// </summary>
        /// <param name="entity">Veri</param>
        void Remove(TEntity entity);
        /// <summary>
        /// Veritabanından ilgili kaydın IsRemoved Stateini true olarak ayarlar(geçici silmek yada gizlemek diyebiliriz)
        /// </summary>
        /// <param name="entity">Veri</param>
        void PrivateRemove(TEntity entity);
        /// <summary>
        /// Veritabanından IsRemoved State'i true olan tüm verileri getirir(geçici silinen yada gizlenen veriler diyebiliriz)
        /// </summary>
        /// <param name="order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllPrivateRemovedAsync(Order order = default);
        /// <summary>
        /// Veritabanından ilgili ID'ye göre veri siler
        /// </summary>
        /// <param name="ıd"></param>
        Task<bool> Remove(Guid ıd);
        /// <summary>
        /// Veritabanından ilgili verilerin silinmesi sağlar
        /// </summary>
        /// <param name="entities">Veriler</param>
        void RemoveRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Veritabanından ilgili koşula uyan verilerin sayısını almak için kullanılır
        /// </summary>
        /// <param name="predicate">Koşul</param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Veritabanından ilgili tablodaki verilerin sayısını almak için kullanılır
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
        /// <summary>
        /// Veritabanındaki ilgili kaydın güncellenmesini sağlar
        /// </summary>
        /// <param name="entity">Veri</param>
        /// <returns></returns>
        TEntity Update(TEntity entity);
        /// <summary>
        /// Veritabanında verilen koşula göre ilgili kaydın var olup olmadığını kontrol eder
        /// </summary>
        /// <param name="predicate">Koşul</param>
        /// <returns></returns>
        Task<bool> IsDataExists(Expression<Func<TEntity, bool>> predicate);
    }
}

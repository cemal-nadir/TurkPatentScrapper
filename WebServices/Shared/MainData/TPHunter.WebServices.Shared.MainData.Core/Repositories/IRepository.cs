using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {


        /// <summary>
        /// Veritabanından ilgili id'ye ait verileri getirmek için kullanılır
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid ID);
        /// <summary>
        /// Veritabanından verilen ID dizisindeki tüm verileri getirir
        /// </summary>
        /// <param name="IDs">ID dizisi</param>
        /// <param name="Order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByIdsAsync(Guid[] IDs, Order Order);

        /// <summary>
        /// Veritabanından ilgili bütün verileri çekmek için kullanılır
        /// </summary>
        /// <param name="Order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Order Order);
        /// <summary>
        /// Veritabanından verilen koşula uyan tüm verileri pagination tekniği ile getirir
        /// </summary>
        /// <param name="Predicate">Koşul</param>
        /// <param name="Order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <param name="Page">Paginationda Gerekli Olan Sayfa Numarası</param>
        /// <param name="Size">Paginationda Gerekli Olan Getirilecek Veri Adedi</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllPaginateAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> Predicate, Order Order, int Page, int Size);

        /// <summary>
        /// Veritabanından verilen koşula uyan tüm verileri getirir
        /// </summary>
        /// <param name="Predicate"></param>
        /// <param name="Order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> Predicate, Order Order);
        /// <summary>
        /// Veritabanından verilen koşula uyan ilk veriyi getirir
        /// </summary>
        /// <param name="Predicate">Koşul</param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> Predicate);
        /// <summary>
        /// Veritabanına veri eklemek için kullanılır
        /// </summary>
        /// <param name="Entity">Veri</param>
        /// <returns></returns>
        Task<TEntity> AddAsync(TEntity Entity);
        /// <summary>
        /// Veritabanına çoklu veri eklemek için kullanılır
        /// </summary>
        /// <param name="Entities">Veriler</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> Entities);
        /// <summary>
        /// Veritabanından ilgili kaydın silinmesi sağlar
        /// </summary>
        /// <param name="Entity">Veri</param>
        void Remove(TEntity Entity);
        /// <summary>
        /// Veritabanından ilgili kaydın IsRemoved Stateini true olarak ayarlar(geçici silmek yada gizlemek diyebiliriz)
        /// </summary>
        /// <param name="Entity">Veri</param>
        void PrivateRemove(TEntity Entity);
        /// <summary>
        /// Veritabanından IsRemoved State'i true olan tüm verileri getirir(geçici silinen yada gizlenen veriler diyebiliriz)
        /// </summary>
        /// <param name="Order">Sıralama Yönü ve Kolon Adını İçeren Model</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllPrivateRemovedAsync(Order Order);
        /// <summary>
        /// Veritabanından ilgili ID'ye göre veri siler
        /// </summary>
        /// <param name="ID"></param>
        Task<bool> Remove(Guid ID);
        /// <summary>
        /// Veritabanından ilgili verilerin silinmesi sağlar
        /// </summary>
        /// <param name="Entities">Veriler</param>
        void RemoveRange(IEnumerable<TEntity> Entities);
        /// <summary>
        /// Veritabanından ilgili koşula uyan verilerin sayısını almak için kullanılır
        /// </summary>
        /// <param name="Predicate">Koşul</param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> Predicate);
        /// <summary>
        /// Veritabanından ilgili tablodaki verilerin sayısını almak için kullanılır
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
        /// <summary>
        /// Veritabanındaki ilgili kaydın güncellenmesini sağlar
        /// </summary>
        /// <param name="Entity">Veri</param>
        /// <returns></returns>
        TEntity Update(TEntity Entity);
    }
}

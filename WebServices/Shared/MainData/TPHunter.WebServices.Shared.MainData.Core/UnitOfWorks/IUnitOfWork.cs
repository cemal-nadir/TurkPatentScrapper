using System.Threading.Tasks;

namespace TPHunter.WebServices.Shared.MainData.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}

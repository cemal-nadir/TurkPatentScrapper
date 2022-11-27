using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Source.Scrapper.Abstract.Shared
{
    public interface ITurkPatentClientService
    {
        public Task<ISearchParam> GetTrademarkParam();
        public Task<ISearchParam> GetDesignParam();
        public Task<ISearchParam> GetPatentParam();
    }
}

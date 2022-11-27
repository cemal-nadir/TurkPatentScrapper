using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Source.Scrapper.Abstract.Main
{
   public interface IWorker
    {
        public Task Download(ISearchParam  searchParam);
        public Task Update(string[]applicationNumbers);
    }
}

using System.Threading.Tasks;

namespace TPHunter.Source.Scrapper.Abstract.Main
{
   public interface IWorker
    {
        public Task Download();
        public Task Update();
    }
}

using System.Collections.Generic;

namespace TPHunter.Source.Scrapper.Abstract.Shared
{
    public interface IPage<T> where T:class
    {
        public void Prepare();
        public void Search(object searchParam);
        public void Search(string applicationNumber);
        public bool CheckAndClickNext();
        public IEnumerable<T> ScrapMulti();
        public T ScrapSingle();
    }
}

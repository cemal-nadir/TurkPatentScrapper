using System.Collections.Generic;
using OpenQA.Selenium;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Source.Scrapper.Abstract.Shared
{
    public interface IPage<out T> where T:IModel
    {
        public void SetDriver(IWebDriver driver);
        public void Prepare();
        public void Search(ISearchParam searchParam);
        public void Search(string applicationNumber);
        public int GetDataCount();
        public bool CheckAndClickNext();
        public IEnumerable<T> ScrapMulti();
        public T ScrapSingle();
    }
}

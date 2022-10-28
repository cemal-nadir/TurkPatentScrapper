using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Scrapper.Abstract.Shared
{
    public interface IPage<T> where T:class
    {
        public void Prepare();
        public void Search(object searchParam);
        public IEnumerable<T> ScrapMulti();
        public T ScrapSingle();
    }
}

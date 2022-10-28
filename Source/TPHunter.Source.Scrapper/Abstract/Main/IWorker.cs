using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Scrapper.Abstract.Main
{
   public interface IWorker
    {
        public Task Download();
        public Task Update();
    }
}

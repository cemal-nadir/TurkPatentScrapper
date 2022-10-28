using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Abstract.Scrapper.Main;

namespace TPHunter.Source.Contract.Scrapper
{
    public class Proxy
    {
        private IWorker Worker { get; set; }

        public IWorker GetMarkaWorker()
        {
            if (Worker is null || Worker is not M)
                BrowserBase = new ChromeBase();

            return BrowserBase;
        }

    }
}

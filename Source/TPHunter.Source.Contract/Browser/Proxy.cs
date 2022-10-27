using Browser.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Contract.Browser
{
    public class Proxy
    {
        private IBrowserBase BrowserBase { get; set; }

        public IBrowserBase GetChromeBase()
        {
            if (BrowserBase is null|| BrowserBase is not ChromeBase)
                BrowserBase = new ChromeBase();

            return BrowserBase;
        }

    }
}

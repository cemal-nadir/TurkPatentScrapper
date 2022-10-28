using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Scrapper.Models
{
   public class ResponseListTableModel
    {
        public string ApplicationNumber { get; set; }
        public IWebElement DetailButton { get; set; }
    }
}

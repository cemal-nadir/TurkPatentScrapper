using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Core.Models.Scrapper
{
   public class PatentTransactionModel
    {
        public DateTime? Date { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string Transaction { get; set; }
    }
}

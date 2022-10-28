using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Scrapper.Models
{
   public class MarkTransactions
    {
        public string TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MarkTransactionDetail> MarkTransactionDetails { get; set; }
    }
}

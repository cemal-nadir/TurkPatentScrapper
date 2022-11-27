using System;
using System.Collections.Generic;

namespace TPHunter.Shared.Scrapper.Models
{
   public class MarkTransactionsModel
    {
        public string TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MarkTransactionDetail> MarkTransactionDetails { get; set; }
    }
}

using System;

namespace TPHunter.Shared.Scrapper.Models
{
   public class PatentTransactionModel
    {
        public DateTime? Date { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string Transaction { get; set; }
    }
}

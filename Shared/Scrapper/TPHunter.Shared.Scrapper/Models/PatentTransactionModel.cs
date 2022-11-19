using System;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
   public class PatentTransactionModel : IModel
    {
        public DateTime? Date { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string Transaction { get; set; }
    }
}

using System;

namespace TPHunter.Shared.Scrapper.Models
{
    public class DesignTransactionModel
    {
        public string TransactionType { get; set; }
        public string TransactionDetail { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string DescriptionDetail { get; set; }
    }
}

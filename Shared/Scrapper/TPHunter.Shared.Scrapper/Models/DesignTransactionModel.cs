using System;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
    public class DesignTransactionModel : IModel
    {
        public string TransactionType { get; set; }
        public string TransactionDetail { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string DescriptionDetail { get; set; }
    }
}

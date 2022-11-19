using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
   public class MarkTransactionDetail : IModel
    {
        public string DecisionReason { get; set; }
        public string AboutMark { get; set; }
    }
}

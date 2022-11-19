using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionDescription:EntityBase
    {
        public string Description { get; set; }
        public ICollection<TradeMarkTransaction> TradeMarkTransactions { get; set; }
    }
}

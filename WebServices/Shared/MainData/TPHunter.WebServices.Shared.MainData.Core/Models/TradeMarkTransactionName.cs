using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionName:EntityBase
    {
        public string Transaction { get; set; }
        public virtual ICollection<TradeMarkTransaction> TradeMarkTransactions { get; set; }
    }
}

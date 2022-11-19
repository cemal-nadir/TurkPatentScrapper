using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionType : EntityBase
    {
        public string Type { get; set; }
        public virtual ICollection<TradeMarkTransaction> TradeMarkTransactions { get; set; }
    }
}

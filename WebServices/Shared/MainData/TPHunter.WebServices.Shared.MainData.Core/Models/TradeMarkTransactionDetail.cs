using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionDetail:EntityBase
    {
        public string DecisionReason { get; set; }
        public string AboutMark { get; set; }
        public Guid TradeMarkTransactionId { get; set; }
        public virtual TradeMarkTransaction TradeMarkTransaction { get; set; }
    }
}

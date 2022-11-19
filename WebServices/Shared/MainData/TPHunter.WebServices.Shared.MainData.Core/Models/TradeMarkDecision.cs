using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkDecision:EntityBase
    {
        public string Decision { get; set; }
        public Guid? TradeMarkDecisionReasonId { get; set; }
        public virtual TradeMarkDecisionReason TradeMarkDecisionReason { get; set; }
        public virtual ICollection<TradeMark> TradeMarks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransaction:EntityBase
    {
        public Guid TradeMarkTransactionTypeId { get; set; }
        public virtual TradeMarkTransactionType TradeMarkTransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public Guid? TradeMarkTransactionNameId { get; set; }
        public virtual TradeMarkTransactionName TradeMarkTransactionName { get; set; }
        public Guid? TradeMarkTransactionDescriptionId { get; set; }
        public virtual TradeMarkTransactionDescription TradeMarkTransactionDescription { get; set; }
        public Guid TradeMarkId { get; set; }
        public virtual TradeMark TradeMark { get; set; }
        public virtual ICollection<TradeMarkTransactionDetail> TradeMarkTransactionDetails { get; set; }
       
    }
}

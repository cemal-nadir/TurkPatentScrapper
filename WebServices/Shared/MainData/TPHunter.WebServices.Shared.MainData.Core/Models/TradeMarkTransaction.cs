using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransaction:EntityBase
    {
        public Guid TradeMarkTransactionTypeID { get; set; }
        public virtual TradeMarkTransactionType TradeMarkTransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public Guid? TradeMarkTransactionNameID { get; set; }
        public virtual TradeMarkTransactionName TradeMarkTransactionName { get; set; }
        public Guid? TradeMarkTransactionDescriptionID { get; set; }
        public virtual TradeMarkTransactionDescription TradeMarkTransactionDescription { get; set; }
        public Guid TradeMarkID { get; set; }
        public virtual TradeMark TradeMark { get; set; }
        public virtual ICollection<TradeMarkTransactionDetail> TradeMarkTransactionDetails { get; set; }
       
    }
}

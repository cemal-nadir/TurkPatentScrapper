using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionDetail:EntityBase
    {
        public string DecisionReason { get; set; }
        public string AboutMark { get; set; }
        public Guid TradeMarkTransactionID { get; set; }
        public virtual TradeMarkTransaction TradeMarkTransaction { get; set; }
    }
}

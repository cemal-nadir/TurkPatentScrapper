using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionName:EntityBase
    {
        public string Transaction { get; set; }
        public virtual ICollection<TradeMarkTransaction> TradeMarkTransactions { get; set; }
    }
}

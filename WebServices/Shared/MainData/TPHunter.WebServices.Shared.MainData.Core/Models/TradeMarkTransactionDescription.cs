using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkTransactionDescription:EntityBase
    {
        public string Description { get; set; }
        public ICollection<TradeMarkTransaction> TradeMarkTransactions { get; set; }
    }
}

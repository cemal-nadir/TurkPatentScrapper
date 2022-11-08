using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkServices:EntityBase
    {
        public int? Class { get; set; }
        public string Service { get; set; }
        public Guid TradeMarkID { get; set; }
        public virtual TradeMark TradeMark { get; set; }
    }
}

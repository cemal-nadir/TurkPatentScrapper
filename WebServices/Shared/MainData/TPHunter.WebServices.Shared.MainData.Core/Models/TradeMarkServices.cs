using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkServices:EntityBase
    {
        public int? Class { get; set; }
        public string Service { get; set; }
        public Guid TradeMarkId { get; set; }
        public virtual TradeMark TradeMark { get; set; }
    }
}

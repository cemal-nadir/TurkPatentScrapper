using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkStatus:EntityBase
    {
        public string Status { get; set; }
        public virtual ICollection<TradeMark> TradeMarks { get; set; }
    }
}

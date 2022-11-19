using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
   public class TradeMarkPriortyCountry:EntityBase
    {
        public string Country { get; set; }
        public virtual ICollection<TradeMarkPriorty> TradeMarkPriorties { get; set; }
    }
}

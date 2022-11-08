using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMarkPriorty:EntityBase
    {
        public DateTime? Date { get; set; }
        public Guid? TradeMarkPriortyCountryID { get; set; }
        public virtual TradeMarkPriortyCountry TradeMarkPriortyCountry { get; set; }
        public string ApplicationNumber { get; set; }
        public virtual ICollection<TradeMark> TradeMarks { get; set; }

    }
}

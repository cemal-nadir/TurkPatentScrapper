using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Attorney:EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid? AttorneyCompanyID { get; set; }
        public virtual AttorneyCompany AttorneyCompany { get; set; }
        public virtual ICollection<TradeMark> TradeMarks { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
        public virtual ICollection<Patent> Patents { get; set; }
    }
}

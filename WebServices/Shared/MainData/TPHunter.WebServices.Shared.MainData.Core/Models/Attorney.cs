using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Attorney:EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid? AttorneyCompanyId { get; set; }
        public virtual AttorneyCompany AttorneyCompany { get; set; }
        public virtual ICollection<TradeMark> TradeMarks { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
        public virtual ICollection<Patent> Patents { get; set; }
    }
}

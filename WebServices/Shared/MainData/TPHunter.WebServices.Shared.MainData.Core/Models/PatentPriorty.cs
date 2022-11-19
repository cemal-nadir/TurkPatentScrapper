using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentPriorty:EntityBase
    {
        public Guid PatentId { get; set; }
        public virtual Patent Patent { get; set; }
        public DateTime? PriortyDate { get; set; }
        public string PriortyNumber { get; set; }
        public Guid? PatentPriortyCountryId { get; set; }
        public virtual PatentPriortyCountry PatentPriortyCountry { get; set; }

    }
}

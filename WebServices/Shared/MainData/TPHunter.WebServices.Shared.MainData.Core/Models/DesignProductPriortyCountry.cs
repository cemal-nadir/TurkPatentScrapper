using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignProductPriortyCountry:EntityBase
    {
        public string Country { get; set; }
        public virtual ICollection<DesignProduct> DesignProducts { get; set; }
    }
}

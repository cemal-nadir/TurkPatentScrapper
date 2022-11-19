using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Holder:EntityBase
    {
        public string HolderCode { get; set; }
        public string HolderName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<HolderRelation> HolderRelations { get; set; }
    }
}

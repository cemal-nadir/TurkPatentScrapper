using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Inventor:EntityBase
    {
        public string InventorCode { get; set; }
        public string InventorName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<InventorRelation> InventorRelations { get; set; }
    }
}

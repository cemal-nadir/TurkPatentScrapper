using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class LocarnoClass:EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<DesignProductClassesRelation> DesignProductClassesRelations { get; set; }
    }
}

using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
   public class DesignProductClassesRelation:EntityBase
    {
        public Guid LocarnoClassId { get; set; }
        public LocarnoClass LocarnoClass { get; set; }
        public Guid DesignProductId { get; set; }
        public DesignProduct DesignProduct { get; set; }
    }
}

using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignerRelation:EntityBase
    {
        public Guid DesignId { get; set; }
        public Guid DesignerId { get; set; }
        public virtual Design Design { get; set; }
        public virtual Designer Designer { get; set; }
    }
}

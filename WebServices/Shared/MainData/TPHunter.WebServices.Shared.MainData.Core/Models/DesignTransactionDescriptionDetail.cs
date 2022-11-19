using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransactionDescriptionDetail:EntityBase
    {
        public string Detail { get; set; }
        public virtual ICollection<DesignTransactionDescription> DesignTransactionDescriptions { get; set; }
    }
}

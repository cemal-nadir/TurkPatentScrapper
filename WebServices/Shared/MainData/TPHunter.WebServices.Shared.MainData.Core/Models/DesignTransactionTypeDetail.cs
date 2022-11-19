using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransactionTypeDetail:EntityBase
    {
        public string Detail { get; set; }
        public virtual ICollection<DesignTransactionType> DesignTransactionTypes { get; set; }
    }
}

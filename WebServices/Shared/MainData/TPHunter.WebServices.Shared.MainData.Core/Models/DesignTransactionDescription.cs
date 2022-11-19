using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransactionDescription : EntityBase
    {
        public string Description { get; set; }
        public Guid? DesignTransactionDescriptionDetailId { get; set; }
        public virtual DesignTransactionDescriptionDetail DesignTransactionDescriptionDetail { get; set; }
        public virtual ICollection<DesignTransaction> DesignTransactions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransactionType:EntityBase
    {
        public string Type { get; set; }
        public Guid? DesignTransactionTypeDetailId { get; set; }
        public virtual DesignTransactionTypeDetail DesignTransactionTypeDetail { get; set; }
        public virtual ICollection<DesignTransaction> DesignTransactions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransactionDescription : EntityBase
    {
        public string Description { get; set; }
        public Guid? DesignTransactionDescriptionDetailID { get; set; }
        public virtual DesignTransactionDescriptionDetail DesignTransactionDescriptionDetail { get; set; }
        public virtual ICollection<DesignTransaction> DesignTransactions { get; set; }
    }
}

using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentTransactionName:EntityBase
    {
        public string Transaction { get; set; }
        public virtual ICollection<PatentTransaction> PatentTransactions { get; set; }
    }
}

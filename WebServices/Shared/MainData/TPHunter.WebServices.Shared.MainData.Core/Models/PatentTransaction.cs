using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentTransaction:EntityBase
    {
        public Guid PatentId { get; set; }
        public virtual Patent Patent { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? NotificationDate { get; set; }
        public Guid PatentTransactionNameId { get; set; }
        public virtual PatentTransactionName PatentTransactionName { get; set; }
    }
}

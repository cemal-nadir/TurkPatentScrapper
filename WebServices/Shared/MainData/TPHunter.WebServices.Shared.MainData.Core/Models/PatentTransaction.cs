using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentTransaction:EntityBase
    {
        public Guid PatentID { get; set; }
        public virtual Patent Patent { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? NotificationDate { get; set; }
        public Guid PatentTransactionNameID { get; set; }
        public virtual PatentTransactionName PatentTransactionName { get; set; }
    }
}

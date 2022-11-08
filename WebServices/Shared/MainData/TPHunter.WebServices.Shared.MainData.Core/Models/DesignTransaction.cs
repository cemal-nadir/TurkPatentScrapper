using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransaction : EntityBase
    {
        public Guid DesignID { get; set; }
        public virtual Design Design { get; set; }
        public Guid DesignTransactionTypeID { get; set; }
        public virtual DesignTransactionType DesignTransactionType { get; set; }
        public DateTime? Date { get; set; }
        public Guid DesignTransactionDescriptionID { get; set; }
        public virtual DesignTransactionDescription DesignTransactionDescription { get; set; }
    }
}

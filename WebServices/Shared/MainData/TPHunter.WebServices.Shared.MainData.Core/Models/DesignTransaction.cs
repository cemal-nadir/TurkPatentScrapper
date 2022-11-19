using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignTransaction : EntityBase
    {
        public Guid DesignId { get; set; }
        public virtual Design Design { get; set; }
        public Guid DesignTransactionTypeId { get; set; }
        public virtual DesignTransactionType DesignTransactionType { get; set; }
        public DateTime? Date { get; set; }
        public Guid DesignTransactionDescriptionId { get; set; }
        public virtual DesignTransactionDescription DesignTransactionDescription { get; set; }
    }
}

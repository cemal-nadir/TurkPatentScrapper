using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Design:EntityBase, IItemBase
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string BulletinNumber { get; set; }
        public DateTime? BulletinDate { get; set; }
        public Guid? DesignStatusId { get; set; }
        public virtual DesignStatus DesignStatus { get; set; }
        public Guid? AttorneyId { get; set; }
        public virtual Attorney Attorney { get; set; }
        public virtual ICollection<DesignerRelation> DesignerRelations { get; set; }
        public virtual ICollection<DesignProduct> DesignProducts { get; set; }
        public virtual ICollection<DesignTransaction> DesignTransactions { get; set; }
    }
}

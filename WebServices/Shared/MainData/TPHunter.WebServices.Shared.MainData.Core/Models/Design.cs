using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Design:EntityBase
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string BulletinNumber { get; set; }
        public DateTime? BulletinDate { get; set; }
        public Guid? DesignStatusID { get; set; }
        public virtual DesignStatus DesignStatus { get; set; }
        public virtual ICollection<DesignerRelation>DesignerRelations { get; set; }
        public Guid? AttorneyID { get; set; }
        public virtual Attorney Attorney { get; set; }
        public virtual ICollection<DesignProduct> DesignProducts { get; set; }
        //Burada Kaldık
        public virtual ICollection<DesignTransaction> DesignTransactions { get; set; }
    }
}

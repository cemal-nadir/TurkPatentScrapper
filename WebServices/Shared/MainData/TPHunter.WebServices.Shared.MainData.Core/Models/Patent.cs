using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Patent : EntityBase,IItemBase
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public Guid? PatentApplicationTypeId { get; set; }
        public virtual PatentApplicationType PatentApplicationType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public Guid? PatentProtectionTypeId { get; set; }
        public PatentProtectionType PatentProtectionType { get; set; }
        public string EpcPublishNumber { get; set; }
        public string EpcApplicationNumber { get; set; }
        public string PctPublishNumber { get; set; }
        public string PctApplicationNumber { get; set; }
        public DateTime? PctPublishDate { get; set; }
        public virtual IEnumerable<InventorRelation> InventorRelations { get; set; }
        public string InventionTitle { get; set; }
        public string InventionSummary { get; set; }
        public Guid? AttorneyId { get; set; }
        public virtual Attorney Attorney { get; set; }
        public DateTime BulletinDate { get; set; }
        public virtual ICollection<PatentPriorty> PatentPriorties { get; set; }
        public virtual ICollection<PatentClassRelation> PatentClassRelations { get; set; }
        public virtual ICollection<PatentTransaction> PatentTransactions { get; set; }
        public virtual ICollection<PatentPublication> PatentPublications { get; set; }
        public virtual ICollection<PatentPayment> PatentPayments { get; set; }
        public virtual ICollection<PatentPdf> PatentPdFs { get; set; }
    }
}

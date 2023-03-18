using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class TradeMark: EntityBase, IItemBase
    {
        public string ApplicationNumber { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string InternationalRegistrationNumber { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DeclareBullettinDate { get; set; }
        public DateTime? RegistrationBullettinDate { get; set; }
        public string DeclareBullettinNumber { get; set; }
        public string RegistrationBullettinNumber { get; set; }
        public DateTime? ProtectionDate { get; set; }
        public Guid? TradeMarkStatusId { get; set; }
        public virtual TradeMarkStatus TradeMarkStatus { get; set; }
        public int[] Classes { get; set; }
        public Guid? TradeMarkTypeId { get; set; }
        public TradeMarkType TradeMarkType { get; set; }
        public string Name { get; set; }
        public Guid? AttorneyId { get; set; }
        public Attorney Attorney { get; set; }
        public Guid? TradeMarkDecisionId { get; set; }
        public virtual TradeMarkDecision TradeMarkDecision { get; set; }
        public Guid? TradeMarkPriortyId { get; set; }
        public virtual TradeMarkPriorty TradeMarkPriorty { get; set; }
        public virtual ICollection<TradeMarkServices> TradeMarkServices { get; set; }
        public virtual ICollection<TradeMarkTransaction> TradeMarkTransaction { get; set; }
        public string ImageId { get; set; }
    }
}

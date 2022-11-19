using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentPayment:EntityBase
    {
        public Guid PatentId { get; set; }
        public virtual Patent Patent { get; set; }
        public int? Queue { get; set; }
        public int? Year { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }
    }
}

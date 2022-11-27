using System;

namespace TPHunter.Shared.Scrapper.Models
{
    public class PatentPaymentModel
    {
        public string Queue { get; set; }
        public string Year { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaidAmount { get; set; }
    }
}

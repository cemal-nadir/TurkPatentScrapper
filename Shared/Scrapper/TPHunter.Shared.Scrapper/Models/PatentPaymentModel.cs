using System;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
    public class PatentPaymentModel : IModel
    {
        public string Queue { get; set; }
        public string Year { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaidAmount { get; set; }
    }
}

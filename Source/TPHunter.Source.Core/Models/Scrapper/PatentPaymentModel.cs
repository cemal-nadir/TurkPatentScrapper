using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Core.Models.Scrapper
{
    public class PatentPaymentModel
    {
        public string Queue { get; set; }
        public string Year { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaidAmount { get; set; }
    }
}

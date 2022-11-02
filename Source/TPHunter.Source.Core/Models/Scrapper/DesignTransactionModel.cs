using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Core.Models.Scrapper
{
    public class DesignTransactionModel
    {
        public string TransactionType { get; set; }
        public string TransactionDetail { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string DescriptionDetail { get; set; }
    }
}

using System;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
    public class PatentPriortyModel : IModel
    {
        public DateTime? PriortyDate { get; set; }
        public string PriortyNumber { get; set; }
        public string PriortyCountry { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Core.Models.Scrapper
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string[] LocarnoClass { get; set; }
        public string[] ProductImages { get; set; }
        public string PriortyApplicationNumber { get; set; }
        public string PriortyCountry { get; set; }
        public string ExhibitionName { get; set; }
        public string ExhibitionPlace { get; set; }
        public DateTime? ExhibitionDate { get; set; }
        public DateTime? FirstExhibitionDate { get; set; }
        public DateTime? PriortyDate { get; set; }
        public string PriortyType { get; set; }
        public bool IsProductApproved { get; set; }
    }
}

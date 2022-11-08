using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignProduct:EntityBase
    {
        public Guid DesignID { get; set; }
        public virtual Design Design { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DesignProductClassesRelation> DesignProductClassesRelations { get; set; }
        public virtual ICollection<DesignProductImage> DesignProductImages { get; set; }
        public string PriortyApplicationNumber { get; set; }
        public Guid? DesignPriortyCountryID { get; set; }
        public virtual DesignProductPriortyCountry DesignPriortyCountry { get; set; }
        public string ExhibitionName { get; set; }
        public string ExhibitionPlace { get; set; }
        public DateTime? ExhibitionDate { get; set; }
        public DateTime? FirstExhibitionDate { get; set; }
        public DateTime? PriortyDate { get; set; }
        public Guid? DesignProductPriortyTypeID { get; set; }
        public virtual DesignProductPriortyType DesignProductPriortyType { get; set; }
        public bool IsProductApproved { get; set; }
    }
}

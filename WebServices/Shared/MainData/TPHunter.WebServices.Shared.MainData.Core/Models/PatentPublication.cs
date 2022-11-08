using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentPublication:EntityBase
    {
        public Guid PatentID { get; set; }
        public virtual Patent Patent { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid PatentPublicationDescriptionID { get; set; }
        public virtual PatentPublicationDescription PatentPublicationDescription { get; set; }
    }
}

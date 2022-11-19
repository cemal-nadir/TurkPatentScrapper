using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentPublication:EntityBase
    {
        public Guid PatentId { get; set; }
        public virtual Patent Patent { get; set; }
        public DateTime? PublishDate { get; set; }
        public Guid PatentPublicationDescriptionId { get; set; }
        public virtual PatentPublicationDescription PatentPublicationDescription { get; set; }
    }
}

using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentPdf:EntityBase
    {
        public Guid PatentId { get; set; }
        public virtual Patent Patent { get; set; }
        public PdfType PdfType { get; set; }
        public string FileId { get; set; }
    }
}

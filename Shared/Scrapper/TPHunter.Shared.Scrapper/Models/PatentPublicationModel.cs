using System;
using TPHunter.Shared.Scrapper.Abstracts;
namespace TPHunter.Shared.Scrapper.Models
{
    public class PatentPublicationModel : IModel
    {
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
    }
}

using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
   public class MarkServicesModel : IModel
    {
        public int? Class { get; set; }
        public string Service { get; set; }
    }
}

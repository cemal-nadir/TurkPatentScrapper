using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
    public class HolderModel : IModel
    {
        public string HolderCode { get; set; }
        public string HolderName { get; set; }
        public string Address { get; set; }

    }
}

using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Models
{
    public class InventorModel : IModel
    {
        public string InventorCode { get; set; }
        public string InventorName { get; set; }
        public string Address { get; set; }
    }
}

using System;

namespace TPHunter.Shared.Scrapper.Abstracts
{
    public interface ISearchParam
    {
        public int? BulletinNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class SearchParam : ISearchParam
    {
        public int? BulletinNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

   
}

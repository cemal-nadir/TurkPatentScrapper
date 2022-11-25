using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Shared.Scrapper.Abstracts
{
    public interface ISearchParam
    {
    }

    public class BulletinParam : ISearchParam
    {
        public int BulletinNumber { get; set; }
    }

    public class DateRangeParam : ISearchParam
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

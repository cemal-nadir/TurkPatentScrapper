using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class InventorRelation:EntityBase
    {
        public Guid PatentID { get; set; }
        public virtual Patent Patent { get; set; }
        public Guid InventorID { get; set; }
        public virtual Inventor Inventor { get; set; }
    }
}

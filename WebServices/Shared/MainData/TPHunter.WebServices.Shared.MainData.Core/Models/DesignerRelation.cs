using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignerRelation:EntityBase
    {
        public Guid DesignID { get; set; }
        public Guid DesignerID { get; set; }
        public virtual Design Design { get; set; }
        public virtual Designer Designer { get; set; }
    }
}

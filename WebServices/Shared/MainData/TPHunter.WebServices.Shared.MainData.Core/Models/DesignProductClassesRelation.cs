using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
   public class DesignProductClassesRelation:EntityBase
    {
        public Guid LocarnoClassID { get; set; }
        public LocarnoClass LocarnoClass { get; set; }
        public Guid DesignProductID { get; set; }
        public DesignProduct DesignProduct { get; set; }
    }
}

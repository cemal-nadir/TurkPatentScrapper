using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignProductImage:EntityBase
    {
        public Guid DesignProductID { get; set; }
        public virtual DesignProduct DesignProduct { get; set; }
        public string ImageID { get; set; }
    }
}

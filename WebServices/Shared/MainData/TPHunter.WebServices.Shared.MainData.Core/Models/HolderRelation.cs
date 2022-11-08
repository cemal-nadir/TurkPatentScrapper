using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class HolderRelation:EntityBase
    {
        public Guid HolderID { get; set; }
        public virtual Holder Holder { get; set; }
        public Guid DataID { get; set; }
        public DataType DataType { get; set; }
        
    }
    public enum DataType
    {
        Trademark,
        Patent,
        Design
    }
}

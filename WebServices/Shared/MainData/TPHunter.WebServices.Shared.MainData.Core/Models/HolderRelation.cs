using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class HolderRelation:EntityBase
    {
        public Guid HolderId { get; set; }
        public virtual Holder Holder { get; set; }
        public Guid DataId { get; set; }
        public DataType DataType { get; set; }
        
    }
}

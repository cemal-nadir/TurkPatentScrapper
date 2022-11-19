using System;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignProductImage:EntityBase
    {
        public Guid DesignProductId { get; set; }
        public virtual DesignProduct DesignProduct { get; set; }
        public string ImageId { get; set; }
    }
}

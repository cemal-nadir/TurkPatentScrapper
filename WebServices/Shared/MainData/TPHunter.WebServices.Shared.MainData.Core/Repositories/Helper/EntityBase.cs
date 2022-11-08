using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper
{
    /// <summary>
    /// Tüm Entitylerin miras alacağı base sınıf
    /// </summary>
    public class EntityBase
    {
        public Guid ID { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

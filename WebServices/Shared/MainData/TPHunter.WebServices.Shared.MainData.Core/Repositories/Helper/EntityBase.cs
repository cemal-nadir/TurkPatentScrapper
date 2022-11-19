using System;

namespace TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper
{
    /// <summary>
    /// Tüm Entitylerin miras alacağı base sınıf
    /// </summary>
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

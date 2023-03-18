using System;

namespace TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper
{
    public interface IItemBase
    {
        public Guid? AttorneyId { get; set; }
        public string ApplicationNumber { get; set; }
    }
}

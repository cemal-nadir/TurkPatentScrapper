using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentClassType:EntityBase
    {
        public string Type { get; set; }
        public virtual ICollection<PatentClass> PatentClasses { get; set; }
    }
}

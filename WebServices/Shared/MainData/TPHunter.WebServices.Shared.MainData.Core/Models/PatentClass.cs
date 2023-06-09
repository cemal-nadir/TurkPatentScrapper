﻿using System;
using System.Collections.Generic;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class PatentClass:EntityBase
    {
        public Guid PatentClassTypeId { get; set; }
        public virtual PatentClassType PatentClassType { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PatentClassRelation> PatentClassRelations { get; set; }
    }
}

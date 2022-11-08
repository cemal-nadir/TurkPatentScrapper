﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class Designer:EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<DesignerRelation> DesignerRelations { get; set; }
    }
}

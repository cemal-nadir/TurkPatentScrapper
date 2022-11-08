﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.WebServices.Shared.MainData.Core.Repositories.Helper;

namespace TPHunter.WebServices.Shared.MainData.Core.Models
{
    public class DesignStatus:EntityBase
    {
        public string Status { get; set; }
        public virtual ICollection<Design> Designs { get; set; }
    }
}

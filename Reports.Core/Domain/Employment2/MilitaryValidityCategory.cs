﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment2
{
    public class MilitaryValidityCategory : AbstractEntityWithVersion
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
    }
}

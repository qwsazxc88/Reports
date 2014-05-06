﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class ForeignLanguage : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string LanguageName { get; set; }
        public virtual string Level { get; set; }
    }
}

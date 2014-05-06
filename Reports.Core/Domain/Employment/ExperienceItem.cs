using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class ExperienceItem : AbstractEntity
    {
        public virtual User User { get; set; }
    }
}

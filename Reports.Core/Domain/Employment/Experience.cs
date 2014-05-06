using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class Experience : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual IList<ExperienceItem> ExperienceItems { get; set; }
        public virtual int ExperienceYears { get; set; }
        public virtual int ExperienceMonths { get; set; }
        public virtual string WorkBookSeries { get; set; }
        public virtual string WorkBookNumber { get; set; }
        public virtual DateTime WorkBookDateOfIssue { get; set; }
        public virtual string WorkBookSupplementSeries { get; set; }
        public virtual string WorkBookSupplementNumber { get; set; }
        public virtual DateTime WorkBookSupplementDateOfIssue { get; set; }
    }
}

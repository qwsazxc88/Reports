using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain.Employment
{
    public class Managers : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual string ProbationaryPeriod { get; set; }
        public virtual string Salary { get; set; }
        public virtual string WorkCity { get; set; }
        public virtual string PersonalAddition { get; set; }
        public virtual string PositionAddition { get; set; }
        public virtual bool IsFront { get; set; }
        public virtual string Bonus { get; set; }
        public virtual bool IsLiable { get; set; }
        public virtual string RequestNumber { get; set; }
    }
}

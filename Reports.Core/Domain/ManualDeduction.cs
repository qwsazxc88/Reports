using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    public class ManualDeduction: AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual MissionReport MissionReport { get; set; }
        public virtual DateTime DeleteDate { get; set; }
        public virtual DateTime SendTo1C { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime DeductionDate { get; set; }
        public virtual Decimal AllSum { get; set; }
    }
}

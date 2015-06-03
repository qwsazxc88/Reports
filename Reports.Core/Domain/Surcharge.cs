using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Ручная доплата
    /// </summary>
    public class Surcharge : AbstractEntityWithVersion
    {
        public virtual DateTime EditDate { get; set; }
        public virtual DateTime SurchargeDate { get; set; }

        public virtual int Number { get; set; }
        public virtual decimal Sum { get; set; }
        public virtual DateTime? EmailSendToUserDate { get; set; }

        public virtual User User { get; set; }
        public virtual User Editor { get; set; }

        public virtual DateTime? SendTo1C { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual bool DeleteAfterSendTo1C { get; set; }
        public virtual MissionReport MissionReport { get; set; }
        public virtual Deduction Deduction { get; set; }
    }
}

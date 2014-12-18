using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class GpdAct : AbstractEntityWithVersion
    {
        public virtual int CreatorID { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual int EditorID { get; set; }
        public virtual string ActNumber { get; set; }
        public virtual DateTime? ActDate { get; set; }
        public virtual int GCID { get; set; }
        public virtual DateTime? ChargingDate { get; set; }
        public virtual DateTime? DateBegin { get; set; }
        public virtual DateTime? DateEnd { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string PurposePayment { get; set; }
        public virtual string ESSSNum { get; set; }
        public virtual int StatusID { get; set; }

        //комментарии
        public virtual IList<GpdActComment> Comments { get; set; }
    }
}

using System;

namespace Reports.Core.Domain
{
    public class TimesheetCorrectionComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual TimesheetCorrection TimesheetCorrection { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    public class Timesheet : AbstractEntityWithVersion
    {
        public virtual DateTime Month { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime? UserNotAcceptDate { get; set; }
        public virtual DateTime? PersonnelNotAcceptDate { get; set; }
        public virtual IList<TimesheetDay> Days { get; set; }
    }
}
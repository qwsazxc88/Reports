using System;

namespace Reports.Core.Domain
{
    public class WorkingDaysConstant : AbstractEntityWithVersion
    {
        public virtual DateTime Month { get; set; }
        public virtual int Days { get; set; }
        public virtual int Hours { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Режим работы подразделений.
    /// </summary>
    public class StaffDepartmentOperationModes : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentManagerDetails DepartmentManagerDetail { get; set; }
        public virtual int ModeType { get; set; }
        public virtual int WeekDay { get; set; }
        public virtual string WorkBegin { get; set; }
        public virtual string WorkEnd { get; set; }
        public virtual string BreakBegin { get; set; }
        public virtual string BreakEnd { get; set; }
        public virtual bool IsWorkDay { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

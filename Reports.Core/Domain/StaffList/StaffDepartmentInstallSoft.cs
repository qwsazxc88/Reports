using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник банковского ПО.
    /// </summary>
    public class StaffDepartmentInstallSoft : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

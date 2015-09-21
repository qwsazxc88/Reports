using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Связи групп и установленного банковского ПО.
    /// </summary>
    public class StaffDepartmentSoftGroupLinks : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentInstallSoft InstallSoft { get; set; }
        public virtual StaffDepartmentSoftGroup SoftGroup { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

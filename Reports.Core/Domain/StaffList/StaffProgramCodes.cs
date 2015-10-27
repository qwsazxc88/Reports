using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник кодов совместимых программ.
    /// </summary>
    public class StaffProgramCodes : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentManagerDetails DepartmentManagerDetail { get; set; }
        public virtual StaffProgramReference Program { get; set; }
        public virtual string Code { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

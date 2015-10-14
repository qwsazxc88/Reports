using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник бизнес-групп
    /// </summary>
    public class StaffDepartmentBusinessGroup : AbstractEntityWithVersion
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual StaffDepartmentAdministration DepartmentAdministration { get; set; }
        public virtual Department Department { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

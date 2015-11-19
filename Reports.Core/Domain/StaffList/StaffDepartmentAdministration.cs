using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник управлений.
    /// </summary>
    public class StaffDepartmentAdministration : AbstractEntityWithVersion
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual StaffDepartmentManagement DepartmentManagement { get; set; }
        public virtual Department Department { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

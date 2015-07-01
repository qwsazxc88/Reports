using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Операции проводимые подразделением
    /// </summary>
    public class StaffDepartmentOperationLinks : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentManagerDetails DepartmentManagerDetail { get; set; }
        public virtual StaffDepartmentOperations DepartmentOperation { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

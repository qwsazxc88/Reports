using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник принадлежностей подразделения.
    /// </summary>
    public class StaffDepartmentAccessory : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

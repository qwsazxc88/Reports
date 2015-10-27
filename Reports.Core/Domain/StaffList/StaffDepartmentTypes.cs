using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник типов подразделений.
    /// </summary>
    public class StaffDepartmentTypes : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник видов заявок для подразделений.
    /// </summary>
    public class StaffDepartmentRequestTypes : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

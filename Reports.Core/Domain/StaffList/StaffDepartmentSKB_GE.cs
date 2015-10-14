using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник СКБ/GE.
    /// </summary>
    public class StaffDepartmentSKB_GE : AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

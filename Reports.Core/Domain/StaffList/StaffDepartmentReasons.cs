using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник причин внесения.
    /// </summary>
    public class StaffDepartmentReasons : AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

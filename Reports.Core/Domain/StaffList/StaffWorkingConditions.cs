using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник видов условий труда.
    /// </summary>
    public class StaffWorkingConditions : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
    }
}

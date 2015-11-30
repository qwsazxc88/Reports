using System;
using System.Collections.Generic;


namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник действий с надбавками.
    /// </summary>
    public class StaffExtraChargeActions : AbstractEntity
    {
        public virtual string Name { get; set; }
    }
}

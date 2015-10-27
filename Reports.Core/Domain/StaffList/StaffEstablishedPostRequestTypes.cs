using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник видов заявок для штатных единиц.
    /// </summary>
    public class StaffEstablishedPostRequestTypes : AbstractEntityWithVersion
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

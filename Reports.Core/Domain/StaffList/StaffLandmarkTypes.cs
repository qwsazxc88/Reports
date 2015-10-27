using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник видов ориентиров.
    /// </summary>
    public class StaffLandmarkTypes : AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Ориентиры подразделений.
    /// </summary>
    public class StaffDepartmentLandmarks : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentManagerDetails DepartmentManagerDetail { get; set; }
        public virtual StaffLandmarkTypes LandmarkTypes { get; set; }
        public virtual string Description { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

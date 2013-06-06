using System;

namespace Reports.Core.Domain
{
    public class EmploymentComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Employment Employment { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
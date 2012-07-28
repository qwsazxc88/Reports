using System;

namespace Reports.Core.Domain
{
    public class AbsenceComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Absence Absence { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
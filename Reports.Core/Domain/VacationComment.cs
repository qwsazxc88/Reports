using System;

namespace Reports.Core.Domain
{
    public class VacationComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Vacation Vacation { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
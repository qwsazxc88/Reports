using System;

namespace Reports.Core.Domain
{
    public class NameChange : AbstractEntityWithVersion
    {
        public virtual string PreviousName { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual string Place { get; set; }
        public virtual string Reason { get; set; }
    }
}
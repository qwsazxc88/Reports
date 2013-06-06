using System;

namespace Reports.Core.Domain
{
    public class WorkingGraphic : AbstractEntity
    {
        public virtual int UserId { get; set; }
        public virtual DateTime Day { get; set; }
        public virtual float? Hours { get; set; }
    }
}
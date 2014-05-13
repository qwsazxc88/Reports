using System;

namespace Reports.Core.Domain
{
    public class TerraGraphic : AbstractEntity
    {
        public virtual int UserId { get; set; }
        public virtual DateTime Day { get; set; }
        public virtual decimal Hours { get; set; }
        public virtual int? PointId { get; set; }
        public virtual decimal? FactHours { get; set; }
        public virtual int? FactPointId { get; set; }
        public virtual bool? IsCreditAvailable { get; set; }
    }
}
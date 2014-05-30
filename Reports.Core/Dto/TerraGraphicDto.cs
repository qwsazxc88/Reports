using System;

namespace Reports.Core.Dto
{
    public class TerraGraphicDbDto
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime Day { get; set; }
        public virtual decimal? Hours { get; set; }
        public virtual int? PointId { get; set; }
        public virtual decimal? FactHours { get; set; }
        public virtual int? FactPointId { get; set; }
        public virtual bool? IsCreditAvailable { get; set; }
        public virtual string PointName { get; set; }
        public virtual string PointTitle { get; set; }
        public virtual string FactPointName { get; set; }
        public virtual string FactPointTitle { get; set; }
    }
}
using System;

namespace Reports.Core.Dto
{
    public class TerraGraphicDbDto
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime Day { get; set; }
        public virtual int Hours { get; set; }
        public virtual int? PointId { get; set; }
        public virtual bool? IsCreditAvailable { get; set; }
        public virtual string PointName { get; set; }
        public virtual string PointTitle { get; set; }
    }
}
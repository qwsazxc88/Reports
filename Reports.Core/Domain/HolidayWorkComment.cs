using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к работе выходные и праздничные дни
    /// </summary>
    public class HolidayWorkComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual HolidayWork HolidayWork { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
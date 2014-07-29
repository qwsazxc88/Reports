using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к отчету (найм)
    /// </summary>
    public class AppointmentReportComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual AppointmentReport AppointmentReport { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
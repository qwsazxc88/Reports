using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к заявке (найм)
    /// </summary>
    public class AppointmentComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
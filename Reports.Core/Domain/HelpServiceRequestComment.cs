using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к заявке на услугу
    /// </summary>
    public class HelpServiceRequestComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual HelpServiceRequest Request { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к увольнению
    /// </summary>
    public class DismissalComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Dismissal Dismissal { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
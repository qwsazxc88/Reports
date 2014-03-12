using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к командировке
    /// </summary>
    public class MissionComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual Mission Mission { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
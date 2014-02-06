using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий кприказу на командировку
    /// </summary>
    public class MissionOrderComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual MissionOrder MissionOrder { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
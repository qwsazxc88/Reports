using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Комментарий к авансовому отчету
    /// </summary>
    public class MissionReportComment : AbstractEntityWithVersion
    {
        public virtual User User { get; set; }
        public virtual MissionReport MissionReport { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
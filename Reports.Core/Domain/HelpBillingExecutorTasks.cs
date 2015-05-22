using System;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Список ответственных за задачу в биллинге.
    /// </summary>
    public class HelpBillingExecutorTasks : AbstractEntityWithVersion
    {
        public virtual User Worker { get; set; }
        //public virtual int HelpBillingId { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
    }
}

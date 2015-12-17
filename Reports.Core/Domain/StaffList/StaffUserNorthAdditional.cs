using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Журнал северных надбавок(%) для сотрудников.
    /// </summary>
    public class StaffUserNorthAdditional : AbstractEntity
    {
        public virtual User User { get; set; }
        public virtual DateTime? DateCalculate { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual DateTime? CreateDate { get; set; }
    }
}

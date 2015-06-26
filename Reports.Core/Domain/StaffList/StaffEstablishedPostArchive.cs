using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Архив справочника штатных единиц.
    /// </summary>
    public class StaffEstablishedPostArchive : AbstractEntity
    {
        public virtual StaffEstablishedPost StaffEstablishedPost { get; set; }
        public virtual Position Position { get; set; }
        public virtual Department Department { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Decimal Salary { get; set; }
        public virtual Decimal StaffECSalary { get; set; }
        public virtual bool IsUsed { get; set; }
        public virtual DateTime? BeginAccountDate { get; set; }
        public virtual int Priority { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
    }
}

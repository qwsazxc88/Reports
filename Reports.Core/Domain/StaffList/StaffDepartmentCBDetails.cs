using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Реквизиты ЦВ к заявке подразделения.
    /// </summary>
    public class StaffDepartmentCBDetails : AbstractEntityWithVersion
    {
        public virtual StaffDepartmentRequest DepRequest { get; set; }
        public virtual int? ATMCountTotal { get; set; }
        public virtual int? ATMCashInStarted { get; set; }
        public virtual int? ATMCashInCount { get; set; }
        public virtual int? ATMCount { get; set; }
        public virtual Department DepCashin { get; set; }
        public virtual Department DepATM { get; set; }
        public virtual DateTime? CashInStartedDate { get; set; }
        public virtual DateTime? ATMStartedDate { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
    }
}

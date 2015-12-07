using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Заявки по штатным единицам.
    /// </summary>
    public class StaffEstablishedPostRequest : AbstractEntityWithVersion
    {
        public virtual DateTime? DateRequest { get; set; }
        public virtual StaffEstablishedPostRequestTypes RequestType { get; set; }
        /// <summary>
        /// Справочник штатных единиц.
        /// </summary>
        public virtual StaffEstablishedPost StaffEstablishedPost { get; set; }
        public virtual Position Position { get; set; }
        public virtual Department Department { get; set; }
        public virtual Schedule Schedule { get; set; }//график работы
        public virtual StaffWorkingConditions WorkingCondition { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Decimal Salary { get; set; }
        public virtual bool IsUsed { get; set; }
        public virtual bool IsDraft { get; set; }
        public virtual DateTime? DateSendToApprove { get; set; }
        public virtual DateTime? DateAccept { get; set; }
        public virtual DateTime? BeginAccountDate { get; set; }
        public virtual AppointmentReason Reason { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual IList<StaffEstablishedPostChargeLinks> PostChargeLinks { get; set; }
    }
}

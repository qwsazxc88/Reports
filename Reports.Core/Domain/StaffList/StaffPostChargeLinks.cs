using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Связи надбавок с сотрудниками.
    /// </summary>
    public class StaffPostChargeLinks : AbstractEntity
    {
        public virtual User Staff { get; set; }
        public virtual StaffExtraCharges ExtraCharges { get; set; }
        public virtual Decimal Salary { get; set; }
        /// <summary>
        /// Действия с надбавкой.
        /// </summary>
        public virtual StaffExtraChargeActions ExtraChargeActions { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual StaffMovements StaffMovements { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Связь надбавок с штатными единицами
    /// </summary>
    public class StaffEstablishedPostChargeLinks : AbstractEntityWithVersion
    {
        public virtual StaffEstablishedPostRequest EstablishedPostRequest { get; set; }
        public virtual StaffEstablishedPost EstablishedPost { get; set; }
        public virtual StaffExtraCharges ExtraCharges { get; set; }
        public virtual decimal? Amount { get; set; }
        public virtual bool IsUsed { get; set; }
        /// <summary>
        /// Действия с надбавкой.
        /// </summary>
        public virtual StaffExtraChargeActions ExtraChargeActions { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }        
    }
}

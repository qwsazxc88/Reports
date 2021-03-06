﻿using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник штатных единиц.
    /// </summary>
    public class StaffEstablishedPost : AbstractEntityWithVersion
    {
        public virtual Position Position { get; set; }
        public virtual Department Department { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Decimal Salary { get; set; }
        public virtual bool IsUsed { get; set; }
        public virtual DateTime? BeginAccountDate { get; set; }
        public virtual int Priority { get; set; }
        public virtual User Creator { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual User Editor { get; set; }
        public virtual DateTime? EditDate { get; set; }
        public virtual IList<StaffEstablishedPostChargeLinks> PostChargeLinks { get; set; }
        public virtual IList<StaffEstablishedPostArchive> EstablishedPostArchive { get; set; }
        public virtual IList<StaffEstablishedPostRequest> EstablishedPostRequest { get; set; }
        /// <summary>
        /// Связи штатной единицы с сотрудниками
        /// </summary>
        public virtual IList<StaffEstablishedPostUserLinks> EstablishedPostUserLinks { get; set; }
    }
}

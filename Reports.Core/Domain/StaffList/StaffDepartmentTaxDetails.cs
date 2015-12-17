using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Налоговые реквизиты.
    /// </summary>
    public class StaffDepartmentTaxDetails : AbstractEntity
    {
        public virtual Department Department { get; set; }
        public virtual string KPP { get; set; }
        public virtual string OKTMO { get; set; }
        public virtual string OKATO { get; set; }
        public virtual string OKPO { get; set; }
        public virtual string RegionCode { get; set; }
        public virtual string TaxAdminCode { get; set; }
        public virtual string TaxAdminName { get; set; }
        public virtual string PostAddress { get; set; }
    }
}

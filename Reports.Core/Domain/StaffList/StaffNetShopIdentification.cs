using System;
using System.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Справочник видов идентификаций сетевых магазинов.
    /// </summary>
    public class StaffNetShopIdentification : AbstractEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime? CreateDate { get; set; } 
    }
}

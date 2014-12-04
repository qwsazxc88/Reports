using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Роли для ручной привязки
    /// </summary>
    public class ManualRole : AbstractEntityWithVersion
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }        
        public virtual int? DaysForApproval { get; set; }
    }
}

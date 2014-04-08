using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Расширенные роли пользователей в дополнение к заданным через RoleId
    /// </summary>
    public class MissionOrderRole : AbstractEntityWithVersion
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }        
        public virtual int? DaysForApproval { get; set; }
    }
}

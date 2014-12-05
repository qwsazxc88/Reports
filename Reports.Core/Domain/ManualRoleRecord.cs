using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Ручные привязки ролей
    /// </summary>
    public class ManualRoleRecord : AbstractEntityWithVersion
    {
        // RoleOwner
        public virtual User User { get; set; }

        public virtual ManualRole Role { get; set; }

        public virtual User TargetUser { get; set; }

        public virtual Department TargetDepartment { get; set; }
    }
}

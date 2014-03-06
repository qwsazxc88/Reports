using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Расширенные роли пользователей в дополнение к заданным через RoleId
    /// </summary>
    public class ClearanceChecklistRoleRecord : AbstractEntityWithVersion
    {
        // RoleOwner
        public virtual User User { get; set; }

        public virtual ClearanceChecklistRole Role { get; set; }

        public virtual int? DaysForApproval { get; set; }

        public virtual User TargetUser { get; set; }

        public virtual Department TargetDepartment { get; set; }
    }
}

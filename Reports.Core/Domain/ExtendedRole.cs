using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Reports.Core.Domain
{
    /// <summary>
    /// Расширенные роли пользователей в дополнение к заданным через RoleId
    /// </summary>
    public class ExtendedRole : AbstractEntityWithVersion
    {
        public virtual string Description { get; set; }

        // Role Owners
        public virtual ICollection<User> RoleOwners { get; set; }

        /*public ExtendedRole()
        {
            this.RoleOwners = new List<User>();
        }*/
    }
}

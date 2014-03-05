using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IClearanceChecklistRoleDao : IDao<ClearanceChecklistRole>
    {
        IList<ClearanceChecklistRole> GetClearanceChecklistRoles();
        IList<User> GetClearanceChecklistRoleAuthorities(ClearanceChecklistRole clearanceChecklistRole);
    }
}

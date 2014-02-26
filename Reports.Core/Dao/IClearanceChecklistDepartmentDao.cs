using System;
using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IClearanceChecklistDepartmentDao : IDao<ClearanceChecklistDepartment>
    {
        IList<ClearanceChecklistDepartment> GetClearanceChecklistDepartments();
        IList<User> GetClearanceChecklistDepartmentAuthorities(int clearanceChecklistDepartmentId);
    }
}

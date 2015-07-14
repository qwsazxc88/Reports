using System.Collections.Generic;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public interface IDepartmentDao : IDao<Department>
    {
        //IList<Department> LoadAllSorted();
        IList<Department> SearchByName(string name);
        Department SearchByNameDistinct(string name);
        Department GetRootDepartment();
        IList<Department> GetDepartmentsTree(int departmentId);
        IList<Department> SearchByParentId(int parentId);
        IList<User> GetDepartmentManagers(int departmentId, bool allLevels = false);
        IList<Reports.Core.Dto.DepartmentDto> GetDepartmentsForManager23(int managerId, int level, bool dep3only);
        Department GetParentDepartmentWithLevel(Department dep, int level);
    }
}
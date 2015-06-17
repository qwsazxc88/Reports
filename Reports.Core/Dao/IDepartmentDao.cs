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
        Department GetParentDepartmentWithLevel(Department dep, int level);
        /// <summary>
        /// Достаем подразделение по коду
        /// </summary>
        /// <param name="Code">Код</param>
        /// <returns></returns>
        Department GetByCode(string Code);
    }
}
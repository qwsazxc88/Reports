using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

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
        /// <summary>
        /// Достаем уровень подразделений из СКД с привязкой к точкам из Финграда по заданному родителю.
        /// </summary>
        /// <param name="DepId">Код родительского подразделения.</param>
        /// <returns></returns>
        IList<DepartmentWithFigradPointsDto> GetDepartmentWithFingradPoint(string DepId);
    }
}
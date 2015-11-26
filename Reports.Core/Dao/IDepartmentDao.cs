﻿using System.Collections.Generic;
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
        IList<Reports.Core.Dto.DepartmentDto> GetDepartmentsForManager23(int managerId, int level, bool dep3only);
        Department GetParentDepartmentWithLevel(Department dep, int level);
        IList<Terrapoint_DepartmentDto> GetTP_D_list();
        IList<Department_TerrapointDto> GetD_TP_list();
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
        /// <summary>
        /// Подсчет количества штатных единиц в пределах указанного подразделения.
        /// </summary>
        /// <param name="Id">Id подразделения</param>
        /// <returns></returns>
        int DepPositionCount(int Id);
        /// <summary>
        /// Достаем уровень подразделений с полями из финграда.
        /// </summary>
        /// <param name="Id">Id родительского подразделения</param>
        /// <param name="IsParentDepOnly">Признак достать только родительское подразделение.</param>
        /// <returns></returns>
        IList<StaffListDepartmentDto> DepFingradName(string Id, bool IsParentDepOnly);
    }
}
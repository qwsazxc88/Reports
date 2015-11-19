using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник филиалов.
    /// </summary>
    public interface IStaffDepartmentBranchDao : IDao<StaffDepartmentBranch>
    {
        /// </summary>
        /// Филиалы.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentBranchDto> GetDepartmentBranches();
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
        /// <summary>
        /// Достаем запись из справочника кодировок филиалов по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение ниже 2 уровня.</param>
        /// <returns></returns>
        StaffDepartmentBranch GetDepartmentBranchByDeparment(Department Dep);
        /// <summary>
        /// Формируем новый код для филиала.
        /// </summary>
        /// <returns></returns>
        string GetNewBranchCode();
    }
}

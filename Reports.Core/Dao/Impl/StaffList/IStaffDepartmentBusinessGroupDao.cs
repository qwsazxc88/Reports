using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник бизнес-групп
    /// </summary>
    public interface IStaffDepartmentBusinessGroupDao : IDao<StaffDepartmentBusinessGroup>
    {
        /// </summary>
        /// Бизнс-группы.
        /// </summary>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        IList<StaffDepartmentBusinessGroupDto> GetDepartmentBusinessGroups(int AdminFilterId, int ManagementFilterId, int BranchFilterId);
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
        /// <summary>
        /// Достаем запись из справочника кодировок бизнес-групп по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение 5 уровня</param>
        /// <returns></returns>
        StaffDepartmentBusinessGroup GetDepartmentBusinessGroupByDeparment(Department Dep);
        /// <summary>
        /// Формируем новый код для бизнес-группы.
        /// </summary>
        /// <param name="Administration">Управление</param>
        /// <returns></returns>
        string GetNewBusinessGroupCode(StaffDepartmentAdministration Administration);
    }
}

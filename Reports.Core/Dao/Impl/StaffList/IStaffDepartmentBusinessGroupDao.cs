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
        /// <returns></returns>
        IList<StaffDepartmentBusinessGroupDto> GetDepartmentBusinessGroups(int AdminFilterId, int ManagementFilterId, int BranchFilterId);
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
    }
}

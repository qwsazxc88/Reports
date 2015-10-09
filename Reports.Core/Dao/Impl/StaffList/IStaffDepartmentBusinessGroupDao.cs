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
        /// <returns></returns>
        IList<StaffDepartmentBusinessGroupDto> GetDepartmentBusinessGroups();
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
    }
}

using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник групп операций
    /// </summary>
    public interface IStaffDepartmentOperationGroupsDao : IDao<StaffDepartmentOperationGroups>
    {
        /// <summary>
        /// Список групп операций.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentOperationGroupsDto> GetOperationGroups();
    }
}

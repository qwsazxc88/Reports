using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник операций подразделений.
    /// </summary>
    public interface IStaffDepartmentOperationsDao : IDao<StaffDepartmentOperations>
    {
        /// <summary>
        /// Список операций.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentOperationsDto> GetOperations();
    }
}

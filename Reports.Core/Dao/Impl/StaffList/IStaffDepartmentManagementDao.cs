using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// справочник дирекций.
    /// </summary>
    public interface IStaffDepartmentManagementDao : IDao<StaffDepartmentManagement>
    {
        /// </summary>
        /// Справочник дирекций.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentManagementDto> GetDepartmentManagements();
    }
}

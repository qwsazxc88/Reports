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
        /// Список причин занесения в справочник подразделений.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentBranchDto> GetDepartmentBranches();
    }
}

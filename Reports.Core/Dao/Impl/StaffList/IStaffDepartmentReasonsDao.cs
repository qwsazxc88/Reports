using Reports.Core.Domain;
using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник причин внесения.
    /// </summary>
    public interface IStaffDepartmentReasonsDao : IDao<StaffDepartmentReasons>
    {
        /// <summary>
        /// Список причин занесения в справочник подразделений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetDepartmentReasons();
    }
}

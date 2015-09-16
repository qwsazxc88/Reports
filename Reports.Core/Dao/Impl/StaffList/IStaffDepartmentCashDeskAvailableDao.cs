using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник видов наличия кассы.
    /// </summary>
    public interface IStaffDepartmentCashDeskAvailableDao : IDao<StaffDepartmentCashDeskAvailable>
    {
        /// <summary>
        /// Список видов наличия кассы.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetCashDeskAvailable();
    }
}

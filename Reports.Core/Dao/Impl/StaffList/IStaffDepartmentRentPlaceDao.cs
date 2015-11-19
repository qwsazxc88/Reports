using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Арендованное помещение.
    /// </summary>
    public interface IStaffDepartmentRentPlaceDao : IDao<StaffDepartmentRentPlace>
    {
        /// <summary>
        /// Список признаков арендованных помещений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetRentPlace();
    }
}

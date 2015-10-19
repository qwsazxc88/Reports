using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник принадлежностей подразделения.
    /// </summary>
    public interface IStaffDepartmentAccessoryDao : IDao<StaffDepartmentAccessory>
    {
        /// <summary>
        /// Список принадлежностей.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetAccessoryes();
    }
}

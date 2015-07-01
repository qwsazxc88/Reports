using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник типов подразделений.
    /// </summary>
    public interface IStaffDepartmentTypesDao : IDao<StaffDepartmentTypes>
    {
        /// <summary>
        /// Список типов подразделений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetDepartmentTypes();
    }
}

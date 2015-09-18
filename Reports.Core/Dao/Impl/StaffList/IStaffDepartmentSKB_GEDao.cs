using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник СКБ/GE.
    /// </summary>
    public interface IStaffDepartmentSKB_GEDao : IDao<StaffDepartmentSKB_GE>
    {
        /// <summary>
        /// Список СКБ/GE.
        /// </summary>
        /// <returns></returns>
        IList<IdNameDto> GetSKB_GE();
    }
}

using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Связи групп и установленного банковского ПО.
    /// </summary>
    public interface IStaffDepartmentSoftGroupLinksDao : IDao<StaffDepartmentSoftGroupLinks>
    {
        /// <summary>
        /// Связи ПО с группами.
        /// </summary>
        /// <param name="GroupId">Id группы ПО.</param>
        /// <returns></returns>
        IList<StaffDepartmentSoftGroupLinksDto> GetSoftGroupLinks(int GroupId);
    }
}

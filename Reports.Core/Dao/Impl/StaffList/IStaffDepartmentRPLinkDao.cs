using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник РП-привязок
    /// </summary>
    public interface IStaffDepartmentRPLinkDao : IDao<StaffDepartmentRPLink>
    {
        /// </summary>
        /// РП-привязки.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentRPLinkDto> GetDepartmentRPLinks();
    }
}

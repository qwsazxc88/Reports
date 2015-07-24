using Reports.Core.Domain;
using System.Collections.Generic;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник штатных единиц.
    /// </summary>
    public interface IStaffEstablishedPostDao : IDao<StaffEstablishedPost>
    {
        /// <summary>
        /// Список штатных единиц к подразделению.
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        IList<StaffEstablishedPostDto> GetStaffEstablishedPosts(int DepartmentId);
    }
}

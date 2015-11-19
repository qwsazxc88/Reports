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
        /// <summary>
        /// Список сотрудников с должностями к подразделению.
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        IList<StaffEstablishedPostDto> GetStaffEstablishedArrangements(int DepartmentId);
        /// <summary>
        /// Достаем список сотрудников, закрепленных за данной штатной единицей.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        IList<User> GetEstablishedPostUsed(int SEPId);
    }
}

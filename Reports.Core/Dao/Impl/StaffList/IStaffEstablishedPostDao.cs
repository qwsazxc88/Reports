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
        /// <param name="DepartmentId">Id подразделения</param>
        /// <param name="IsSalaryEnable">Признак показа окладов.</param>
        /// <returns></returns>
        IList<StaffEstablishedPostDto> GetStaffEstablishedPosts(int DepartmentId, bool IsSalaryEnable);
        /// <summary>
        /// Список сотрудников с должностями к подразделению.
        /// </summary>
        /// <param name="DepartmentId">Id подразделения</param>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        IList<StaffEstablishedPostDto> GetStaffEstablishedArrangements(int DepartmentId, int SEPId);
        /// <summary>
        /// Достаем количество сотрудников, закрепленных за данной штатной единицей.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        int GetEstablishedPostUsed(int SEPId);
        /// <summary>
        /// Достаем связи штатной единицы и сотрудников.
        /// </summary>
        /// <param name="SEPId">Id штатной единицы.</param>
        /// <returns></returns>
        IList<StaffEstablishedPostUserLinks> GetEstablishedPostUserLinks(int SEPId);
    }
}

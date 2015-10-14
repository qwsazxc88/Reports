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
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
    }
}

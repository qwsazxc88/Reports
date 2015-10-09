using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// справочник управлений.
    /// </summary>
    public interface IStaffDepartmentAdministrationDao : IDao<StaffDepartmentAdministration>
    {
        /// </summary>
        /// Управления.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentAdministrationDto> GetDepartmentAdministrations();
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
    }
}

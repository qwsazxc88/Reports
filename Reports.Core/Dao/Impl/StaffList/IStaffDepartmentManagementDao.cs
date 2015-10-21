using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// справочник дирекций.
    /// </summary>
    public interface IStaffDepartmentManagementDao : IDao<StaffDepartmentManagement>
    {
        /// </summary>
        /// Справочник дирекций.
        /// </summary>
        /// <returns></returns>
        IList<StaffDepartmentManagementDto> GetDepartmentManagements();
        /// <summary>
        /// Проверка на доступность удаления данной строки.
        /// </summary>
        /// <param name="Id">Id удаляемой строки</param>
        /// <returns></returns>
        bool IsEnableDelete(int Id);
        /// <summary>
        /// Достаем запись из справочника кодировок дирекций по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение ниже 3 уровня.</param>
        /// <returns></returns>
        StaffDepartmentManagement GetDepartmentManagementByDeparment(Department Dep);
        /// <summary>
        /// Формируем новый код для дирекции.
        /// </summary>
        /// <param name="Branch">Филиал</param>
        /// <returns></returns>
        string GetNewManagementCode(StaffDepartmentBranch Branch);
    }
}

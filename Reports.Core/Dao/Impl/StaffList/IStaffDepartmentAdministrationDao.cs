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
        /// <summary>
        /// Достаем запись из справочника кодировок управлений по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение 4 уровня</param>
        /// <returns></returns>
        StaffDepartmentAdministration GetDepartmentAdministrationByDeparment(Department Dep);
        /// <summary>
        /// Формируем новый код для управления.
        /// </summary>
        /// <param name="Management">Дирекция</param>
        /// <returns></returns>
        string GetNewAdministrationCode(StaffDepartmentManagement Management);
    }
}

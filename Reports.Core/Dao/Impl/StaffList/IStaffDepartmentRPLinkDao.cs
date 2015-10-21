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
        /// <summary>
        /// Достаем запись из справочника кодировок РП-привязок по подразделению из СКД
        /// </summary>
        /// <param name="Dep">Подразделение 6 уровня</param>
        /// <returns></returns>
        StaffDepartmentRPLink GetDepartmentRPLinkByDeparment(Department Dep);
        /// <summary>
        /// Формируем новый код для РП-привязки.
        /// </summary>
        /// <param name="BusinessGroup">Бизнес-группа</param>
        /// <returns></returns>
        string GetNewRPLinkCode(StaffDepartmentBusinessGroup BusinessGroup);
    }
}

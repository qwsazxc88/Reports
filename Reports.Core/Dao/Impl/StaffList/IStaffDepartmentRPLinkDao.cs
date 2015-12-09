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
        /// <param name="BGFilterId">Id бизнес-группы.</param>
        /// <param name="AdminFilterId">Id управления.</param>
        /// <param name="ManagementFilterId">Id дирекции</param>
        /// <param name="BranchFilterId">Id филиала</param>
        /// <returns></returns>
        IList<StaffDepartmentRPLinkDto> GetDepartmentRPLinks(int BGFilterId, int AdminFilterId, int ManagementFilterId, int BranchFilterId);
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
        /// <summary>
        /// Достаем структуру финграда для нашего подразделения
        /// </summary>
        /// <param name="DepartmentId">Id подразделения 6 уровня</param>
        /// <returns></returns>
        StaffDepartmentFingradStructureDto GetFingradStructureForDeparment(int DepartmentId);
    }
}

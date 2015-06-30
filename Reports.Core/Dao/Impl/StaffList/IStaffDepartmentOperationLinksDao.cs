using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Операции проводимые подразделением.
    /// </summary>
    public interface IStaffDepartmentOperationLinksDao : IDao<StaffDepartmentOperationLinks>
    {
        /// <summary>
        /// Достаем список операций для подразделения по заявке.
        /// </summary>
        /// <param name="DMDetailId">Id текущей заявки.</param>
        /// <returns></returns>
        IList<DepOperationDto> GetDepartmentOperationLinks(int DMDetailId);
    }
}

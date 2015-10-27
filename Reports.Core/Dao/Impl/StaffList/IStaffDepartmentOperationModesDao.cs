using System.Collections.Generic;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Режим работы подразделения.
    /// </summary>
    public interface IStaffDepartmentOperationModesDao : IDao<StaffDepartmentOperationModes>
    {
        /// <summary>
        /// Достаем список операций для подразделения по заявке.
        /// </summary>
        /// <param name="DMDetailId">Id управленческих реквизитов текущей заявки.</param>
        /// <returns></returns>
        IList<DepOperationModeDto> GetDepartmentOperationModes(int DMDetailId);
    }
}

using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник банковского ПО.
    /// </summary>
    public interface IStaffDepartmentInstallSoftDao : IDao<StaffDepartmentInstallSoft>
    {
        /// <summary>
        /// Список Установленного ПО.
        /// </summary>
        /// <returns></returns>
        IList<IdNameWithOldNameDto> GetInstallSoft();
    }
}

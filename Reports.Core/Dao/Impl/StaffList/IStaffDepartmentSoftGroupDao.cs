using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;


namespace Reports.Core.Dao
{
    /// <summary>
    /// Справочник групп банковского ПО.
    /// </summary>
    public interface IStaffDepartmentSoftGroupDao : IDao<StaffDepartmentSoftGroup>
    {
        /// <summary>
        /// Список признаков арендованных помещений.
        /// </summary>
        /// <returns></returns>
        IList<IdNameWithOldNameDto> GetSoftGroups();
    }
}

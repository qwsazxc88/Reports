using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IHolidayWorkDao : IDao<HolidayWork>
    {
        IList<VacationDto> GetDocuments(
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int statusId);
    }
}
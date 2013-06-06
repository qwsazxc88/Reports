using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface ITimesheetCorrectionDao : IDao<TimesheetCorrection>
    {
        IList<VacationDto> GetDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate,
            int sortedBy,
            bool? sortDescending);
    }
}
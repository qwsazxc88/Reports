using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionDao : IDao<Mission>
    {
        IList<VacationDto> GetDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate);
    }
}
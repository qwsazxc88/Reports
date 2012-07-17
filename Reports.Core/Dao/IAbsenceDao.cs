using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IAbsenceDao : IDao<Absence>
    {
        IList<VacationDto> GetDocuments(
            UserRole role,
            int departmentId,
            int positionId,
            int absenceTypeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate);
    }
}
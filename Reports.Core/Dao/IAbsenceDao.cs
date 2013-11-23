using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IAbsenceDao : IDao<Absence>
    {
        IList<VacationDto> GetDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int absenceTypeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName, 
            int sortedBy,
            bool? sortDescending);

        IList<BeginEndDateDto> LoadForUserAndPeriod(DateTime beginDate, DateTime endDate, int userId);
    }
}
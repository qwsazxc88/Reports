using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IChildVacationDao : IDao<ChildVacation>
    {
        IList<VacationDto> GetDocuments(int userId, UserRole role, int departmentId, int positionId, int vacationTypeId,
                                        int requestStatusId,
                                        DateTime? beginDate, DateTime? endDate, string userName, 
                                        int sortedBy, bool? sortDescending);
        IList<ChildVacation> LoadForIdsList(List<int> ids);
    }
}
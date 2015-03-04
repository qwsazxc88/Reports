using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IVacationDao : IDao<Vacation>
    {
        IList<VacationDto> GetDocuments(int userId, UserRole role, int departmentId, int positionId, int vacationTypeId,
                                        int requestStatusId,
                                        DateTime? beginDate, DateTime? endDate, string userName, 
                                        int sortedBy,bool? sortDescending, string Number);

        int GetRequestCountsForUserAndDates(DateTime beginDate,
                                            DateTime endDate, int userId, int vacationId,bool isChildVacantion);
        IList<Vacation> LoadForIdsList(List<int> ids);
        bool ResetApprovals(int id);
    }
}
using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IDismissalDao : IDao<Dismissal>
    {
        IList<VacationDto> GetDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int statusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName, 
            int sortedBy,
            bool? sortDescending,
            string Number
            );

        DateTime? GetDismissalDateForUser(int userId);
        IList<Dismissal> LoadForIdsList(List<int> ids);
    }
}
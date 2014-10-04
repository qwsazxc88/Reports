using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface ISicklistDao : IDao<Sicklist>
    {
        IList<SicklistDto> GetSicklistDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int statusId,
            int paymentPercentTypeId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName, 
            int sortedBy,
            bool? sortDescending
            );
        bool ResetApprovals(int id);
        IList<Sicklist> LoadForIdsList(List<int> ids);
        int GetRequestCountsForUserAndDates(DateTime beginDate, DateTime endDate, int userId, int requestId);
    }
}
using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IMissionDao : IDao<Mission>
    {
        IList<MissionDto> GetDocuments(
            int userId, 
            UserRole role,
            int departmentId,
            int positionId,
            int typeId,
            int requestStatusId,
            DateTime? beginDate,
            DateTime? endDate,
            string userName, 
            int sortedBy,
            bool? sortDescending);

        int SetRecalculateDate(List<int> idsForApprove);
    }
}
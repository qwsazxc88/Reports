using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IClearanceChecklistDao : IDao<ClearanceChecklist>
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
            bool? sortDescending
        );

        ClearanceChecklistApproval GetApprovalById(int id);
        ISet<User> GetClearanceChecklistApprovingAuthorities();

        bool SetApproval(int approvalId, int approvedBy, out ClearanceChecklistApprovalDto modifiedApproval);
        bool SetComment(int approvalId, string comment);
    }
}

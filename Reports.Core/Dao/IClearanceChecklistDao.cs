using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IClearanceChecklistDao : IDao<Dismissal>
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
        IList<User> GetClearanceChecklistApprovingAuthorities();

        bool SetApproval(int approvalId, int approvedBy, out ClearanceChecklistApprovalDto modifiedApproval);
        bool SetComment(int approvalId, string comment);
        bool SetBottomFields(int clearanceChecklistId, int? registryNumber, decimal? personalIncomeTax, string oKTMO);

        IList<ClearanceChecklistRole> GetClearanceChecklistRoles();
        IList<User> GetClearanceChecklistRoleAuthorities(ClearanceChecklistRole clearanceChecklistRole);
    }
}

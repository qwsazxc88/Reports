using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ClearanceChecklistDao : DefaultDao<ClearanceChecklist>, IClearanceChecklistDao
    {
        public ClearanceChecklistDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList<VacationDto> GetDocuments(
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
            )
        {
            // TODO Replace this stub with necessary code
            string sqlQuery =
                string.Format(sqlSelectForListClearanceChecklist,
                                DeleteRequestText,
                                "v.[CreateDate]",
                                "Обходной лист",
                                "[dbo].[ClearanceChecklist]",
                                "'" + DateTime.MinValue.ToShortDateString() + "'",
                                "'" + DateTime.MinValue.ToShortDateString() + "'",
                                typeId
                );
            return GetDefaultDocuments(userId, role, departmentId,
                positionId, typeId,
                statusId, beginDate, endDate, userName,
                sqlQuery, sortedBy, sortDescending);
            //return new List<VacationDto>();
        }

        public ClearanceChecklistApproval GetApprovalById(int id)
        {
            //var Approval = new ClearanceChecklistApproval();
            var approval = Session.Get<ClearanceChecklistApproval>(id);
            return approval;            
        }

        public bool SetApproval(int approvalId, int approvedBy, out ClearanceChecklistApprovalDto modifiedApproval)
        {
            var approval = GetApprovalById(approvalId);
            //string sqlQuery = string.Format("update {0} set {1}={2} where {3}={4}", "[dbo].[ClearanceChecklistApproval]", );
            if (approval != null)
            {
                var transaction = Session.BeginTransaction();
                approval.ApprovalDate = DateTime.Now;
                approval.ApprovedBy = Session.Get<User>(approvedBy);
                Session.Update(approval);
                transaction.Commit();
                modifiedApproval = new ClearanceChecklistApprovalDto { Id = approvalId,
                    ApprovalDate = approval.ApprovalDate.HasValue ? approval.ApprovalDate.Value.ToString("dd.MM.yyyy") : "",
                    ApprovedBy = approval.ApprovedBy.FullName
                };
                return true;
            }
            else
            {
                modifiedApproval = new ClearanceChecklistApprovalDto();
                return false;
            }
        }
    }
}

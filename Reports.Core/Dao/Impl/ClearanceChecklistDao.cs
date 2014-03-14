using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ClearanceChecklistDao : DefaultDao<Dismissal>, IClearanceChecklistDao
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
            string sqlQuery =
                string.Format(sqlSelectForListClearanceChecklist,
                                DeleteRequestText,
                                "v.[CreateDate]",
                                "Обходной лист",
                                "[dbo].[Dismissal]",
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

     
        /// <summary>
        /// Get all authorities who have the right to approve clearance checklists
        /// </summary>
        /// <returns>A collection of all authorities who have the right to approve clearance checklists</returns>
        public IList<User> GetClearanceChecklistApprovingAuthorities()
        {
            IList<User> clearanceChecklistApprovingAuthorities = new List<User>();
            clearanceChecklistApprovingAuthorities =
                Session.CreateCriteria<User>().List<User>()
                .Where<User>(user => user.ClearanceChecklistRoleRecords != null && user.ClearanceChecklistRoleRecords.Count > 0)
                .ToList<User>();
                //.Add(Restrictions.Where<ClearanceChecklistRole>(role => role != null))
                /*.Add(Restrictions.Where<IList<ClearanceChecklistRoleRecord>>(roles => (roles !=null && roles.Count>0)))
                .List<User>();*/
            return clearanceChecklistApprovingAuthorities;
        }

        /// <summary>
        /// Add approval information to a clearance checklist
        /// </summary>
        /// <param name="approvalId">ID of the approval that is to be updated</param>
        /// <param name="approvedBy">The authority whose approval is to be added</param>
        /// <param name="modifiedApproval">The DTO containing the approval information</param>
        /// <returns></returns>
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

        /// <summary>
        /// Add/modify a comment to an approval for a clearance checklist
        /// </summary>
        /// <param name="approvalId">ID of the approval the comment belongs to</param>
        /// <param name="comment">The text of the comment</param>
        /// <returns></returns>
        public bool SetComment(int approvalId, string comment)
        {
            var approval = GetApprovalById(approvalId);
            if (approval != null)
            {
                var transaction = Session.BeginTransaction();
                approval.Comment = comment;
                Session.Update(approval);
                transaction.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetBottomFields(int clearanceChecklistId, int? registryNumber, decimal? personalIncomeTax, string oKTMO)
        {
            var clearanceChecklist = Session.Get<Dismissal>(clearanceChecklistId);
            if (clearanceChecklist != null)
            {
                var transaction = Session.BeginTransaction();
                clearanceChecklist.RegistryNumber = registryNumber;
                clearanceChecklist.PersonalIncomeTax = personalIncomeTax;
                clearanceChecklist.OKTMO = oKTMO;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get a list of all clearance checklist roles
        /// </summary>
        /// <returns></returns>
        public IList<ClearanceChecklistRole> GetClearanceChecklistRoles()
        {
            return Session.CreateCriteria<ClearanceChecklistRole>().List<ClearanceChecklistRole>();
        }

        /// <summary>
        /// Get all users who own a given role
        /// </summary>
        /// <param name="clearanceChecklistRole"></param>
        /// <returns></returns>
        public IList<User> GetClearanceChecklistRoleAuthorities(ClearanceChecklistRole clearanceChecklistRole)
        {
            // SELECT * FROM User INNER JOIN ClearanceChecklistRoleRecord cclrr ON User.Id = cclrr.UserId WHERE cclrr.RoleId = clearanceChecklistRoleRecord.Id
            IList<User> roleOwners = Session.CreateCriteria<User>()
                .CreateAlias("ClearanceChecklistRoleRecords", "c", JoinType.InnerJoin)
                .Add(Restrictions.Eq("c.Role.Id", clearanceChecklistRole.Id)).List<User>();

            return roleOwners;
        }
    }
}

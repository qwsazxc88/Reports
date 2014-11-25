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

        public IList<ClearanceChecklistDto> GetClearanceChecklistDocuments(
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
                                "Обходной лист",
                                "v.[CreateDate]",
                                "[dbo].[Dismissal]",
                                "'" + DateTime.MinValue.ToShortDateString() + "'",
                                "v.[EndDate]",
                                //"'" + DateTime.MinValue.ToShortDateString() + "'",
                                typeId,
                                "v.[RegistryNumber]",
                                "v.[PersonalIncomeTax]",
                                "v.[OKTMO]"
                );
            string relevanceFilter = @"
                                -- с недостающими согласованиями
								inner join [dbo].[ClearanceChecklistApproval] approval on v.Id = approval.DismissalId
									and approval.ApprovedById is null
								-- с ролями, по которым нет согласования текущим пользователем
								inner join [dbo].[ClearanceChecklistRole] approvalRole on approval.RoleId = approvalRole.Id
									and approvalRole.Id in
									(select RoleId from [dbo].[ClearanceChecklistRoleRecord] where UserId = {0})";
            relevanceFilter = string.Format(relevanceFilter, userId);

            int? superPersonnelId = ConfigurationService.SuperPersonnelId;

            // Фильтрация по релевантности производится для всех кроме суперпользователя и аутсорса
            if (!(superPersonnelId.HasValue && superPersonnelId.Value == userId) && !(role == UserRole.OutsourcingManager))
            {
                sqlQuery += relevanceFilter;
            }

            string whereString = string.Empty;
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetSpecialFiltersWhere(whereString);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            //query.SetResultTransformer(Transformers.
            // return query.SetResultTransformer(Transformers.AliasToBean(typeof(ClearanceChecklistDto))).List<ClearanceChecklistDto>();
            return query.SetResultTransformer(Transformers.AliasToBean<ClearanceChecklistDto>()).List<ClearanceChecklistDto>();
        }

        // Фильтры, специфичные для обходных листов
        private string GetSpecialFiltersWhere(string whereString)
        {
            whereString += @" and v.DeleteDate is null
                              and v.UserDateAccept is not null
                              and v.ManagerDateAccept is not null
                              and v.PersonnelManagerDateAccept is not null ";
            return whereString;
        }

        public override string GetDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EndDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EndDate] < :endDate ";
            }
            return whereString;
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestStatus", NHibernateUtil.String).
                AddScalar("RegistryNumber", NHibernateUtil.Int32).
                AddScalar("PersonalIncomeTax", NHibernateUtil.Decimal).
                AddScalar("OKTMO", NHibernateUtil.String);
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
                .Where<User>(user => user.ClearanceChecklistRoleRecords != null
                    && user.ClearanceChecklistRoleRecords.Where(ccrr => ccrr.Role.DeleteDate == null).Count() > 0)
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
                Session.Update(clearanceChecklist);
                transaction.Commit();
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

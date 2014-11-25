using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistDao : DefaultDao<Sicklist>, ISicklistDao
    {
        public const string StrInvalidManagerLevel = "Неверный уровень руководителя (id {0}) {1} в базе даннных.";

        public SicklistDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }
        
        public IList<SicklistDto> GetSicklistDocuments(
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
            )
        {
            string sqlQuery =
                string.Format(sqlSelectForListSicklist,
                    DeleteRequestText,
                    "dbo.SicklistType",
                    "v.[CreateDate]",
                    "Болезнь (неявка)",
                    "[dbo].[Sicklist]",
                    "v.[BeginDate]",
                    "v.[EndDate]"
                );

            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortedBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);

            return query.SetResultTransformer(Transformers.AliasToBean<SicklistDto>()).List<SicklistDto>();

            //return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();

            //return GetDefaultDocuments(userId, role, departmentId,
            //    positionId, typeId,
            //    statusId, beginDate, endDate,userName,
            //    sqlQuery, sortedBy, sortDescending);

//            string sqlQuery =
//                string.Format(@"select v.Id as Id,
//                         u.Id as UserId,
//                         'Болезнь (неявка) '+ u.Name + case when [DeleteDate] is not null then N' ({0})' else '' end as Name,
//                         v.[CreateDate] as Date    
//            from [dbo].[Sicklist] v
//            inner join [dbo].[Users] u on u.Id = v.UserId",DeleteRequestText);
//            string whereString = GetWhereForUserRole(role, userId);
//            whereString = GetTypeWhere(whereString, typeId);
//            whereString = GetStatusWhere(whereString, statusId);
//            whereString = GetDatesWhere(whereString, beginDate, endDate);
//            whereString = GetPositionWhere(whereString, positionId);
//            whereString = GetDepartmentWhere(whereString, departmentId);
            
//            if (paymentPercentTypeId != 0)
//            {
//                if (whereString.Length > 0)
//                    whereString += @" and ";
//                whereString += @"v.[PaymentPercentId] = :paymentPercentTypeId ";
//            }
//            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString,0, null);
//            IQuery query = CreateQuery(sqlQuery);
//            AddDatesToQuery(query, beginDate, endDate);
//            if (paymentPercentTypeId != 0)
//                query.SetInt32("paymentPercentTypeId", paymentPercentTypeId);
//            return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
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
                AddScalar("UserExperienceIn1C", NHibernateUtil.Boolean).
                AddScalar("IsOriginalReceived", NHibernateUtil.Boolean);
        }

        public bool ResetApprovals(int id)
        {
            var sicklist = Session.Get<Sicklist>(id);
            if (sicklist != null)
            {
                var transaction = Session.BeginTransaction();
                sicklist.ManagerDateAccept = null;
                sicklist.UserDateAccept = null;
                Session.Update(sicklist);
                transaction.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IList<Sicklist> LoadForIdsList(List<int> ids)
        {
            if (ids.Count == 0)
                return new List<Sicklist>();
            ICriteria criteria = Session.CreateCriteria(typeof(Sicklist));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<Sicklist>();
        }
        
        public int GetRequestCountsForUserAndDates(DateTime beginDate, DateTime endDate, int userId, int requestId)
        {
            return GetRequestsCountForType(beginDate, endDate, RequestTypeEnum.Sicklist, userId, UserRole.Employee, requestId);
        }
        
        public virtual string GetWhereForUserRole(UserRole role, int userId, ref string sqlQuery)
        {
            switch (role)
            {
                case UserRole.Employee:
                    sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Format(" u.Id = {0} ", userId);
                case UserRole.Manager:
                    User currentUser = UserDao.Load(userId);
                    if (currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", userId));

                    string sqlQueryPartTemplate =
                        @" select distinct emp.Id from dbo.Users emp
                                            inner join dbo.Users manU on manU.Login = emp.Login+N'R' and manU.RoleId = 4 and manU.IsActive = 1
                                             inner join dbo.Department dManU on manU.DepartmentId = dManU.Id and
                                             ((manU.[level] in ({0})) or ((manU.[level] = {1}) and (manU.IsMainManager = 0)))
                                             inner join dbo.Department dMan on dManU.Path like dMan.Path+N'%'
                                             inner join dbo.Users man on man.DepartmentId = dMan.Id and man.Id = {2}";
                    string sqlQueryPart = string.Empty;
                    string sqlFlag = string.Empty;

                    switch (currentUser.Level)
                    {
                        case 2:
                            sqlQueryPart = string.Format(sqlQueryPartTemplate, "3", "2", currentUser.Id);
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                        and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;
                        case 3:
                        case 4:
                            sqlQueryPartTemplate += @" union 
                                select distinct emp.id from Users emp
                                    inner join dbo.Department dept
	                                    on emp.DepartmentId = dept.id
                                where
	                                (emp.RoleId & 2) > 0
	                                and
	                                emp.departmentid in
		                                (select Department.id from Department where Department.Path like
			                                (select department.path+'%' from department where id =
				                                (select departmentid from users where id={2})
			                                )
		                                )
                                    and
                                    not (select Login from dbo.Users where Id={2}) = emp.Login + N'R'";
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                        and v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            if (currentUser.Level == 3)
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "4,5,6", "3", currentUser.Id);
                            }
                            else
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "5,6", "4", currentUser.Id);
                            }
                            break;
                        case 5:
                        case 6:
                            sqlQueryPartTemplate += @" union 
                             select distinct emp1.Id from dbo.Users emp1
                             inner join dbo.Department dEmp1 on emp1.DepartmentId = dEmp1.Id 
                             and  ((emp1.RoleId & 2) > 0) 
                             inner join dbo.Department dMan on dEmp1.Path like dMan.Path+N'%'
                             inner join dbo.Users man on man.DepartmentId = dMan.Id and man.Id = {2}
                             where not exists (select Id from dbo.Users empMan1 where
                                empMan1.RoleId = 4 and empMan1.Login = emp1.Login+N'R' and empMan1.IsActive = 1)";
                            if (currentUser.Level == 5)
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "6", "5", currentUser.Id);
                            }
                            else
                            {
                                sqlQueryPart = string.Format(sqlQueryPartTemplate, "-1", "6", currentUser.Id);
                            }
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                            and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, userId, currentUser.Level));
                    }

                    sqlQueryPart = string.Format(@"u.Id in ( {0} )", sqlQueryPart);

                    sqlQuery = string.Format(sqlQuery, sqlFlag, string.Empty);
                    // Автороль должна действовать только для уровней ниже третьего
                    sqlQueryPart = string.Format(" ((u.Level>3 or u.Level IS NULL) and {0} ) ", sqlQueryPart);
                    // Ручные привязки человек-человек и человек-подразделение из MissionOrderRoleRecord
                    sqlQueryPart += string.Format(" or u.Id in (select morr.TargetUserId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
                    sqlQueryPart += string.Format(" or u.DepartmentId in (select morr.TargetDepartmentId from [dbo].[MissionOrderRoleRecord] morr where morr.UserId = {0})", userId);
                    sqlQueryPart = string.Format(@"({0})", sqlQueryPart);
                    return sqlQueryPart;
                case UserRole.Director:
                    //User currUser = UserDao.Load(userId);
                    //if(currUser == null)
                    //    throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных",userId));
                    const string sqlFlagD = @"case when (
                                            (v.UserDateAccept is not null 
                                            and  v.ManagerDateAccept is not null 
                                            and  v.[ChiefDateAccept] is null 
                                            and  v.[NeedToAcceptByChief] = 1) 
                                            or
                                            (v.UserDateAccept is not null 
                                             and  v.ManagerDateAccept is null 
                                             and v.[NeedToAcceptByChiefAsManager] = 1)
                                            )
                                            then 1 else 0 end as Flag";
                    sqlQuery = string.Format(sqlQuery, sqlFlagD, string.Empty);
                    return @" ((v.[NeedToAcceptByChief] = 1) or (v.[NeedToAcceptByChiefAsManager] = 1) or (ao.NeedToAcceptByChief = 1))  ";
                case UserRole.Accountant:
                case UserRole.OutsourcingManager:
                case UserRole.Secretary:
                case UserRole.Findep:
                    sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using Reports.Core.Utils;


namespace Reports.Core.Dao.Impl
{
    public class HelpQuestionRequestDao : DefaultDao<HelpQuestionRequest>, IHelpQuestionRequestDao
    {
        public const string StrInvalidManagerDepartment = "Не указано структурное подразделение для руководителя (id {0}) в базе даннных.";
        public const string StrNoManagerDepartments = "Не найдено структурных подразделений для руководителя (id {0}) в базе даннных.";

        protected IManualRoleRecordDao manualRoleRecordDao;
        public IManualRoleRecordDao ManualRoleRecordDao
        {
            get { return Validate.Dependency(manualRoleRecordDao); }
            set { manualRoleRecordDao = value; }
        }

        public const string sqlSelectForRn = 
                                //@";with res as
                                @"{0} )
                                select {1} as Number,* from res order by Number ";
        protected const string sqlSelectForHqList =
                                @"
                                ;with hesc as
                                (
                                    select hqr.id, 
                                    count(hqhe.Id) as SendCount
                                    from [dbo].[HelpQuestionRequest] hqr
                                    left join [dbo].[HelpQuestionHistoryEntity] hqhe on hqhe.HelpQuestionRequestId = hqr.Id
                                    and hqhe.Type = 1
                                    group by hqr.id
                                ),
                                hemd as
                                (
                                    select
                                    hqhe.HelpQuestionRequestId,
                                    max([CreateDate]) as MaxDate
                                    from [dbo].[HelpQuestionHistoryEntity] hqhe 
                                    where hqhe.Type = 6
                                    group by hqhe.HelpQuestionRequestId
                                ),
                                helr as
                                (
	                                select 
	                                hemd.HelpQuestionRequestId,
	                                hqhe.RecipientRoleId as LastRedirectId
	                                from hemd
	                                left join [dbo].[HelpQuestionHistoryEntity] hqhe
	                                on hemd.HelpQuestionRequestId = hqhe.HelpQuestionRequestId and hemd.MaxDate = hqhe.CreateDate
                                    where not exists (select Id from [dbo].[HelpQuestionHistoryEntity] hqhe1 where hqhe1.CreateDate > hemd.MaxDate
	                                    and hqhe1.Type = 1   )
                                ),
                                res as
                                (
                                select distinct v.Id as Id,
                                u.Id as UserId,
                                u.Name as UserName,
                                up.Name as Position,
                                -- case when v.CreatorId != v.UserId then crUser.Name else N'' end as ManagerName,
                                dep.Name as DepName,
                                v.Number as RequestNumber,
                                v.CreateDate as CreateDate,
                                v.EndWorkDate as EndWorkDate,
                                t.Name as QuestionType,
                                s.Name as QuestionSubtype,
                                hesc.SendCount as QuestionsCount,
                                r.Name as RedirectRole,
                                case when v.CreatorRoleId = 4 and v.UserId = v.CreatorId then 1 else 0 end as IsManagerQuestion,
                                case when v.[SendDate] is null then 1
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then 2 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then 3 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null then 4
                                     when v.[ConfirmWorkDate] is not null then 5 
                                    else 0
                                end as StatusNumber,
                                case when v.[SendDate] is null then N'Черновик'
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then N'Вопрос задан' 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then N'Вопрос принят в работу' 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null then N'Ответ на вопрос получен' 
                                     when v.[ConfirmWorkDate] is not null then N'Ответ на вопрос подтвержден' 
                                    else N''
                                end as Status,
                                J.Name as Dep3Name
                                from [dbo].[HelpQuestionRequest] v
                                inner join [dbo].[HelpQuestionType] t on v.TypeId = t.Id
                                inner join [dbo].[HelpQuestionSubtype] s on v.[SubtypeId] = s.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                -- inner join [dbo].[Users] crUser on crUser.Id = v.CreatorId
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                left join hesc on v.Id = hesc.Id 
                                left join helr on v.Id = helr.HelpQuestionRequestId and v.[SendDate] is not null
                                left join [dbo].[Role] r on r.Id = helr.LastRedirectId
                                inner join dbo.Department dep on u.DepartmentId = dep.Id
                                LEFT JOIN [dbo].[Department] as H ON H.Code = dep.ParentId
                                LEFT JOIN [dbo].[Department] as I ON I.Code = H.ParentId
                                LEFT JOIN [dbo].[Department] as J ON J.Code = I.ParentId
                                LEFT JOIN [dbo].[Department] as K ON K.Code = J.ParentId
                                {0}";
        
        public HelpQuestionRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Position", NHibernateUtil.String).
                AddScalar("DepName", NHibernateUtil.String).
                AddScalar("RequestNumber", NHibernateUtil.Int32).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("EndWorkDate", NHibernateUtil.DateTime).
                AddScalar("QuestionType", NHibernateUtil.String).
                AddScalar("QuestionSubtype", NHibernateUtil.String).
                AddScalar("QuestionsCount", NHibernateUtil.Int32).
                AddScalar("IsManagerQuestion", NHibernateUtil.Boolean).
                AddScalar("RedirectRole", NHibernateUtil.String).
                AddScalar("StatusNumber", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("Dep3Name", NHibernateUtil.String);
        }
        public List<HelpServiceQuestionDto> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                string number,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForHqList;
            //для кадровиков показываем вопросы по своим дирекциям
            if (role == UserRole.PersonnelManager)
            {
                sqlQuery = string.Format(sqlQuery, string.Empty);
                sqlQuery += "INNER JOIN [dbo].[UserToPersonnel] as L ON L.[UserID] = v.[UserID] and L.[PersonnelId] = " + userId.ToString() + " {0}";
            }


            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetNumberWhere(whereString, number);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
            List<HelpServiceQuestionDto> documentList = query
                .SetResultTransformer(Transformers.AliasToBean(typeof(HelpServiceQuestionDto)))
                .List<HelpServiceQuestionDto>().ToList();
            return documentList;
        }
        public override string GetSqlQueryOrdered(string sqlQuery, string whereString,
                    int sortedBy,
                    bool? sortDescending)
        {
            string orderBy = string.Empty;
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
            {
                orderBy = " ORDER BY CreateDate DESC,UserName";
                return string.Format(sqlSelectForRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY CreateDate DESC,UserName";
                    return string.Format(sqlSelectForRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by UserName";
                    break;
                case 2:
                    orderBy = @" order by Position";
                    break;
                /*case 3:
                    orderBy += @" order by ManagerName";
                    break;*/
                case 4:
                    orderBy = @" order by DepName";
                    break;
                case 5:
                    orderBy = @" order by RequestNumber";
                    break;
                case 6:
                    orderBy = @" order by CreateDate";
                    break;
                case 7:
                    orderBy = @" order by EndWorkDate";
                    break;
                case 8:
                    orderBy = @" order by QuestionType";
                    break;
                case 9:
                    orderBy = @" order by QuestionSubtype";
                    break;
                case 10:
                    orderBy = @" order by QuestionsCount";
                    break;
                case 11:
                    orderBy = @" order by IsManagerQuestion";
                    break;
                case 12:
                    orderBy = @" order by RedirectRole";
                    break;
                case 13:
                    orderBy = @" order by Status";
                    break;
                case 14:
                    orderBy = @" order by Dep3Name";
                    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }
        public override string GetWhereForUserRole(UserRole role, int userId, ref string sqlQuery)
        {
            switch (role)
            {
                case UserRole.Employee:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return string.Format(" u.Id = {0} ", userId);
                case UserRole.Manager:
                    User currentUser = UserDao.Load(userId);
                    string sqlQueryPart = string.Empty;
                    switch (currentUser.Level)
                    {
                        case 2:
                        case 3:
                            //                            sqlQueryPart = string.Format(sqlQueryPartTemplate, "3", "2", currentUser.Id);
                            //                            sqlFlag = @"case when v.UserDateAccept is not null 
                            //                                        and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            IList<Department> depList = ManualRoleRecordDao.LoadDepartmentsForUserId(currentUser.Id);
                            if (depList == null || depList.Count() == 0)
                                throw new ArgumentException(string.Format(StrNoManagerDepartments, currentUser.Id));
                            sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                            return string.Format(@"(u.Id = {3}) or (depM.Id in {0} and ((u.RoleId & 2 > 0) or (u.Level > {1}) or ( u.Level = {1} and (u.[IsMainManager] < {2}))))",
                                CoreUtils.CreateIn("(", depList), currentUser.Level, currentUser.IsMainManager ? 1 : 0, userId);
                        case 4:
                        case 5:
                        case 6:
                            if (currentUser.Department == null)
                                throw new ArgumentException(string.Format(StrInvalidManagerDepartment, currentUser.Id));
                            sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                            return string.Format(@"(u.Id = {3}) or (depM.Id = {0} and ((u.RoleId & 2 > 0) or (u.Level > {1}) or ( u.Level = {1} and (u.[IsMainManager] < {2}))))", 
                                currentUser.Department.Id,currentUser.Level,currentUser.IsMainManager?1:0,userId);
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, currentUser.Id,
                                currentUser.Level));
                    }
                //return sqlQueryPart;
                case UserRole.OutsourcingManager:
                case UserRole.ConsultantOutsourcing:
                case UserRole.ConsultantPersonnel:
                case UserRole.ConsultantAccountant:
                case UserRole.PersonnelManager:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return " v.[TypeId] = 1 ";
                case UserRole.ConsultantOutsorsingManager:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return " v.[TypeId] = 2 ";
                case UserRole.Admin:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
        public override string GetDatesWhere(string whereString, DateTime? beginDate,
            DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[CreateDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[CreateDate] < :endDate ";
            }
            return whereString;
        }
        public override string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1:
                        statusWhere = @"v.[SendDate] is null ";
                        break;
                    case 2:
                        statusWhere = @"v.[SendDate] is not null and v.[BeginWorkDate] is null";
                        break;
                    case 3:
                        statusWhere = @"v.[BeginWorkDate] is not null and v.[EndWorkDate] is null";
                        break;
                    case 4:
                        statusWhere = @"v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null";
                        break;
                    case 5:
                        statusWhere = @"v.[ConfirmWorkDate] is not null";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                //return whereString;
            }
            return whereString;
        }
    }
}
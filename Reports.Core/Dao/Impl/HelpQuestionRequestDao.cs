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
                                    --where not exists (select Id from [dbo].[HelpQuestionHistoryEntity] hqhe1 where hqhe1.CreateDate > hemd.MaxDate and hqhe1.Type = 1 )
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
                                v.Base as Base,
                                t.Name as QuestionType,
                                s.Name as QuestionSubtype,
                                hesc.SendCount as QuestionsCount,
                                r.id as RedirectRoleID, r.Name as RedirectRole,
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
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null then N'Ответ на вопрос предоставлен' 
                                     when v.[ConfirmWorkDate] is not null then N'Ответ на вопрос подтвержден' 
                                    else N''
                                end as Status,
                                dep3.Name as Dep3Name
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
                                inner join dbo.Users currentUser on currentUser.Id = :userId
                                LEFT JOIN dbo.Department dep3 ON dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
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
                AddScalar("Dep3Name", NHibernateUtil.String).
                AddScalar("Base", NHibernateUtil.Boolean);
                
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
            if (role == UserRole.PersonnelManager)// DEPRECATED Был консультант ОК, стал кадровик
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
            query.SetInt32("userId", userId);
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
                case 15:
                    orderBy = @" order by Base";
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
                case UserRole.DismissedEmployee:
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
                            //IList<Department> depList = ManualRoleRecordDao.LoadDepartmentsForUserId(currentUser.Id);
                            //if (depList == null || depList.Count() == 0)
                            //    throw new ArgumentException(string.Format(StrNoManagerDepartments, currentUser.Id));
                            //sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            //sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                            //return string.Format(@"(u.Id = {3}) or (depM.Id in {0} and ((u.RoleId & 2 > 0) or (u.Level > {1}) or ( u.Level = {1} and (u.[IsMainManager] < {2}))))",
                            //    CoreUtils.CreateIn("(", depList), currentUser.Level, currentUser.IsMainManager ? 1 : 0, userId);
                            sqlQueryPart += " -1";
                            sqlQuery = string.Format(sqlQuery, "");
                            break;
                        case 4:
                        case 5:
                        case 6:
                            //if (currentUser.Department == null)
                            //    throw new ArgumentException(string.Format(StrInvalidManagerDepartment, currentUser.Id));
                            //sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            //sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                            //return string.Format(@"(u.Id = {3}) or (depM.Id = {0} and ((u.RoleId & 2 > 0) or (u.Level > {1}) or ( u.Level = {1} and (u.[IsMainManager] < {2}))))", 
                            //    currentUser.Department.Id,currentUser.Level,currentUser.IsMainManager?1:0,userId);
                            sqlQuery = string.Format(sqlQuery, "");
                            // Выборка замов и руководителей нижележащих уровней по ветке для применения автоматических прав уровней 4-6
                            sqlQueryPart += @" select distinct managerEmployeeAccount.Id from dbo.Users managerEmployeeAccount
                             inner join dbo.Users managerManagerAccount
                               on managerManagerAccount.Login = managerEmployeeAccount.Login+N'R'
                                 and (managerManagerAccount.RoleId & 4) > 0
                                 and managerManagerAccount.IsActive = 1
                                 and
                                 (
                                   (
                                     -- Руководители нижележащих уровней
                                     managerManagerAccount.Level > currentUser.Level
                                   )
                                   or
                                   (
                                     -- Замы для уровней 4-6
                                     managerManagerAccount.Level = currentUser.Level and managerManagerAccount.IsMainManager = 0
                                   )
                                 )
                             inner join dbo.Department managerManagerAccountDept
                               on managerManagerAccount.DepartmentId = managerManagerAccountDept.Id
                                 -- Исключить состоящих в ветке руководства
                                 and managerManagerAccountDept.Path not like N'9900424.9900426.9900427.%'
                               
                             -- по ветке
                             inner join dbo.Department higherDept
                               on managerManagerAccountDept.Path like higherDept.Path+N'%'
                             where currentUser.DepartmentId = higherDept.Id
                               -- Исключение своей учетной записи 7 уровня
                               and not currentUser.Login = managerEmployeeAccount.Login + N'R'";

                            // Выборка рядовых пользователей по ветке для применения автоматических прав
                            sqlQueryPart += @"
                                union
                                select distinct employee.Id from Users employee
                                    left join [dbo].[Users] employeeManagerAccount
                                    on (employeeManagerAccount.RoleId & 4) > 0
                                        and employeeManagerAccount.Login = u.Login+N'R'
                                        and employeeManagerAccount.IsActive = 1
                                    inner join dbo.Department employeeDept
                                      on employee.DepartmentId = employeeDept.Id
                                        -- Исключить состоящих в ветке руководства
                                        and employeeDept.Path not like N'9900424.9900426.9900427.%'
                                    inner join dbo.Department higherDept
                                      on employeeDept.Path like higherDept.Path+N'%'
                                where (employee.RoleId & 2) > 0
                                    and (employeeManagerAccount.Id is null or employeeManagerAccount.IsActive = 0)
                                    and currentUser.DepartmentId = higherDept.Id
                                    and not currentUser.Login = employee.Login + N'R'";


                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, currentUser.Id,
                                currentUser.Level));
                    }
                    sqlQueryPart = string.Format(@"u.Id=currentUser.Id or  u.Id in ( {0} )", sqlQueryPart);

                    // Автороль должна действовать только для уровней ниже третьего
                    sqlQueryPart = string.Format(" (u.Level>3 or u.Level IS NULL) and ( {0} ) ", sqlQueryPart);
                    // Ручные привязки человек-человек и человек-подразделение из ManualRoleRecord
                    sqlQueryPart += string.Format(@"
                                or u.Id in (select mrr.TargetUserId from [dbo].[ManualRoleRecord] mrr where mrr.UserId = {0} and mrr.RoleId = 1)", userId);
                    sqlQueryPart += string.Format(@"
                                or 
                                (
                                    (u.RoleId & 2) > 0
                                    and
                                    u.DepartmentId in
                                    (
                                        select distinct branchDept.Id from [dbo].[ManualRoleRecord] mrr
                                            inner join Department targetDept
                                                on targetDept.Id = mrr.TargetDepartmentId
                                            inner join [dbo].[Department] branchDept
                                                on branchDept.Path like targetDept.Path + '%'
                                            inner join Users
                                                on mrr.UserId = {0}
                                            inner join Role
                                                on mrr.RoleId = 1
                                    )
                                )
                                ", userId);
                    sqlQueryPart = string.Format(@"({0})", sqlQueryPart);
                    //sqlQuery = string.Format(sqlQuery, sqlFlag, string.Empty);
                    return sqlQueryPart;
                /*case UserRole.ConsultantOutsorsingManager:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    //показываем вопросы руководителей, которые перенаправлены на эту роль
                    return " (case when v.CreatorRoleId = 4 and v.UserId = v.CreatorId then (case when v.ConsultantRoleId = " + (int)UserRole.ConsultantOutsorsingManager + " then 0 else 1 end) else 0 end) = 0 and r.id = " + Convert.ToString((int)UserRole.ConsultantOutsorsingManager) + " "; DEPRECATED УБИРАЕМ РОЛЬ Консультант ОК*/
                //return sqlQueryPart;
                case UserRole.ConsultantPersonnel:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @" r.[Id] = " + (int)UserRole.ConsultantPersonnel + " or v.CreatorId=:userId ";
                case UserRole.Accountant:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @" r.[Id] = " + (int)UserRole.Accountant + " ";
                case UserRole.PersonnelManager:
                    //sqlQuery = string.Format(sqlQuery, string.Empty);
                    //return " v.[TypeId] = 2 ";
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @" r.[Id] = " + (int)UserRole.PersonnelManager + " or v.CreatorId=:userId ";
                case UserRole.OutsourcingManager:
                case UserRole.Estimator:
                case UserRole.ConsultantOutsourcing:
                
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
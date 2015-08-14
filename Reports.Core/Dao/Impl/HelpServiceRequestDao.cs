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
    public class HelpServiceRequestDao : DefaultDao<HelpServiceRequest>, IHelpServiceRequestDao
    {        
        public const string StrInvalidManagerDepartment = "Не указано структурное подразделение для руководителя (id {0}) в базе даннных.";
        public const string StrNoManagerDepartments = "Не найдено структурных подразделений для руководителя (id {0}) в базе даннных.";
                
        protected IManualRoleRecordDao missionOrderRoleRecordDao;
        public IManualRoleRecordDao ManualRoleRecordDao
        {
            get { return Validate.Dependency(missionOrderRoleRecordDao); }
            set { missionOrderRoleRecordDao = value; }
        }

        public HelpServiceRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public const string sqlSelectForRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";
        protected const string sqlSelectForHsList =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                u.Name as UserName,
                                up.Name as Position,
                                case when v.CreatorId != v.UserId then crUser.Name else N'' end as ManagerName,
                                case when v.DepartmentId is not null then fDep.Name
                                    else dep.Name 
                                end as Dep7Name,
                                v.Number as RequestNumber,
                                v.FiredUserName as FiredUserName,
                                v.FiredUserSurname as FiredUserSurname,
                                v.FiredUserPatronymic as FiredUserPatronymic,
                                v.UserBirthDate as UserBirthDate,
                                v.IsOriginalReceived as IsOriginalReceived,
                                v.IsForGEMoney as IsForGEMoney,
                                att.DocumentsCount as DocumentsCount,
                                NT.Name as NoteName,
                                v.CreateDate as CreateDate,
                                v.EditDate as EditDate,
                                v.EndWorkDate as EndWorkDate,
                                v.ConfirmWorkDate as ConfirmWorkDate,
                                t.Name as RequestType,
                                m.Name as RequestTransferType,
                                case when v.[SendDate] is null then 1
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then 2 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then 3 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null and v.[NotEndWorkDate] is null then 4
                                     when v.[ConfirmWorkDate] is not null then 5 
                                     when v.[NotEndWorkDate] is not null then 6
                                    else 0
                                end as StatusNumber,
                                case when v.[SendDate] is null then N'Черновик сотрудника'
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then N'Услуга запрошена' 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then N'Услуга формируется' 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null and v.[NotEndWorkDate] is null then N'Услуга сформирована' 
                                     when v.[ConfirmWorkDate] is not null then N'Услуга оказана' 
                                     when v.[NotEndWorkDate] is not null then N'Услуга не может быть сформирована'
                                    else N''
                                end as Status,
                                v.Address as address,
                                case when v.DepartmnetId is not null then fDep3.Name
                                    else dep3.Name 
                                end as Dep3Name
                                L.Name as ProdTimeName,
                                O.Name as PeriodName
                                from dbo.HelpServiceRequest v
                                left join [dbo].[RequestAttachment] att on v.Id=att.RequestId and att.RequestType=11
                                inner join [dbo].[HelpServiceType] t on v.TypeId = t.Id
                                inner join [dbo].[HelpServiceTransferMethod] m on v.[TransferMethodId] = m.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                inner join [dbo].[Users] crUser on crUser.Id = v.CreatorId
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                LEFT join dbo.Department dep on u.DepartmentId = dep.Id
                                inner join dbo.Users currentUser on currentUser.Id = :userId
                                left join [dbo].[Department] fDep ON v.DepartmentId=fDep.id
                                left join [dbo].[Department] fDep3 ON fDep.[Path] like fDep3.[Path]+N'%' and fDep3.ItemLevel = 3
                                LEFT JOIN [dbo].[NoteType] as NT ON v.NoteId=NT.Id
                                LEFT JOIN dbo.Department dep3 ON dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                                LEFT JOIN [dbo].[HelpServiceProductionTime] as L ON L.Id = v.ProductionTimeId
                                LEFT JOIN [dbo].[HelpServicePeriod] as O ON O.Id = v.PeriodId
                                {0}";
        
        public override IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Position", NHibernateUtil.String).
                AddScalar("ManagerName", NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                AddScalar("RequestNumber", NHibernateUtil.Int32).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("EndWorkDate", NHibernateUtil.DateTime).
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("ConfirmWorkDate", NHibernateUtil.DateTime).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestTransferType", NHibernateUtil.String).
                AddScalar("StatusNumber", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("Address", NHibernateUtil.String).
                AddScalar("Dep3Name", NHibernateUtil.String).
                AddScalar("ProdTimeName", NHibernateUtil.String).
                AddScalar("FiredUserName",NHibernateUtil.String).
                AddScalar("FiredUserSurname",NHibernateUtil.String).
                AddScalar("FiredUserPatronymic",NHibernateUtil.String).
                AddScalar("NoteName",NHibernateUtil.String).
                AddScalar("UserBirthDate",NHibernateUtil.Date).
                AddScalar("IsOriginalReceived",NHibernateUtil.Boolean).
                AddScalar("IsForGEMoney",NHibernateUtil.Boolean).
                AddScalar("DocumentsCount",NHibernateUtil.Int32).
                AddScalar("PeriodName", NHibernateUtil.String)
                ;  
        }
        public List<HelpServiceRequestDto> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                string number,
                int sortBy,
                bool? sortDescending,
                string Address,
                int typeId=0
            )
        {
            string sqlQuery = sqlSelectForHsList;

            //для кадровиков показываем вопросы по своим дирекциям
            if (role == UserRole.PersonnelManager && userId != 10)
            {
                sqlQuery = string.Format(sqlQuery, string.Empty);
                sqlQuery += "LEFT JOIN [dbo].[UserToPersonnel] as N ON N.[UserID] = v.[UserID] and (N.[PersonnelId] = " + userId.ToString() + " or v.[UserID]=" + userId.ToString() + "){0}";
            }

            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetNumberWhere(whereString, number);
            whereString = GetTypeWhere(whereString, typeId);

            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
            query.SetInt32("userId", userId);
            List<HelpServiceRequestDto> documentList = query
                .SetResultTransformer(Transformers.AliasToBean(typeof(HelpServiceRequestDto)))
                .List<HelpServiceRequestDto>().ToList();
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
                case 3:
                    orderBy += @" order by ManagerName";
                    break;
                case 4:
                    orderBy = @" order by Dep7Name";
                    break;
                case 5:
                    orderBy = @" order by RequestNumber";
                    break;
                case 6:
                    orderBy = @" order by CreateDate";
                    break;
                case 7:
                    orderBy = @" order by ConfirmWorkDate";
                    break;
                case 8:
                    orderBy = @" order by RequestType";
                    break;
                case 9:
                    orderBy = @" order by RequestTransferType";
                    break;
                case 10:
                    orderBy = @" order by Status";
                    break;
                case 11:
                    orderBy = @" order by Dep3Name";
                    break;
                case 12:
                    orderBy = @" order by ProdTimeName";
                    break;
                case 13:
                    orderBy = @" order by FiredUserSurname";
                    break;
                case 14:
                    orderBy = @" order by NoteName";
                    break;
                case 18:
                    orderBy = @" order by PeriodName";
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
                            //IList<Department> depList =  ManualRoleRecordDao.LoadDepartmentsForUserId(currentUser.Id);
                            //if(depList == null || depList.Count() == 0)
                            //  throw new ArgumentException(string.Format(StrNoManagerDepartments, currentUser.Id));
                            //sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            sqlQueryPart += " -1";
                            sqlQuery = string.Format(sqlQuery, "");
                            //return string.Format(@" depM.Id in {0}", CoreUtils.CreateIn("(",depList));
                            break;
                        case 4:
                        case 5:
                        //case 6:
                        //    if(currentUser.Department == null)
                        //        throw new ArgumentException(string.Format(StrInvalidManagerDepartment,currentUser.Id));
                        //    sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                        //    sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                        //    return string.Format(@" depM.Id = {0}",currentUser.Department.Id);
                        case 6:
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
                                where ((employee.RoleId & 2) > 0 or employee.RoleId=2097152)
                                    and (employeeManagerAccount.Id is null or employeeManagerAccount.IsActive = 0)
                                    and currentUser.DepartmentId = higherDept.Id
                                    and not currentUser.Login = employee.Login + N'R'";


                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, currentUser.Id,
                                currentUser.Level));
                    }
                    sqlQueryPart = string.Format(@"u.Id in ( {0} )", sqlQueryPart);

                    // Автороль должна действовать только для уровней ниже третьего
                    sqlQueryPart = string.Format(" ((u.Level>3 or u.Level IS NULL) and {0} ) ", sqlQueryPart);
                    // Нужно показывать свои заявки
                    sqlQueryPart += string.Format(@"
                                or u.Id={0}", userId);
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
                //return sqlQueryPart;
                case UserRole.Estimator:
                        /*sqlQuery = string.Format(sqlQuery, string.Empty);
                        return @"  v.[TypeId] in (2, 4, 5, 7, 8, 10, 11, 12, 16, 17, 20, 21, 26, 27) ";*/
                    
                case UserRole.PersonnelManager://кадровики ОК
                       /* if (userId == 10)
                        {
                            sqlQuery = string.Format(sqlQuery, string.Empty);
                            return @"  v.[TypeId] in (2, 4, 5, 7, 8, 10, 11, 12, 16, 17, 20, 21, 26, 27) ";
                        }
                    //4, 2, 5, 7, 10, 11, 21, 26, 27 - эти услуги только для просмотра, не могут принять в работу и посмотреть прикрепленный расчетчиками скан
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @"  v.[TypeId] in (1, 3, 6, 8, 9, 12, 13, 14, 15, 16, 18, 19, 20, 22, 23, 24, 25, 28, 4, 2, 5, 7, 10, 11, 21, 26, 27) ";*/
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return userId != 10 ? @" v.UserId=:userId or N.PersonnelId=:userId " : String.Empty;
                case UserRole.OutsourcingManager:
                case UserRole.ConsultantPersonnel:
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
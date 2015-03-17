using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionReportDao : DefaultDao<MissionReport>, IMissionReportDao
    {
        protected const string sqlSelectForMissionReportRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";

        protected const string sqlSelectForMrList =
            @"select v.Id as Id,
                                u.Id as UserId,
                                u.Name as UserName,
                                up.Name as Position,
                                dep3.Name as Dep3Name,
                                dep.Name as Dep7Name,
                                v.EditDate as EditDate,
                                v.Number as ReportNumber,
                                o.Number as OrderNumber,
                                case when dis.UserDateAccept is not null AND dis.DeleteDate is null Then 'True' else 'False' end AS IsDismissal,
                                -- t.Name as MissionType,  
                                -- case when v.Kind = 1 then  N' Внутренняя'
                                --     when v.Kind = 2 then  N' Внешняя'
                                --     else N'' end
                                -- as MissionKind,  
                                -- [dbo].[fnGetMissionOrderTargetsCities](v.Id) as Target,
                                u.Grade as Grade,
                                v.AllSum as GradeSum,
                                v.UserAllSum as UserSum,
                                v.UserSumReceived as UserSumReceived,
                                v.[AccountantAllSum] as AccountantSum,
                                v.UserAllSum - v.AllSum as GradeIncrease,
                                case when v.[AccountantDateAccept] is not null then
                                     isnull(AccountantAllSum,0) - isnull(PurchaseBookAllSum,0) 
                                    - isnull([UserSumReceived],0)
                                     else null end as Saldo,
                                case when v.DeleteDate is not null then N'Отклонен'
                                     when v.SendTo1C is not null then N'Выгружен в 1с' 
                                     when v.[AccountantDateAccept] is not null 
                                          -- and v.ManagerDateAccept is not null 
                                          -- and v.UserDateAccept is not null 
                                          then N'Согласован'
                                    -- when  NeedToAcceptByChief = 0
                                    --      and v.ManagerDateAccept is not null 
                                    --      and v.UserDateAccept is not null 
                                    --      then N'Согласован'    
                                    when  v.[AccountantDateAccept] is null 
                                          -- and NeedToAcceptByChief = 1
                                          and v.ManagerDateAccept is not null 
                                          and v.UserDateAccept is not null 
                                          then N'Отправлен бухгалтеру'    
                                    when  v.ManagerDateAccept is null 
                                          and v.UserDateAccept is not null 
                                          then N'Отправлен руководителю'    
                                    when  v.UserDateAccept is null 
                                          then N'Черновик сотрудника'    
                                    else N''
                                end as State,
                                uBuh.Name as AccountantName,
                                case when [IsDocumentsSaveToArchive] = 1 then N'Да' else N'Нет' end as IsDocumentsSaveToArchive,
                                ArchiveDate,
                                -- case when [Archivist] is not null then N'Да' else N'Нет' end as IsDocumentsSendToArchivist,
                                ArchiveNumber
                                from dbo.MissionReport v
                                inner join[dbo].[MissionOrder] o on o.Id = v.[MissionOrderId]
                                -- left join dbo.MissionType t on v.TypeId = t.Id
                                inner join dbo.Users currentUser
                                    on currentUser.Id = :userId
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                left join Dismissal dis ON dis.UserId=v.UserId
                                left join [dbo].[Users] uManagerAccount
                                    on (uManagerAccount.RoleId & 4) > 0
                                        and u.Email = uManagerAccount.Email
                                        and uManagerAccount.Login like u.Login+N'R'
                                        and uManagerAccount.IsActive = 1
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                left join [dbo].[Users] uBuh on uBuh.Id = v.AcceptAccountant
                                inner join dbo.Department dep on u.DepartmentId = dep.Id
                                LEFT JOIN dbo.Department dep3 ON dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3";
                                //{0}";

        public MissionReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual bool IsReportForOrderExists(int orderId)
        {
            return (int)Session.CreateCriteria(typeof(MissionReport))
                              .Add(Restrictions.Eq("MissionOrder.Id", orderId))
                              .SetProjection(Projections.RowCount())
                              .UniqueResult() > 0;
        }
        public virtual MissionReport GetReportForOrder(int orderId)
        {
            return (MissionReport)Session.CreateCriteria(typeof(MissionReport))
                              .Add(Restrictions.Eq("MissionOrder.Id", orderId))
                              .UniqueResult();
        }

        public IList<MissionReportDto> GetDocuments(int userId,
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
            string sqlQuery = sqlSelectForMrList;
            string whereString = GetWhereForUserRole(role, userId/*, ref sqlQuery*/);
            //whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetNumberWhere(whereString, number);
            //

            //whereString += String.Format(" or u.Id in (select mrr.TargetUserId from [dbo].[ManualRoleRecord] mrr where mrr.UserId = {0})", userId);
            //whereString += String.Format(" or u.DepartmentId in (select mrr.TargetDepartmentId from [dbo].[ManualRoleRecord] mrr where mrr.UserId = {0})", userId);
           
            //
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
            query.SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(MissionReportDto))).List<MissionReportDto>();
        }

        public override string GetWhereForUserRole(UserRole role, int userId/*, ref string sqlQuery*/)
        {
            switch (role)
            {
                #region Employees
                case UserRole.Employee:
                    return string.Format(" u.Id = {0} ", userId);
                #endregion

                #region Managers
                case UserRole.Manager:
                    User currentUser = UserDao.Load(userId);
                    if (currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", userId));

                    string sqlQueryPart = string.Empty;

                    switch (currentUser.Level)
                    {
                        case 2:
                        case 3:

                            sqlQueryPart += " -1";
                            break;

                        case 4:                            
                        case 5:
                        case 6:

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
                                where ((employee.RoleId & 2) > 0 or (employee.RoleId & 2097152) > 0)
                                    and (employeeManagerAccount.Id is null or employeeManagerAccount.IsActive = 0)
                                    and currentUser.DepartmentId = higherDept.Id
                                    and not currentUser.Login = employee.Login + N'R'";
                            break;

                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, userId, currentUser.Level));
                    }

                    sqlQueryPart = string.Format(@"u.Id in ( {0} )", sqlQueryPart);

                    // Автороль должна действовать только для уровней ниже третьего
                    sqlQueryPart = string.Format(" ((u.Level>3 or u.Level IS NULL) and {0} ) ", sqlQueryPart);
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
                    return sqlQueryPart;
                #endregion

                case UserRole.Accountant:
                    return string.Empty;
                case UserRole.OutsourcingManager:
                //case UserRole.Secretary:
                case UserRole.Findep:
                case UserRole.Archivist:
                    //sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }

        public override string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    //case 1:
                    //    statusWhere =
                    //        @"UserDateAccept is null and ManagerDateAccept is null and PersonnelManagerDateAccept is null and SendTo1C is null";
                    //    break;
                    case 1:
                        statusWhere = @"v.UserDateAccept is not null";
                        break;
                    case 2:
                        statusWhere = @"v.UserDateAccept is null";
                        break;
                    case 3:
                        statusWhere = @"v.ManagerDateAccept is not null";
                        break;
                    case 4:
                        statusWhere = @"v.ManagerDateAccept is null";
                        break;
                    case 5:
                        statusWhere = @"v.[AccountantDateAccept] is not null";
                        break;
                    case 6:
                        statusWhere = @"v.[AccountantDateAccept] is null";
                        break;
//                    case 5:
//                        statusWhere = @"(ChiefDateAccept is not null and NeedToAcceptByChief = 1) or
//                                        (ManagerDateAccept is not null and NeedToAcceptByChiefAsManager = 1)";
//                        break;
//                    case 6:
//                        statusWhere = @"(ChiefDateAccept is null  and NeedToAcceptByChief = 1) or
//                                        (ManagerDateAccept is null and NeedToAcceptByChiefAsManager = 1)";
//                        break;
                    case 7:
                        statusWhere = @"v.UserDateAccept is not null and v.ManagerDateAccept is null";
                        break;
                    case 8:
                        statusWhere = @"v.UserDateAccept is not null and v.ManagerDateAccept is not null and v.[AccountantDateAccept] is null";
                        break;
                   case 9:
                        statusWhere = @"v.SendTo1C is not null";
                        break;
                    //case 8:
                    //    statusWhere =
                    //        @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
                    //    break;
                    case 10:
                        statusWhere = @"v.[DeleteDate] is not null";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (statusId != 10)
                    statusWhere += " and v.DeleteDate is null ";
                //if (statusId != 9 && statusId != 10)
                //    statusWhere += " and SendTo1C is null ";
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
            }
            return whereString;
        }

        public override string GetDatesWhere(string whereString, DateTime? beginDate,
           DateTime? endDate)
        {
            if (beginDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EditDate] >= :beginDate ";
            }
            if (endDate.HasValue)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @"v.[EditDate] < :endDate ";
            }
            return whereString;
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
                orderBy = " ORDER BY UserName,EditDate DESC";
                return string.Format(sqlSelectForMissionReportRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY UserName,EditDate DESC";
                    return string.Format(sqlSelectForMissionReportRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by UserName";
                    break;
                case 2:
                    orderBy += @" order by Dep7Name";
                    break;
                case 3:
                    orderBy = @" order by EditDate";
                    break;
                case 4:
                    orderBy = @" order by ReportNumber";
                    break;
                case 5:
                    orderBy = @" order by OrderNumber";
                    break;
                //case 6:
                //    orderBy = @" order by Target";
                //    break;
                case 6:
                    orderBy = @" order by Grade";
                    break;
                case 7:
                    orderBy = @" order by GradeSum";
                    break;
                case 8:
                    orderBy = @" order by UserSum";
                    break;
                case 9:
                    orderBy = @" order by AccountantSum";
                    break;
                case 10:
                    orderBy = @" order by GradeIncrease";
                    break;
              
                //case 11:
                //    orderBy = @" order by HasMission";
                //    break;
                case 11:
                    orderBy = @" order by State";
                    break;
                case 12:
                    orderBy = @" order by AccountantName";
                    break;
                case 13:
                    orderBy = @" order by IsDocumentsSaveToArchive";
                    break;
                case 14:
                    orderBy = @" order by ArchiveDate";
                    break;
                case 15:
                    orderBy = @" order by ArchiveNumber";
                    break;
                case 16:
                    orderBy = @" order by Saldo";
                    break;
                case 17:
                    orderBy = @" order by Position";
                    break;
                case 18:
                    orderBy = @" order by UserSumReceived";
                    break;
                case 19:
                    orderBy = @" order by Dep3Name";
                    break;
                //case 14:
                //    orderBy = @" order by NeedSecretary";
                //    break;
                //case 15:
                //    orderBy = @" order by MissionKind";
                //    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForMissionReportRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }

        public override IQuery CreateQuery(string sqlQuery)
        {
        //public int Id { get; set; }
        //public int UserId { get; set; }
        //public int Number { get; set; }
        //public string UserName { get; set; }
        //public string Dep7Name { get; set; }
        //public DateTime EditDate { get; set; }
        //public int ReportNumber { get; set; }
        //public int OrderNumber { get; set; }
        //public int Grade { get; set; }
        //public decimal GradeSum { get; set; }
        //public decimal UserSum { get; set; }
        //public decimal AccountantSum { get; set; }
        //public Decimal? GradeIncrease { get; set; }
        //public string State { get; set; }
        //public string AccountantName { get; set; }
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("Position", NHibernateUtil.String).
                AddScalar("Dep3Name",NHibernateUtil.String).
                AddScalar("Dep7Name", NHibernateUtil.String).
                AddScalar("EditDate", NHibernateUtil.DateTime).
                AddScalar("ReportNumber", NHibernateUtil.Int32).
                AddScalar("OrderNumber", NHibernateUtil.Int32).
                
                //AddScalar("MissionType", NHibernateUtil.String).
                //AddScalar("MissionKind", NHibernateUtil.String).
                //AddScalar("Target", NHibernateUtil.String).
                AddScalar("Grade", NHibernateUtil.Int32).
                AddScalar("GradeSum", NHibernateUtil.Decimal).
                AddScalar("UserSum", NHibernateUtil.Decimal).
                AddScalar("UserSumReceived", NHibernateUtil.Decimal).
                AddScalar("AccountantSum", NHibernateUtil.Decimal).
                AddScalar("GradeIncrease", NHibernateUtil.Decimal).
                AddScalar("Saldo", NHibernateUtil.Decimal).
                //AddScalar("HasMission", NHibernateUtil.String).
                //AddScalar("NeedSecretary", NHibernateUtil.String).
                AddScalar("State", NHibernateUtil.String).
                AddScalar("AccountantName", NHibernateUtil.String).
                AddScalar("IsDocumentsSaveToArchive", NHibernateUtil.String).
                AddScalar("ArchiveDate", NHibernateUtil.DateTime).
                AddScalar("ArchiveNumber", NHibernateUtil.String).
                //AddScalar("BeginDate", NHibernateUtil.DateTime).
                //AddScalar("EndDate", NHibernateUtil.DateTime).
                //AddScalar("Flag", NHibernateUtil.Boolean).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("IsDismissal", NHibernateUtil.Boolean);
        }

        public virtual List<MissionReport> GetReportsWithPurchaseBookReportCosts(int userId)
        {
            return (from report in Session.Query<MissionReport>()
                     join cost in Session.Query<MissionReportCost>() on report.Id equals cost.Report.Id
                     where cost.IsCostFromPurchaseBook && report.User.Id == userId 
                            && !report.AccountantDateAccept.HasValue
                     select report).ToList();
            /*string sqlQuery = string.Format(@" select distinct mr.Id,N'АО'+cast(mr.Number as nvarchar(10)) as Name 
                                        from dbo.MissionReport mr
                                        inner join [dbo].[MissionReportCost] mrc on  mr.Id = mrc.ReportId
                                        where mrc.IsCostFromPurchaseBook = 1 and mr.UserId = {0}
                                        and mr.AccountantDateAccept is null
                                        order by Name", userId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();*/
        }
    }
}
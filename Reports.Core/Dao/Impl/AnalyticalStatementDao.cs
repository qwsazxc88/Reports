﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using Reports.Core.Services;
using Reports.Core.Dto;
using Reports.Core.Domain;

namespace Reports.Core.Dao
{
    public class AnalyticalStatementDao :IAnalyticalStatementDao
    {
        private string query = @"SELECT 
	                                U.Id as UserId,
	                                MAX(U.Name) AS Name,
	                                (SELECT SUM(AllSum) FROM [dbo].[MissionOrder] WHERE UserId=U.Id AND CreateDate<:DateStart ) AS OrderedBefore,
	                                (SELECT SUM(AllSum) FROM [dbo].[MissionReport] WHERE  UserId=U.Id AND CreateDate<:DateStart) As ReportedBefore,
	                                SUM(MO.AllSum) AS Ordered,
	                                SUM(MR.AllSum) AS Reported,
                                    MAX(dep.Name) as Dep7Name,
                                    MAX(dep3.Name) as Dep3Name, 
                                    MAX(up.Name) as Position
                                FROM [dbo].[MissionOrder] MO
                                LEFT JOIN [dbo].[MissionReport] MR ON MO.Id= MR.MissionOrderId
                                INNER JOIN [dbo].[Users] U ON U.Id = MO.UserId
                                inner join dbo.Department dep on u.DepartmentId = dep.Id                                
                                LEFT JOIN dbo.Department dep3 ON dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3
                                left join [dbo].[Position]  up on up.Id = u.PositionId";
        private string whereString = @" WHERE"; 
        private string groupByString=" GROUP BY U.Id";
        private ISessionManager _sessionManager;
        
        public AnalyticalStatementDao(ISessionManager sessionManager)
        {
            Validate.NotNull(sessionManager, "sessionManager");
            _sessionManager = sessionManager;
        }

        protected ISession Session
        {
            get { return _sessionManager.CurrentSession; }
        }
        private IQuery CreateQuery(string query)
        {
            return Session.CreateSQLQuery(query).
                AddScalar("userId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("OrderedBefore", NHibernateUtil.Single).
                AddScalar("ReportedBefore", NHibernateUtil.Single).
                AddScalar("Ordered",NHibernateUtil.Single).
                AddScalar("Reported",NHibernateUtil.Single).
                AddScalar("Dep3Name",NHibernateUtil.String).
                AddScalar("Dep7Name",NHibernateUtil.String).
                AddScalar("Position",NHibernateUtil.String)
                ;
        }
        public IList<AnalyticalStatementDto> GetDocuments(
                int userId,
                UserRole role,
                int departmentId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                string number,
                int sortBy,
                bool? sortDescending
            )
        {
            if (beginDate == null) beginDate = new DateTime(1753, 1, 2);
           
            string AddToWhere= string.Empty;
            if (role == UserRole.ConsultantOutsorsingManager)
            {
                query = string.Format(query, string.Empty);
                query += "INNER JOIN [dbo].[UserToPersonnel] as N ON N.[UserID] = v.[UserID] and N.[PersonnelId] = " + userId + " {0}";
            }

            AddToWhere = GetWhereForUserRole(role, userId, ref query);
            if (beginDate != null)
            {
                if (!String.IsNullOrWhiteSpace(AddToWhere)) AddToWhere += " AND";
                AddToWhere += " MO.CreateDate>= :DateStart";
            }
            if (endDate != null)
            {
                if (!String.IsNullOrWhiteSpace(AddToWhere)) AddToWhere += " AND";
                AddToWhere += " MO.CreateDate<=:DateEnd";
            }
            if (!string.IsNullOrWhiteSpace(userName))
            {
                if (!String.IsNullOrWhiteSpace(AddToWhere)) AddToWhere += " AND";
                AddToWhere += " U.Name='"+userName+"'";
            }
            
            IQuery SqlQuery = CreateQuery(query+whereString+AddToWhere+groupByString);
            if (beginDate.HasValue) SqlQuery.SetDateTime("DateStart", beginDate.Value);
            if (endDate.HasValue) SqlQuery.SetDateTime("DateEnd", endDate.Value);
            var res= SqlQuery.SetResultTransformer(Transformers.AliasToBean(typeof(AnalyticalStatementDto)))
                .List<AnalyticalStatementDto>();
            switch (sortBy)
            {
                case 1: res = res.OrderBy(x => x.Name).ToList();
                    break;
                case 2: res = res.OrderBy(x => x.Position).ToList();
                    break;
                case 3: res = res.OrderBy(x => x.Dep3Name).ToList();
                    break;
                case 4: res = res.OrderBy(x => x.Dep7Name).ToList();
                    break;
                case 5: res = res.OrderBy(x => (x.ReportedBefore - x.OrderedBefore)).ToList();
                    break;
                case 6: res = res.OrderBy(x => x.Ordered).ToList();
                    break;
                case 7: res = res.OrderBy(x => x.Reported).ToList();
                    break;
                case 8: res = res.OrderBy(x => (x.ReportedBefore - x.OrderedBefore+x.Reported-x.Ordered)).ToList();
                    break;
            }
            if(sortDescending.HasValue && sortDescending.Value) res=res.Reverse().ToList();
            return res;
        }
        public string GetWhereForUserRole(UserRole role, int userId, ref string sqlQuery)
        {
            var UserDao = Ioc.Resolve<IUserDao>();
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
                            sqlQueryPart += " -1";
                            sqlQuery = string.Format(sqlQuery, "");
                            break;
                        case 4:
                        case 5:
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
                case UserRole.PersonnelManager://кадровик
                    if (userId == 10)//расчетчики
                    {
                        sqlQuery = string.Format(sqlQuery, string.Empty);
                        return @"  v.[TypeId] in (2, 4, 5, 7, 8, 10, 11, 16, 22, 23, 24, 25, 26, 27) ";
                    }
                    else
                    {
                        sqlQuery = string.Format(sqlQuery, string.Empty);
                        return string.Empty;
                    }
                case UserRole.ConsultantOutsorsingManager://кадровики ОК
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @"  v.[TypeId] in (1, 3, 6, 8, 9, 12, 13, 14, 15, 16, 17, 18, 19, 20, 22, 23, 24, 25, 26, 27) ";
                case UserRole.OutsourcingManager:
                case UserRole.ConsultantOutsourcing:
                case UserRole.Admin:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
    }
}

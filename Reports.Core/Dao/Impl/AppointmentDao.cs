﻿using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AppointmentDao : DefaultDao<Appointment>, IAppointmentDao
    {
        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }
        protected IDepartmentDao departmentDao;
        public IDepartmentDao DepartmentDao
        {
            get { return Validate.Dependency(departmentDao); }
            set { departmentDao = value; }
        }
         protected const string sqlSelectForAppointmentRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";

        protected const string sqlSelectForAppointmentList =
            @"select 
                                v.Number as AppNumber,
                                N'' as ReportNumber,
                                v.Id as Id,
                                v.EditDate as EditDate,
                                -- u.Id as UserId,
                                u.Name as UserName,
                                pos.Name as PositionName,
                                mapDep7.Name as ManDep7Name,
                                aPos.Name as CanPosition, 
                                dep3.Name as Dep3Name,
                                dep7.Name as Dep7Name,
                                v.Period as Period,
                                v.Schedule as Schedule,
                                v.Salary+v.Bonus as Salary,
                                v.DesirableBeginDate as DesirableBeginDate,
                                ar.Name as Reason
                                from dbo.Appointment v
                                inner join dbo.AppointmentReason ar on ar.Id = v.ReasonId
                                inner join dbo.Position aPos on v.PositionId = aPos.Id
                                inner join [dbo].[Users] u on u.Id = v.CreatorId
                                left join dbo.Position pos on u.PositionId = pos.Id
                                inner join dbo.Department dep on v.DepartmentId = dep.Id
                                inner join dbo.Department dep3 on dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                                inner join dbo.Department dep7 on dep.[Path] like dep7.[Path]+N'%' and dep7.ItemLevel = 7
                                left join [dbo].[Users] uEmp on uEmp.Login +
                                    case when u.RoleId & 512 > 0 then N'H' else N'R' end  
                                    = u.Login and uEmp.RoleId = 2 
                                left join dbo.Department mapDep7 on mapDep7.Id = uEmp.DepartmentId 
                                ";
                                //{1}";
         public override IQuery CreateQuery(string sqlQuery)
         {
             return Session.CreateSQLQuery(sqlQuery).
                 AddScalar("AppNumber", NHibernateUtil.Int32).
                 AddScalar("Id", NHibernateUtil.Int32).
                 AddScalar("EditDate", NHibernateUtil.DateTime).
                 //AddScalar("UserId", NHibernateUtil.Int32).
                 AddScalar("UserName", NHibernateUtil.String).
                 AddScalar("PositionName", NHibernateUtil.String).
                 AddScalar("ManDep7Name", NHibernateUtil.String).
                 AddScalar("CanPosition", NHibernateUtil.String).
                 AddScalar("Dep3Name", NHibernateUtil.String).
                 AddScalar("Dep7Name", NHibernateUtil.String).
                 AddScalar("Period", NHibernateUtil.String).
                 AddScalar("Schedule", NHibernateUtil.String).
                 AddScalar("Salary", NHibernateUtil.Decimal).
                 AddScalar("DesirableBeginDate", NHibernateUtil.DateTime).
                 AddScalar("Reason", NHibernateUtil.String).
                 AddScalar("Number", NHibernateUtil.Int32);
        }
        public AppointmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual IList<DepartmentDto> GetDepartmentsForManager23(int managerId, int level)
        {
            string sqlQuery = string.Format(@" select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentManager23ToDepartment3] mtod
                                        inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                                        where mtod.managerId = {0}",managerId);
            if(level == 2)
                sqlQuery += string.Format(@" union
                            select d.Id,d.Name,d.Path,d.ItemLevel  from [dbo].[AppointmentManager2ToManager3] mtom
                            inner join [dbo].[AppointmentManager23ToDepartment3] mtod on  mtom.[Manager3Id] = mtod.[ManagerId]
                            inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                            where mtom.[Manager2Id] = {0}", managerId);

            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentDto))).List<DepartmentDto>();
        }
        public virtual IList<int> GetManager3ForManager2(int managerId)
        {
            string sqlQuery = string.Format(@" select [Manager3Id]  as Id from [dbo].[AppointmentManager2ToManager3]
                                            where [Manager2Id] = {0}", managerId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32);
            return query./*SetResultTransformer(Transformers.AliasToBean(typeof(int))).*/List<int>();
        }
        public IList<AppointmentDto> GetDocuments(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                int sortBy,
                bool? sortDescending)
        {
            string sqlQuery = sqlSelectForAppointmentList;
            string whereString = GetWhereForUserRole(role, userId);
            //string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            //whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            //whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(AppointmentDto))).List<AppointmentDto>();
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
                orderBy = " ORDER BY EditDate DESC,UserName";
                return string.Format(sqlSelectForAppointmentRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            }
            switch (sortedBy)
            {
                case 0:
                    orderBy = " ORDER BY EditDate DESC,UserName";
                    return string.Format(sqlSelectForAppointmentRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
                case 1:
                    orderBy = @" order by AppNumber";
                    break;
                case 2:
                    orderBy += @" order by ReportNumber";
                    break;
                case 3:
                    orderBy = @" order by UserName";
                    break;
                case 4:
                    orderBy = @" order by PositionName";
                    break;
                case 20:
                    orderBy = @" order by ManDep7Name";
                    break;
                case 7:
                    orderBy = @" order by CanPosition";
                    break;
                case 5:
                    orderBy = @" order by Dep3Name";
                    break;
                case 6:
                    orderBy = @" order by Dep7Name";
                    break;
                case 8:
                    orderBy = @" order by Period";
                    break;
                case 9:
                    orderBy = @" order by Schedule";
                    break;
                case 10:
                    orderBy = @" order by Salary";
                    break;
                case 11:
                    orderBy = @" order by DesirableBeginDate";
                    break;
                case 12:
                    orderBy = @" order by Reason";
                    break;
                //case 13:
                //    orderBy = @" order by BeginDate,EndDate";
                //    break;
                //case 14:
                //    orderBy = @" order by NeedSecretary";
                //    break;
                //case 15:
                //    orderBy = @" order by MissionKind";
                //    break;
                //case 16:
                //    orderBy = @" order by AirTicketType";
                //    break;
                //case 17:
                //    orderBy = @" order by TrainTicketType";
                //    break;
            }
            if (sortDescending.Value)
                orderBy += " DESC ";
            else
                orderBy += " ASC ";
            return string.Format(sqlSelectForAppointmentRn, sqlQuery, string.Format("ROW_NUMBER() OVER({0})", orderBy));
            //sqlQuery += @" order by Date DESC,Name ";
            //return sqlQuery;
        }
        public override string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1://1, "Заявка создана"
                        statusWhere = @"ManagerDateAccept is null ";
                        break;
                    case 2://2, "Не одобрена вышестоящим руководителем"
                        statusWhere = @"ManagerDateAccept is not null and ChiefDateAccept is null ";
                        break;
                    case 3://3, "Одобрена вышестоящим руководителем"
                        statusWhere = @"ChiefDateAccept is not null  ";
                        break;
                    case 5://5, "Отменена"
                        statusWhere = @"DeleteDate is not null";
                        break;
                   
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if( statusId != 5)
                    statusWhere += " and DeleteDate is null ";
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                return whereString;
            }
            return whereString;
        }
        public override string GetDepartmentWhere(string whereString, int departmentId)
        {
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                Department dep = DepartmentDao.Load(departmentId);
                whereString += string.Format(@" mapDep7.Path  like '{0}' and mapDep7.ItemLevel = {1}", dep.Path + "%", 7);
            }
            return whereString;
        }
        public override string GetWhereForUserRole(UserRole role, int userId)
        {
             User currentUser = UserDao.Load(userId);
            if (currentUser == null)
                throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", userId));
            switch (role)
            {
                case UserRole.Manager:
                   
                    const string sqlQueryPartTemplate = @" ((u.Id = {0}) or ({1})) ";
                    string sqlQueryPart;
                    string sqlDepQueryPart;
                    switch (currentUser.Level)
                    {
                        case 2:
                            // todo manager2 to department ???
                            sqlDepQueryPart = string.Format(
                            @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager2ToManager3 dmtom on  dmtom.Manager2Id = uC.[Id]
                                    where uC.Id = {0} and dmtom.Manager3Id = u.Id
                                )", currentUser.Id);
                            break;
                        case 3:
                            sqlDepQueryPart = string.Format(
                                @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager23ToDepartment3 dmtod on  dmtod.ManagerId = uC.[Id]
                                    inner join dbo.Department dc on dc.Id = dmtod.DepartmentId
                                    where uC.Id = {0}
                                    and dep.Path like dC.Path + N'%' and dC.ItemLevel + 1 = dep.ItemLevel
                                )", currentUser.Id);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            sqlDepQueryPart = string.Format(
                                @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join [dbo].[Department] dC on  dC.Id = uC.[DepartmentId]
                                    where uC.Id = {0}
                                    and dep.Path like dC.Path + N'%' and dC.ItemLevel + 1 = dep.ItemLevel
                                )",currentUser.Id);
                            break;
                        default:
                            throw new ArgumentException(string.Format(MissionOrderDao.StrInvalidManagerLevel, 
                                userId, currentUser.Level));
                    }
                    sqlQueryPart = string.Format(sqlQueryPartTemplate,currentUser.Id,sqlDepQueryPart);
                    return sqlQueryPart;
                case UserRole.Director:
                    sqlDepQueryPart = string.Format(@" ((u.Level = 2) or (u.RoleId = {0} and u.Id != {1})) ",
                                                    (int)UserRole.Director,userId);
                    sqlQueryPart = string.Format(sqlQueryPartTemplate,currentUser.Id,sqlDepQueryPart);
                    return sqlQueryPart;
                case UserRole.PersonnelManager:
                case UserRole.OutsourcingManager:
                case UserRole.StaffManager:
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
    }
}
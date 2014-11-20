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
        #region Selects for documents list
        protected const string sqlSelectForAppointmentRn = @";with res as
                                ({0})
                                select {1} as Number,* from res order by Number ";

        protected const string sqlSelectForAppointmentList =
            @"select 
                v.Number as AppNumber,
                -- N'' as ReportNumber,
                v.Id as Id,
                v.EditDate as EditDate,
                -- u.Id as UserId,
                u.Name as UserName,
                pos.Name as PositionName,
                mapDep7.Name as ManDep7Name,
                -- aPos.Name as CanPosition, 
                v.PositionName as CanPosition, 
                dep3.Name as Dep3Name,
                dep7.Name as Dep7Name,
                -- v.Period as Period,
                v.Schedule as Schedule,
                v.Salary+v.Bonus as Salary,
                v.DesirableBeginDate as DesirableBeginDate,
                ar.Name as Reason,
                r.[Id] as RId,
                r.[Number] as RNumber,
                case when r.[Id] is null then N''
                        when r.[StaffDateAccept] is null then N'Нет' 
                        else N'Да' end as RStaffAccept,
                r.Name as RName,
                r.[Phone] as Phone,
                r.[Email] as Email,
                case when r.[Id] is null then  N'' 
                        when r.[DateAccept] is null and r.[DeleteDate] is null then N''
                        when r.[DateAccept] is null then N'Нет' 
                        else N'Да' end as RApprove,
                case when r.[Id] is null then  N''
                        when r.[DateAccept] is null and r.[DeleteDate] is null then N'' 
                        when r.[DeleteDate] is null then N'' 
                        else r.[RejectReason] end as RReject,
                ur.Name as StaffName,
                case
                        when v.ManagerDateAccept is null then N'Черновик'
                        when v.ManagerDateAccept is not null and v.ChiefDateAccept is null then N'Отправлена на согласование вышестоящему руководителю'
                        when v.ChiefDateAccept is not null and v.StaffDateAccept is null then N'Согласована вышестоящим руководителем'
                        when v.StaffDateAccept is not null then N'Принята в работу'                        
                        when v.DeleteDate is not null then N'Отменена'
                        else N''
                        end as Status
                from dbo.Appointment v
                left join  dbo.AppointmentReport r on r.[AppointmentId] = v.Id
                left join [dbo].[Users] ur on ur.Id = r.CreatorId
                inner join dbo.AppointmentReason ar on ar.Id = v.ReasonId
                -- inner join dbo.Position aPos on v.PositionId = aPos.Id
                inner join [dbo].[Users] u on u.Id = v.CreatorId
                left join dbo.Position pos on u.PositionId = pos.Id
                inner join dbo.Department dep on v.DepartmentId = dep.Id
                inner join dbo.Department crDep on u.DepartmentId = crDep.Id
                inner join dbo.Department dep3 on dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                inner join dbo.Department dep7 on dep.[Path] like dep7.[Path]+N'%' and dep7.ItemLevel = 7
                left join [dbo].[Users] uEmp on uEmp.Login +
                    case when u.RoleId & 512 > 0 then N'H' else N'R' end  
                    = u.Login and uEmp.RoleId = 2 
                left join dbo.Department mapDep7 on mapDep7.Id = uEmp.DepartmentId 
                ";
        #endregion
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
                //AddScalar("Period", NHibernateUtil.String).
                AddScalar("Schedule", NHibernateUtil.String).
                AddScalar("Salary", NHibernateUtil.Decimal).
                AddScalar("DesirableBeginDate", NHibernateUtil.DateTime).
                AddScalar("Reason", NHibernateUtil.String).
                AddScalar("RId", NHibernateUtil.Int32).
                AddScalar("RNumber", NHibernateUtil.Int32).
                AddScalar("RStaffAccept", NHibernateUtil.String).
                AddScalar("RName", NHibernateUtil.String).
                AddScalar("Phone", NHibernateUtil.String).
                AddScalar("Email", NHibernateUtil.String).
                AddScalar("RApprove", NHibernateUtil.String).
                AddScalar("RReject", NHibernateUtil.String).
                AddScalar("StaffName", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String);
        }
        public AppointmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual IList<DepartmentDto> GetDepartmentsForManager23(int managerId, int level,bool dep3only)
        {
            string sqlQuery = string.Format(@" select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentManager23ToDepartment3] mtod
                                        inner join [dbo].[Department] d on d.Id = mtod.[DepartmentId]
                                        where mtod.managerId = {0}",managerId);
            if(level == 2 && !dep3only)
                sqlQuery += string.Format(@" union
                            select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[AppointmentCreateManager2ToDepartment2] mctod
                                        inner join [dbo].[Department] d on d.Id = mctod.[DepartmentId]
                                        where mctod.managerId = {0}", managerId);

            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentDto))).List<DepartmentDto>();
        }
        public virtual DepartmentDto GetDepartmentForPathAndLevel(string path,int level)
        {
            const string sqlQuery = @"select d.Id,d.Name,d.Path,d.ItemLevel from [dbo].[Department] d
                                            where :path like Path+N'%' and ItemLevel = :level";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Path", NHibernateUtil.String).
                AddScalar("ItemLevel", NHibernateUtil.Int32).
                SetString("path", path).
                SetInt32("level", level);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DepartmentDto))).UniqueResult<DepartmentDto>();
        }
        public virtual IList<int> GetManager3ForManager2(int managerId)
        {
            string sqlQuery = string.Format(@" select [Manager3Id]  as Id from [dbo].[AppointmentManager2ToManager3]
                                            where [Manager2Id] = {0}", managerId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32);
            return query.List<int>();
        }
        public virtual IList<int> GetChildrenManager2ForManager2(int parentId)
        {
            string sqlQuery = string.Format(@" select [ChildId]  as Id from [dbo].[AppointmentManager2ParentToManager2Child]
                                            where [ParentId] = {0}", parentId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32);
            return query.List<int>();
        }
        public virtual List<IdNameDto> GetParentForManager2(int childId)
        {
            string sqlQuery = string.Format(@" select u.Id,u.email as Name from users u
                    inner join dbo.AppointmentManager2ParentToManager2Child mptomc on mptomc.ParentId = u.id
                    where mptomc.ChildId = {0}",childId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        public virtual List<IdNameDto> GetParentForManager3(int childId)
        {
            string sqlQuery = string.Format(@"select u.Id,u.email as Name from users u
                        inner join dbo.AppointmentManager2ToManager3 mptomc on mptomc.Manager2Id = u.id
                        where mptomc.Manager3Id = {0}", childId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String);;
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        public virtual List<IdNameDto> GetParentForManager4Department(int departmentId)
        {
            string sqlQuery = string.Format(@"select distinct  u.Id,u.email as Name from users u
                            inner join  dbo.AppointmentManager23ToDepartment3 mtod on mtod.ManagerId = u.Id
                            inner join dbo.Department dParent on dParent.Id = mtod.DepartmentId
                            inner join dbo.Department dChild on dChild.Path like dParent.Path +N'%' 
                            and dChild.ItemLevel = dParent.ItemLevel + 1
                            where dChild.Id = {0}", departmentId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String); ;
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        public virtual List<IdNameDto> GetParentForManagerDepartment(int departmentId)
        {
            string sqlQuery = string.Format(@"select distinct u.Id,u.email as Name from users u
                        inner join dbo.Department dParent on dParent.Id = u.DepartmentId
                        inner join dbo.Department dChild on dChild.Path like dParent.Path +N'%' 
                        and dChild.ItemLevel = dParent.ItemLevel + 1
                        where dChild.Id = {0}", departmentId);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                    AddScalar("Id", NHibernateUtil.Int32).
                    AddScalar("Name", NHibernateUtil.String); ;
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>().ToList();
        }
        #region Search documents for list
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
                    orderBy += @" order by RNumber";
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
                case 13:
                    orderBy = @" order by RStaffAccept";
                    break;
                case 14:
                    orderBy = @" order by RName";
                    break;
                case 15:
                    orderBy = @" order by Phone";
                    break;
                case 16:
                    orderBy = @" order by Email";
                    break;
                case 17:
                    orderBy = @" order by RApprove";
                    break;
                case 18:
                    orderBy = @" order by RReject";
                    break;
                case 19:
                    orderBy = @" order by StaffName";
                    break;
                case 21:
                    orderBy = @" order by Status";
                    break;
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
                    case 1://1, "Черновик"
                        statusWhere = @"v.ManagerDateAccept is null ";
                        break;
                    case 2://2, "Отправлена на согласование вышестоящему руководителю"
                        statusWhere = @"v.ManagerDateAccept is not null and v.ChiefDateAccept is null ";
                        break;
                    case 3://3, "Согласована вышестоящим руководителем"
                        statusWhere = @"v.ChiefDateAccept is not null and v.StaffDateAccept is null ";
                        break;
                    case 4://4, "Принята в работу"
                        statusWhere = @"v.StaffDateAccept is not null ";
                        break;
                    case 5://5, "Отменена"
                        statusWhere = @"v.DeleteDate is not null";
                        break;
                   
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if( statusId != 5)
                    statusWhere += " and v.DeleteDate is null ";
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
                                )
                                or
                                exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager23ToDepartment3 dmtod on  dmtod.ManagerId = uC.[Id]
                                    inner join dbo.Department dc on dc.Id = dmtod.DepartmentId
                                    where uC.Id = {0}
                                    and crDep.Path like dC.Path + N'%' and dC.ItemLevel + 1 = crDep.ItemLevel
                                )
                                or
                                exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager2ParentToManager2Child dmtom on  dmtom.ParentId = uC.[Id]
                                    where uC.Id = {0} and dmtom.ChildId = u.Id
                                )
                                ", currentUser.Id);
                            break;
                        case 3:
                            sqlDepQueryPart = string.Format(
                                @" exists 
                                ( 
                                    select uC.Id from dbo.Users uC
                                    inner join  dbo.AppointmentManager23ToDepartment3 dmtod on  dmtod.ManagerId = uC.[Id]
                                    inner join dbo.Department dc on dc.Id = dmtod.DepartmentId
                                    where uC.Id = {0}
                                    and crDep.Path like dC.Path + N'%' and dC.ItemLevel + 1 = crDep.ItemLevel
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
                                    and crDep.Path like dC.Path + N'%' and dC.ItemLevel + 1 = crDep.ItemLevel
                                )", currentUser.Id);
                            break;
                        default:
                            throw new ArgumentException(string.Format(MissionOrderDao.StrInvalidManagerLevel, 
                                userId, currentUser.Level));
                    }
                    string sqlQueryPart = string.Format(sqlQueryPartTemplate,currentUser.Id,sqlDepQueryPart);
                    return sqlQueryPart;
                //case UserRole.Director:
                //    sqlDepQueryPart = string.Format(@" ((u.Level = 2) or (u.RoleId = {0} and u.Id != {1})) ",
                //                                    (int)UserRole.Director,userId);
                //    sqlQueryPart = string.Format(sqlQueryPartTemplate,currentUser.Id,sqlDepQueryPart);
                //    return sqlQueryPart;
                case UserRole.PersonnelManager:
                case UserRole.OutsourcingManager:
                case UserRole.StaffManager:
                    return string.Empty;
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }
        #endregion
    }
}
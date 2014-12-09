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
                                dep.Name as Dep7Name,
                                v.Number as RequestNumber,
                                v.CreateDate as CreateDate,
                                v.ConfirmWorkDate as ConfirmWorkDate,
                                t.Name as RequestType,
                                m.Name as RequestTransferType,
                                case when v.[SendDate] is null then 1
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then 2 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then 3 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null then 4
                                     when v.[ConfirmWorkDate] is not null then 5 
                                    else 0
                                end as StatusNumber,
                                case when v.[SendDate] is null then N'Черновик сотрудника'
                                     when v.[SendDate] is not null and v.[BeginWorkDate] is null then N'Услуга запрошена' 
                                     when v.[BeginWorkDate] is not null and v.[EndWorkDate] is null then N'Услуга формируется' 
                                     when v.[EndWorkDate] is not null and v.[ConfirmWorkDate] is null then N'Услуга сформирована' 
                                     when v.[ConfirmWorkDate] is not null then N'Услуга оказана' 
                                    else N''
                                end as Status,
                                v.Address as address,
                                J.Name as Dep3Name,
                                L.Name as ProdTimeName
                                from dbo.HelpServiceRequest v
                                inner join [dbo].[HelpServiceType] t on v.TypeId = t.Id
                                inner join [dbo].[HelpServiceTransferMethod] m on v.[TransferMethodId] = m.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                inner join [dbo].[Users] crUser on crUser.Id = v.CreatorId
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                inner join dbo.Department dep on u.DepartmentId = dep.Id
                                LEFT JOIN [dbo].[Department] as H ON H.Code = dep.ParentId
                                LEFT JOIN [dbo].[Department] as I ON I.Code = H.ParentId
                                LEFT JOIN [dbo].[Department] as J ON J.Code = I.ParentId
                                LEFT JOIN [dbo].[Department] as K ON K.Code = J.ParentId
                                LEFT JOIN [dbo].[HelpServiceProductionTime] as L ON L.Id = v.ProductionTimeId
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
                AddScalar("CreateDate", NHibernateUtil.DateTime).
                AddScalar("ConfirmWorkDate", NHibernateUtil.DateTime).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestTransferType", NHibernateUtil.String).
                AddScalar("StatusNumber", NHibernateUtil.Int32).
                AddScalar("Status", NHibernateUtil.String).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("Address", NHibernateUtil.String).
                AddScalar("Dep3Name", NHibernateUtil.String).
                AddScalar("ProdTimeName", NHibernateUtil.String);  
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
                string Address)
        {
            string sqlQuery = sqlSelectForHsList;

            //для кадровиков показываем вопросы по своим дирекциям
            if (role == UserRole.PersonnelManager)
            {
                sqlQuery = string.Format(sqlQuery, string.Empty);
                sqlQuery += "INNER JOIN [dbo].[UserToPersonnel] as N ON N.[UserID] = v.[UserID] and N.[PersonnelId] = " + userId.ToString() + " {0}";
            }

            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetNumberWhere(whereString, number);
            //для текущего пользователя и выбранной роли надо показывать услуги его дирекции

            //User user = userDao.FindById(AuthenticationService.CurrentUser.Id);

            //User user = UserDao.FindByLogin(model.UserName);
            //List<UserRolesDto> usersAndRoles = GetUserRoles(user);

            //IList<User> list = UserDao.FindByEmail(user.Email);
            //foreach (User usr in list.Where(x => x.IsActive))
            //{
            //    List<UserRole> roles = new List<UserRole>();
            //    GetRolesForUser(roles, usr);
            //    usersAndRoles.Add(new UserRolesDto { user = usr, roles = roles });
            //}


            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString, sortBy, sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate, userName);
            if (!string.IsNullOrEmpty(number))
                query.SetString("number", number);
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
                    sqlQuery = string.Format(sqlQuery,string.Empty);
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
                            IList<Department> depList =  ManualRoleRecordDao.LoadDepartmentsForUserId(currentUser.Id);
                            if(depList == null || depList.Count() == 0)
                                throw new ArgumentException(string.Format(StrNoManagerDepartments, currentUser.Id));
                            sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                            return string.Format(@" depM.Id in {0}", CoreUtils.CreateIn("(",depList));
                        case 4:
                        case 5:
                        case 6:
                            if(currentUser.Department == null)
                                throw new ArgumentException(string.Format(StrInvalidManagerDepartment,currentUser.Id));
                            sqlQueryPart = @" inner join dbo.Department depM on dep.Path like depM.Path +N'%'";
                            sqlQuery = string.Format(sqlQuery, sqlQueryPart);
                            return string.Format(@" depM.Id = {0}",currentUser.Department.Id);
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, currentUser.Id, 
                                currentUser.Level));
                    }
                    //return sqlQueryPart;
                case UserRole.OutsourcingManager:
                case UserRole.ConsultantOutsourcing:
                case UserRole.PersonnelManager:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @"  v.[TypeId] in (2, 4, 5) ";
                case UserRole.ConsultantOutsorsingManager:
                    sqlQuery = string.Format(sqlQuery, string.Empty);
                    return @"  v.[TypeId] in (1, 3, 6) ";
                case UserRole.Admin:
                    sqlQuery = string.Format(sqlQuery,string.Empty);
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
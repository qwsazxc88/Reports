using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class UserDao : DefaultDao<User>, IUserDao
    {
        public const string FKExistsViewName = "v_user_fk_exists";
    	public const string LastNameSortFieldName = "LastName";
		public const string FirstNameSortFieldName = "FirstName";
        public const string MiddleNameSortFieldName = "MiddleName";
        public const int bufferSize = 1000;


       

        public int PageSize
        {
            get { return ConfigurationService.PageSize; }
        }

        public UserDao(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public string ConstFKExistsViewName
        {
            get { return FKExistsViewName; }
        }

        public virtual User FindByLogin(string login)
        {
            return (User) Session.CreateCriteria(typeof(User))
                              .Add(Restrictions.Eq("Login", login)).UniqueResult();
        }
        public virtual bool IsLoginWithOtherIdExists(string login,int id)
        {
            return (int)Session.CreateCriteria(typeof(User))
                              .Add(Restrictions.Eq("Login", login))
                              .Add(Restrictions.Not(Restrictions.Eq("Id", id)))
                              .SetProjection(Projections.RowCount())
                              .UniqueResult() > 0;
        }
        public virtual IList<User> FindByEmail(string email)
        {
            var sessionCriteria = Session.CreateCriteria(typeof(User));
            var queryResult = sessionCriteria.Add(Expression.Sql("lower({alias}.Email) = lower(?)", email, NHibernateUtil.String));
            var lst = queryResult.List<User>();
            return lst;
            //return Session.CreateCriteria(typeof(User))
            //                .Add( Expression.Sql("lower({alias}.Email) = lower(?)", email, NHibernateUtil.String ))
            //                .List<User>();
        }
        public virtual IList<User> GetUsersWithRole(UserRole role)
        {
            return Session.CreateCriteria(typeof(User))
                  .Add(Restrictions.Eq("RoleId",(int)role))
//                  .Add(Restrictions.Eq("IsActive", true))
                  .Add(Restrictions.IsNull("DateRelease"))
                  .List<User>();
        }
        public virtual User GetManagerForEmployee(string login)
        {
            return (User)Session.CreateCriteria(typeof(User))
                  .Add(Restrictions.Eq("Login", login+"R"))
                  .Add(Restrictions.Eq("RoleId", 4))
                  .Add(Restrictions.Eq("IsActive", true))
                  .UniqueResult();
        }
        public virtual IList<IdNameDto> GetMainManagersForLevelDepartment(int level, string departmentPath)
        {
            const string sqlQuery = @" select u.Id,u.Email as Name from Users u 
                    inner join Department d on u.DepartmentId = d.Id
                    where u.Level = :level 
                    and u.RoleId = 4 
                    and u.IsMainManager = 1
                    and :path like d.Path+N'%'";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                SetInt32("level", level).
                SetString("path", departmentPath);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
        public virtual IList<IdNameDto> GetManagersWithDepartments()
        {
            const string sqlQuery = @" select u.Id,u.Name+N' '+d.Name as Name from Users u 
                    inner join Department d on u.DepartmentId = d.Id
                    where (u.RoleId & 4) > 0 
                    and u.[IsActive] = 1
                    order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }

        public virtual IList<IdNameDto> GetManagersAndEmployeesForCreateMissionOrder(string departmentPath, int level)
        {
            const string sqlQuery = @" select emp.Id,emp.Name from Users emp
                    inner join Users man on emp.Login+N'R' = man.Login
                    and ((emp.RoleId & 2) > 0) and man.RoleId = 4
                    and emp.IsActive = 1 and man.IsActive = 1 
                    inner join dbo.Department d on man.DepartmentId = d.Id
                    where d.Path like :path and 
                    ((man.[level] > :level) or ((man.[level] = :level) and (man.IsMainManager = 0)))
                    union 
                    select emp.Id,emp.Name from Users emp
                    inner join dbo.Department d on emp.DepartmentId = d.Id
                    where ((emp.RoleId & 2) > 0) 
                    and emp.IsActive = 1 
                    and d.Path like :path
                    order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                SetString("path", departmentPath + "%").
                SetInt32("level", level);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
        public virtual IList<User> GetUserWithEmailAndRole(UserRole role, string email,
            string departmentPath, List<int> levelList)
        {
            const string sqlQuery = @" select u.* from Users u
                                inner join dbo.Department d on d.Id = u.DepartmentId
                                where u.IsActive = 1 and u.DateRelease is null
                                and u.Level in (:levelList)
                                and (u.RoleId & :role) > 0 
                                and u.eMail = :email
                                and d.Path like :path";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                SetInt32("role", (int) role).
                SetString("email", email).
                SetString("path", departmentPath + "%").
                SetParameterList("levelList", levelList);
            return query.List<User>();
            //return Session.CreateCriteria(typeof(User))
            //      .Add(Restrictions.Eq("RoleId", (int)role))
            //      .Add(Restrictions.Eq("IsActive", true))
            //      .Add(Restrictions.Not(Restrictions.Eq("Level", 0)))
            //      .Add(Restrictions.Eq("Email", email))
            //      .Add(Restrictions.IsNull("DateRelease"))
            //      .List<User>();
        }
        public virtual IList<User> LoadUserByCodes(List<string> codes)
        {
            if(codes.Count > bufferSize)
            {
                List<User> result = new List<User>();
                int i = codes.Count/bufferSize;
                for (int j = 0; j < i; j++)
                {
                    List<string> buffer = codes.Skip(j*bufferSize).Take(bufferSize).ToList();
                    List<User> tempList =  Session.CreateCriteria(typeof(User))
                        .Add(Restrictions.In("Code",buffer))
                        .List<User>().ToList();
                    result.AddRange(tempList);
                }
                int rest = codes.Count % bufferSize;
                if (rest > 0)
                {
                    List<string> restBuffer = codes.Skip(i*bufferSize).ToList();
                    List<User> restTempList = Session.CreateCriteria(typeof (User))
                        .Add(Restrictions.In("Code", restBuffer))
                        .List<User>().ToList();
                    result.AddRange(restTempList);
                }
                return result;
            }
            return Session.CreateCriteria(typeof(User))
            .Add(Restrictions.In("Code", codes))
            .List<User>();
        }
        public IList<User> LoadForIdsList(List<int> userIds)
        {
            if (userIds.Count == 0)
                return new List<User>();
            ICriteria criteria = Session.CreateCriteria(typeof(User));
            criteria.Add(Restrictions.In("Id", userIds));
            criteria.AddOrder(new Order("Name", true));
            return criteria.List<User>();
        }
        public virtual int DeleteEmployees(DateTime date)
        {
            const string sqlQuery =
                @"delete at from dbo.Attachment at
                inner join dbo.Document doc on doc.id = at.DocumentId
                inner join users u  on u.id = doc.UserId
                where u.RoleId = :roleId and u.DateRelease < :deleteDate

                delete dc from dbo.DocumentComment dc
                inner join dbo.Document doc on doc.id = dc.DocumentId
                inner join users u  on u.id = doc.UserId
                where u.RoleId = :roleId and u.DateRelease < :deleteDate

                delete doc from dbo.Document doc 
                inner join users u  on u.id = doc.UserId
                where u.RoleId = :roleId and u.DateRelease < :deleteDate

                delete td from dbo.TimesheetDay td
                inner join dbo.Timesheet t on t.id = td.TimesheetId
                inner join users u  on u.id = t.UserId
                where u.RoleId = :roleId and u.DateRelease < :deleteDate

                delete t from dbo.Timesheet t
                inner join users u on u.id = t.UserId
                where u.RoleId = :roleId and u.DateRelease < :deleteDate


                delete u  from users u
                where u.RoleId = :roleId and u.DateRelease < :deleteDate";

            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
            query.SetInt32("roleId", (int)UserRole.Employee);
            query.SetDateTime("deleteDate", date);
            return query.ExecuteUpdate();
        }

        public override User MergeAndFlush(User entity)
        {
            try
            {
                User result = (User)Session.Merge(entity);
                Session.Flush();
                return result;                
            }
            catch(Exception ex)
            {
                Log.Error(
                string.Format("Exception for user with id {0},code {1}",
                                entity.Id, entity.Code),ex);
                throw;
            }
        }

        public virtual int Count()
        {
            return (int)Session.CreateCriteria(typeof(User))
                            .SetProjection(Projections.RowCount())
                            .UniqueResult();
        }

        public IList<IdNameDtoWithDates> GetUsersForManagerWithDatePaged(int managerId, UserRole managerRole,
            DateTime beginDate,DateTime endDate,int departmentId, string userName)
        {
            string sqlQuery =
                        @"select 
            u.Id 
            ,u.Name
            ,case when emp.BeginDate is not null then emp.BeginDate else u.DateAccept end as DateAccept
            ,case when dis.EndDate is not null then dis.EndDate else u.DateRelease end as DateRelease
            from dbo.Users u
            left join dbo.Employment emp on emp.UserId = u.Id
            and u.IsNew = 1
            and emp.ManagerDateAccept is not null
            and emp.UserDateAccept is not null
            and emp.PersonnelManagerDateAccept is not null
            and emp.DeleteDate is null
            left join dbo.Dismissal dis on
            dis.UserId = u.Id
            and dis.ManagerDateAccept is not null
            and dis.UserDateAccept is not null
            and dis.PersonnelManagerDateAccept is not null
            and dis.DeleteDate is null
            ";
            string sqlWhere = string.Empty;
            sqlWhere += " DateAccept <= :endDate and ( DateRelease is null or DateRelease >= :beginDate ) AND ";
            if (!string.IsNullOrEmpty(userName))
                sqlWhere += " u.Name like :userName AND ";
            switch (managerRole)
            {
                case UserRole.Employee:
                    sqlWhere += "u.Id = :userId";
                    break;
                case UserRole.Manager:
                    string userSelectionSubquery = string.Empty;
                    User currentUser = Get(managerId);
                    if (currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", managerId));

                    switch (currentUser.Level)
                    {
                        case 2:
                        case 3:
                            userSelectionSubquery = "-1";
                            break;
                        case 4:
                        case 5:
                        case 6:
                            // Выборка замов и руководителей нижележащих уровней по ветке для применения автоматических прав уровней 4-6
                            userSelectionSubquery = @" select distinct managerEmployeeAccount.Id from dbo.Users managerEmployeeAccount
                                inner join dbo.Users currentUser
                                    on currentUser.Id = :userId
                                inner join dbo.Users managerManagerAccount
                                    on managerManagerAccount.Login = managerEmployeeAccount.Login+N'R'
                                        and managerManagerAccount.RoleId = 4
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
                                    and not currentUser.Login = managerEmployeeAccount.Login + N'R'
                             
                                union

                                select distinct employee.Id from Users employee
                                    inner join dbo.Users currentUser
	                                    on currentUser.Id = :userId
                                    left join [dbo].[Users] employeeManagerAccount
                                        on (employeeManagerAccount.RoleId & 4) > 0
                                            and employeeManagerAccount.Login = u.Login+N'R'
                                            --and employeeManagerAccount.IsActive = 1
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
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, managerId, currentUser.Level));
                    }

                    sqlWhere = string.Format(@"{0}
                        (
                            ( u.Level>3 or u.Level IS NULL )
                            and u.Id in
                            (
                                {1}
                            )
                        )", sqlWhere, userSelectionSubquery);
                    sqlWhere = string.Format(@"{0}
                        or u.Id in
                        (
                            select mrr.TargetUserId
                            from [dbo].[ManualRoleRecord] mrr
                            where mrr.UserId = :userId and mrr.RoleId = 2 and mrr.TargetUserId is not null
                        )
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
                                        on mrr.UserId = :userId
                                    inner join Role
                                        on mrr.RoleId = 2
                            )
                        )
                        ", sqlWhere);
                    break;
                case UserRole.PersonnelManager:
                    sqlWhere += string.Format("u.RoleId = {0}  and  exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ", (int)UserRole.Employee);//"u.PersonnelManagerId = :userId";
                    break;
                case UserRole.Chief:
                    sqlWhere += string.Format("u.RoleId = {0}  and  exists ( select * from ChiefToUser cu where cu.ChiefId = :userId and u.Id = cu.UserId ) ", (int)UserRole.Employee);//"u.PersonnelManagerId = :userId";
                    break;
                case UserRole.OutsourcingManager:
                    sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 5);
                    break;
                default:
                    break;
            }
            if(departmentId != 0 && (managerRole & UserRole.Employee) != UserRole.Employee)
                sqlWhere = GetDepartmentWhere(sqlWhere, departmentId);

            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by u.Name,u.Id ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("DateAccept", NHibernateUtil.DateTime).
                AddScalar("DateRelease", NHibernateUtil.DateTime);
            query.
                SetDateTime("beginDate", beginDate).
                SetDateTime("endDate", endDate);
            if ((managerRole & UserRole.OutsourcingManager) != UserRole.OutsourcingManager)
                query.SetInt32("userId", managerId);
            if(!string.IsNullOrEmpty(userName))
                query.SetString("userName", "%"+userName+"%");
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDtoWithDates))).List<IdNameDtoWithDates>();
        }
        public IList<IdNameDtoWithDates> GetUsersForManagerWithDatePagedForGraphics(int managerId, UserRole managerRole,
            DateTime beginDate, DateTime endDate, int departmentId, string userName)
        {
            string sqlQuery =
                        @"select 
            u.Id 
            ,u.Name
            ,case when emp.BeginDate is not null then emp.BeginDate else u.DateAccept end as DateAccept
            ,case when dis.EndDate is not null then dis.EndDate else u.DateRelease end as DateRelease
            from dbo.Users u
            left join dbo.Employment emp on emp.UserId = u.Id
            and u.IsNew = 1
            and emp.ManagerDateAccept is not null
            and emp.UserDateAccept is not null
            and emp.PersonnelManagerDateAccept is not null
            and emp.DeleteDate is null
            left join dbo.Dismissal dis on
            dis.UserId = u.Id
            and dis.ManagerDateAccept is not null
            and dis.UserDateAccept is not null
            and dis.PersonnelManagerDateAccept is not null
            and dis.DeleteDate is null
            ";
            string sqlWhere = string.Empty;
            sqlWhere += " DateAccept <= :endDate and ( DateRelease is null or DateRelease >= :beginDate ) AND ";
            if (!string.IsNullOrEmpty(userName))
                sqlWhere += " u.Name like :userName AND ";
            switch (managerRole)
            {
                case UserRole.Employee:
                    sqlWhere += "u.Id = :userId";
                    break;
                case UserRole.Manager:
                    sqlWhere += @"u.Id in (select u2.Id from [dbo].[Users] u1 
                                    inner join [dbo].[Department] d on u1.[DepartmentId] = d.Id and  u1.IsActive = 1 and u1.RoleId = 4 
                                    inner join [dbo].[Department] d1 on d1.Path like d.Path+N'%' and d1.ItemLevel = 7
                                    inner join [dbo].[Users] u2 on u2.[DepartmentId]  = d1.Id and ((u2.RoleId & 2) > 0) and u2.IsActive = 1
                                    and (u2.Code + N'R' != u1.Code)
                                    where u1.Id =  :userId 
                                    and not exists 
                                    (
	                                    select Id from [dbo].[Users] u3 
	                                    where u3.Code = u2.Code+N'R' and u3.RoleId = 4 and u3.IsActive = 1
	                                    and u3.level < u1.level
                                    ))";
                    break;
                /*case UserRole.PersonnelManager:
                    sqlWhere += string.Format("u.RoleId = {0}  and  exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ", (int)UserRole.Employee);//"u.PersonnelManagerId = :userId";
                    break;
                case UserRole.Chief:
                    sqlWhere += string.Format("u.RoleId = {0}  and  exists ( select * from ChiefToUser cu where cu.ChiefId = :userId and u.Id = cu.UserId ) ", (int)UserRole.Employee);//"u.PersonnelManagerId = :userId";
                    break;*/
                case UserRole.OutsourcingManager:
                    sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 5);
                    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль {0}",(int)managerRole));
            }
            if (departmentId != 0 && (managerRole & UserRole.Employee) != UserRole.Employee)
                sqlWhere = GetDepartmentWhere(sqlWhere, departmentId);

            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by u.Name,u.Id ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("DateAccept", NHibernateUtil.DateTime).
                AddScalar("DateRelease", NHibernateUtil.DateTime);
            query.
                SetDateTime("beginDate", beginDate).
                SetDateTime("endDate", endDate);
            if ((managerRole & UserRole.OutsourcingManager) != UserRole.OutsourcingManager)
                query.SetInt32("userId", managerId);
            if (!string.IsNullOrEmpty(userName))
                query.SetString("userName", "%" + userName + "%");
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDtoWithDates))).List<IdNameDtoWithDates>();
        }

        public IList<IdNameDtoWithDates> GetUsersForManagerWithDate(int userId, UserRole managerRole)
        {
            string sqlQuery =
                        @"select 
            u.Id 
            ,u.Name
            ,case when emp.BeginDate is not null then emp.BeginDate else u.DateAccept end as DateAccept
            ,case when dis.EndDate is not null then dis.EndDate else u.DateRelease end as DateRelease
            from dbo.Users u
            left join dbo.Employment emp on emp.UserId = u.Id
            and u.IsNew = 1
            and emp.ManagerDateAccept is not null
            and emp.UserDateAccept is not null
            and emp.PersonnelManagerDateAccept is not null
            and emp.DeleteDate is null
            left join dbo.Dismissal dis on
            dis.UserId = u.Id
            and dis.ManagerDateAccept is not null
            and dis.UserDateAccept is not null
            and dis.PersonnelManagerDateAccept is not null
            and dis.DeleteDate is null
            ";
            string sqlWhere = string.Empty;
            switch (managerRole)
            {
                case UserRole.Employee:
                    sqlWhere += "u.Id = :userId";
                    break;
                case UserRole.Manager:
                    sqlWhere += "u.ManagerId = :userId";
                    break;
                case UserRole.PersonnelManager:
                    sqlWhere += string.Format("u.RoleId = {0}  and  exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) ",(int)UserRole.Employee);//"u.PersonnelManagerId = :userId";
                    break;
                default:
                    break;
            }
            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by u.Name,u.Id ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("DateAccept", NHibernateUtil.DateTime).
                AddScalar("DateRelease", NHibernateUtil.DateTime);
            query.SetInt32("userId", userId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDtoWithDates))).List<IdNameDtoWithDates>();
        }

        public IList<IdNameDto> GetUsersForManager(int managerId, UserRole managerRole,
            int departmentId)
        {
            string sqlQuery ="select u.Id,u.Name from dbo.Users u";
            string sqlWhere = string.Empty;
            switch (managerRole)
            {
                case UserRole.Employee:
                    throw new ArgumentException("Список сотрудников недоступен для сотрудника.");
                case UserRole.Manager:
                    string userSelectionSubquery = string.Empty;
                    User currentUser = Get(managerId);
                    if (currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", managerId));

                    switch (currentUser.Level)
                    {
                        case 2:
                        case 3:
                            userSelectionSubquery = "-1";
                            break;
                        case 4:
                        case 5:
                        case 6:
                            // Выборка замов и руководителей нижележащих уровней по ветке для применения автоматических прав уровней 4-6
                            userSelectionSubquery = @" select distinct managerEmployeeAccount.Id from dbo.Users managerEmployeeAccount
                                inner join dbo.Users currentUser
                                    on currentUser.Id = :userId
                                inner join dbo.Users managerManagerAccount
                                    on managerManagerAccount.Login = managerEmployeeAccount.Login+N'R'
                                        and managerManagerAccount.RoleId = 4
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
                                    and not currentUser.Login = managerEmployeeAccount.Login + N'R'
                             
                                union

                                select distinct employee.Id from Users employee
                                    inner join dbo.Users currentUser
	                                    on currentUser.Id = :userId
                                    left join [dbo].[Users] employeeManagerAccount
                                        on (employeeManagerAccount.RoleId & 4) > 0
                                            and employeeManagerAccount.Login = u.Login+N'R'
                                            --and employeeManagerAccount.IsActive = 1
                                    inner join dbo.Department employeeDept
                                        on employee.DepartmentId = employeeDept.Id
                                        -- Исключить состоящих в ветке руководства
                                        and employeeDept.Path not like N'9900424.9900426.9900427.%'
                                    inner join dbo.Department higherDept
                                        on employeeDept.Path like higherDept.Path+N'%'
                                where (employee.RoleId & 2) > 0
                                    and employee.IsActive = 1
                                    and (employeeManagerAccount.Id is null or employeeManagerAccount.IsActive = 0)
                                    and currentUser.DepartmentId = higherDept.Id
                                    and not currentUser.Login = employee.Login + N'R'";
                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, managerId, currentUser.Level));
                    }

                    sqlWhere = string.Format(@"{0}
                        (
                            ( u.Level>3 or u.Level IS NULL )
                            and u.Id in
                            (
                                {1}
                            )
                        )", sqlWhere, userSelectionSubquery);
                    sqlWhere = string.Format(@"{0}
                        or u.Id in
                        (
                            select mrr.TargetUserId
                            from [dbo].[ManualRoleRecord] mrr
                            where mrr.UserId = :userId and mrr.RoleId = 2 and mrr.TargetUserId is not null
                        )
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
                                        on mrr.UserId = :userId
                                    inner join Role
                                        on mrr.RoleId = 2
                            )
                        )
                        ", sqlWhere);

                    //sqlWhere += "u.ManagerId = :userId";
                    break;
                case UserRole.PersonnelManager:
                    sqlWhere += string.Format(" exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) and (u.RoleId & 2) = {0} ",(int)UserRole.Employee);
                    break;
                default:
                    break;
            }
            if((managerRole & UserRole.Employee) != UserRole.Employee)
                sqlWhere = GetDepartmentWhere(sqlWhere, departmentId);
            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by u.Name,u.Id ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                SetInt32("userId", managerId);

            return query.
            SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDtoWithDates))).
            List<IdNameDto>();
        }
        public IList<UserDto> GetUsersForManager(string userName,
            int managerId,UserRole managerRole, int? role,ref int currentPage,
            out int numberOfPages)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(User));
            if (!string.IsNullOrEmpty(userName))
                criteria.Add(Restrictions.InsensitiveLike("Name", "%"+userName+"%"));
            if(role.HasValue && (role.Value > 0))
                criteria.Add(Restrictions.Eq("Role.Id", role.Value));
            switch(managerRole)
            {
                case UserRole.Employee:
                    throw new ArgumentException("Список сотрудников нелоступен для сотрудника.");
                case UserRole.Manager:
                    criteria.Add(Restrictions.Eq("Manager.Id", managerId));
                    break;
                case UserRole.PersonnelManager:
                    criteria.Add(Restrictions.Eq("PersonnelManager.Id", managerId));
                    break;
                case UserRole.BudgetManager:
                    criteria.Add(Restrictions.Eq("Role.Id", (int)UserRole.Employee));
                    break;
                case UserRole.OutsourcingManager:
                    criteria.Add(Restrictions.Eq("Role.Id", (int)UserRole.Employee));
                    break;
                default:
                    break;
            }

            ICriteria countCriteria = (ICriteria)criteria.Clone();
            int rowCount = (int)countCriteria                            
            .SetProjection(Projections.RowCount())
            .UniqueResult();
            numberOfPages = Convert.ToInt32(Math.Ceiling((double) rowCount/PageSize));
            if (currentPage > numberOfPages)
                currentPage = numberOfPages;
            if (numberOfPages == 0)
            {
                currentPage = 1;
                return new List<UserDto>();
            }
            if (currentPage == 0)
                currentPage = 1;
            criteria.
                //AddOrder(new Order("LastName", true)).
                //AddOrder(new Order("FirstName", true)).
                AddOrder(new Order("Name", true));
            IEnumerable<User> userList = criteria
                .List<User>()
                .Skip((currentPage-1)*PageSize)
                .Take(PageSize);
            return userList.ToList().ConvertAll(x => new UserDto
                                                         {
                                                             Id = x.Id,
                                                             Code = x.Code,
                                                             Name = x.FullName,
                                                         });
        }
        public IList<User> GetUsersForAdmin(string userName,int role,
                        ref int currentPage, out int numberOfPages)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(User));
            if (!string.IsNullOrEmpty(userName))
                criteria.Add(Restrictions.InsensitiveLike("Name", "%" + userName + "%"));
            if (role > 0)
            {
                if (role == (int)UserRole.Employee)
                {
                    criteria.Add(Restrictions.Disjunction()
                                              .Add(Restrictions.Eq("RoleId", role))
                                              .Add(Restrictions.Eq("RoleId", role + (int)UserRole.Accountant))
                                              );
                }
                else
                {
                    if (role == (int) UserRole.Accountant)
                        role = (int) UserRole.Accountant + (int) UserRole.Employee;
                    criteria.Add(Restrictions.Eq("RoleId", role));
                }
            }
            ICriteria countCriteria = (ICriteria)criteria.Clone();
            int rowCount = (int)countCriteria
            .SetProjection(Projections.RowCount())
            .UniqueResult();
            numberOfPages = Convert.ToInt32(Math.Ceiling((double)rowCount / PageSize));
            if (currentPage > numberOfPages)
                currentPage = numberOfPages;
            if (numberOfPages == 0)
            {
                currentPage = 1;
                return new List<User>();
            }
            if (currentPage == 0)
                currentPage = 1;

            criteria.
                //AddOrder(new Order("LastName", true)).
                //AddOrder(new Order("FirstName", true)).
                AddOrder(new Order("Name", true));
            IEnumerable<User> userList = criteria
                .List<User>()
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize);
            return userList.ToList();
        }
        public IList<User> GetUsersForPersonnel(string userName,int personnelId,ref int currentPage, out int numberOfPages)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(User));
            if (!string.IsNullOrEmpty(userName))
                criteria.Add(Restrictions.InsensitiveLike("Name", "%" + userName + "%"));
            criteria.Add(Restrictions.Eq("PersonnelManager.Id", personnelId));
            criteria.Add(Restrictions.Eq("IsNew", true));
            ICriteria countCriteria = (ICriteria)criteria.Clone();
            int rowCount = (int)countCriteria
            .SetProjection(Projections.RowCount())
            .UniqueResult();
            numberOfPages = Convert.ToInt32(Math.Ceiling((double)rowCount / PageSize));
            if (currentPage > numberOfPages)
                currentPage = numberOfPages;
            if (numberOfPages == 0)
            {
                currentPage = 1;
                return new List<User>();
            }
            if (currentPage == 0)
                currentPage = 1;

            criteria.
                //AddOrder(new Order("LastName", true)).
                //AddOrder(new Order("FirstName", true)).
                AddOrder(new Order("Name", true));
            IEnumerable<User> userList = criteria
                .List<User>()
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize);
            return userList.ToList();
        }

        public IList<AcceptRequestDateDto> GetAcceptDatesForManager(int userId, UserRole managerRole,
            DateTime beginDate, DateTime endDate)
        {
            string sqlQuery = @"select 
            u.Id as UserId
            ,u.Name as UserName
            ,ard.DateAccept
            from dbo.Users u
            left join AcceptRequestDate ard on ard.UserId = u.Id and ard.DateAccept between :beginDate and :endDate";
            string sqlWhere = string.Format(" u.RoleId = {0} ", (int)UserRole.Manager);
            switch (managerRole)
            {
                case UserRole.Manager:
                    sqlWhere += " and u.Id = :userId";
                    break;
                case UserRole.PersonnelManager:
                    sqlQuery += " inner join UserToPersonnel up on u.Id = up.UserId ";
                    sqlWhere += " and up.PersonnelId = :userId ";
                    break;
                case UserRole.OutsourcingManager:
                    break;
                default:
                    throw new ArgumentException(string.Format("Invalid role {0} of user {1}",managerRole,userId)); 
            }
            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by u.Name,ard.DateAccept ";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("DateAccept", NHibernateUtil.DateTime);
            if ((managerRole & UserRole.OutsourcingManager) != UserRole.OutsourcingManager)
                query.SetInt32("userId", userId);
            query.SetDateTime("beginDate", beginDate);
            query.SetDateTime("endDate", endDate);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(AcceptRequestDateDto))).List<AcceptRequestDateDto>();
        }

        public IList<User> LoadForIdsList(ArrayList ids)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(User));
            criteria.Add(Restrictions.In("Id", ids));
            return criteria.List<User>();
        }
        public IList<IdNameDto> GetUserListForDeduction()
        {
            string sqlQuery = @"select 
                                u.id
                                ,u.Name + N' (' + isnull(d.Name,N'')+ N', ' + isnull(d2.Name,N'')+ N' )' as Name
                                from Users u
                                left join Department d on d.Id = u.DepartmentId
                                left join Department d2 on d.[Path] like d2.[Path]+N'%' and d2.ItemLevel = 3";
            string sqlWhere = string.Format(@" ((u.RoleId & {0}) > 0) OR ((u.RoleId & {1}) > 0) ", (int)UserRole.Employee,(int)UserRole.DismissedEmployee);// Уволенных показываем. убрано:  and ((u.DateRelease is null) or (u.DateRelease >= :releaseDate))
            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
                //AddScalar("DateAccept", NHibernateUtil.DateTime);
            //query.SetDateTime("releaseDate", DateTime.Today.AddMonths(-3));
            //query.SetDateTime("endDate", endDate);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
        public virtual IList<IdNameDto> GetUsersWithPurchaseBookReportCosts()
        {
            const string sqlQuery = @" select distinct u.Id,u.Name+N', '+d.Name+N', '+d3.Name as Name from Users u
                    inner join [dbo].[Department] d on d.Id = u.DepartmentId
                    inner join [dbo].[Department] d3 on d.Path like d3.Path+N'%' and d3.[ItemLevel] = 3
                    where exists
                    (select mr.id from [dbo].[MissionReportCost] mrc
                    inner join [dbo].[MissionReport] mr on mr.Id = mrc.ReportId
                    where mrc.IsCostFromPurchaseBook = 1
                    and mr.AccountantDateAccept is null
                    and mr.UserId = u.Id)
                    order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
		//protected static UserInstitutionLink FindInList(IList<UserInstitutionLink> list, int userId)
		//{
		//    foreach (UserInstitutionLink entity in list)
		//    {
		//        if (entity.User.Id == userId)
		//            return entity;
		//    }
		//    return null;
		//}

        public virtual IList<IdNameAddressDto> GetArchivistAddresses()
        {
            const string sqlQuery = @" select Id,Name,Address
                    from [dbo].[Users]
                    where Address is not null and Address != N''
                        and (RoleId & 8192) > 0
                    order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Address", NHibernateUtil.String);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameAddressDto))).List<IdNameAddressDto>();
        }
        public virtual IList<IdNameDto> GetEmployeesForCreateHelpServiceRequest(List<int> departments)
        {
            const string sqlQuery = @"select emp.Id,emp.Name from Users emp
                    inner join dbo.Department d on emp.DepartmentId = d.Id
                    inner join dbo.Department dm on d.Path like dm.Path+N'%'
                    where ((emp.RoleId & 2) > 0) 
                    and dm.Id in (:departmentIds)
                    and emp.IsActive = 1 
                    order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                SetParameterList("departmentIds", departments);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdNameDto))).List<IdNameDto>();
        }
    }
}

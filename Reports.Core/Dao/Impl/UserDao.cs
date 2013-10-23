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

        //public override void Save(User entity)
        //{
        //    Session.Merge(entity);/*SaveOrUpdateCopy(entity);*/
        //}

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
            return Session.CreateCriteria(typeof(User))
                            .Add( Expression.Sql("lower({alias}.Email) = lower(?)", email, NHibernateUtil.String ))
                            .List<User>();
        }
        public virtual IList<User> GetUsersWithRole(UserRole role)
        {
            return Session.CreateCriteria(typeof(User))
                  .Add(Restrictions.Eq("RoleId",(int)role))
//                  .Add(Restrictions.Eq("IsActive", true))
                  .Add(Restrictions.IsNull("DateRelease"))
                  .List<User>();
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

        //public virtual User FindByCustomerId(string masterCustomerId, string subCustomerId)
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof (User));
        //    criteria.Add(Restrictions.Eq("MasterCustomerId", masterCustomerId));
        //    criteria.Add(Restrictions.Eq("SubCustomerId", subCustomerId));
        //    return (User)criteria.UniqueResult();
        //}

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
                    sqlWhere += "u.ManagerId = :userId";
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
            if(departmentId != 0 && managerRole != UserRole.Employee)
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
            if(managerRole != UserRole.OutsourcingManager)
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
            if (departmentId != 0 && managerRole != UserRole.Employee)
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
            if (managerRole != UserRole.OutsourcingManager)
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
                    throw new ArgumentException("Список сотрудников нелоступен для сотрудника.");
                case UserRole.Manager:
                    sqlWhere += "u.ManagerId = :userId";
                    break;
                case UserRole.PersonnelManager:
                    sqlWhere += string.Format(" exists ( select * from UserToPersonnel up where up.PersonnelId = :userId and u.Id = up.UserId ) and u.RoleId = {0} ",(int)UserRole.Employee);
                    break;
                default:
                    break;
            }
            if(managerRole != UserRole.Employee)
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
            /*ICriteria criteria = Session.CreateCriteria(typeof(User));
            switch (managerRole)
            {
                case UserRole.Employee:
                    throw new ArgumentException("Список сотрудников нелоступен для сотрудника.");
                case UserRole.Manager:
                    criteria.Add(Restrictions.Eq("Manager.Id", managerId));
                    break;
                case UserRole.PersonnelManager:
                    criteria.Add(Restrictions.Eq("PersonnelManager.Id", managerId));
                    break;
                //case UserRole.BudgetManager:
                //    criteria.Add(Restrictions.Eq("Role.Id", (int)UserRole.Employee));
                //    break;
                //case UserRole.OutsourcingManager:
                //    criteria.Add(Restrictions.Eq("Role.Id", (int)UserRole.Employee));
                //    break;
                default:
                    break;
            }
            return criteria.List<User>().ToList().ConvertAll(x => new IdNameDto(x.Id, x.FullName)).OrderBy(x => x.Name).ToList();*/
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
                criteria.Add(Restrictions.Eq("Role.Id", role));
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
            if (managerRole != UserRole.OutsourcingManager)
                query.SetInt32("userId", userId);
            query.SetDateTime("beginDate", beginDate);
            query.SetDateTime("endDate", endDate);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(AcceptRequestDateDto))).List<AcceptRequestDateDto>();
        }


//        public ISet<Institution> GetLinkedInstitutions(int userId)
//        {
//            ISet<Institution> set = new HashedSet<Institution>();
//            set.AddAll(Session.CreateSQLQuery(
//                           "select institution.Id,Location,Name from institution " +
//                           "inner join UserInstitutionLink on UserInstitutionLink.InstitutionId = Institution.Id " +
//                           "where UserInstitutionLink.UserId = " + userId + " order by Name,Location ")
//                           .AddScalar("Id", NHibernateUtil.Int32)
//                           .AddScalar("Location", NHibernateUtil.String)
//                           .AddScalar("Name", NHibernateUtil.String).
////                           .AddScalar("ImageFileId", NHibernateUtil.Int32).
//                           SetResultTransformer(Transformers.AliasToBean(typeof (Institution))).List<Institution>());
//            return set;
//        }

        //public Iesi.Collections.Generic.ISet<IdNameDto> GetAllEntitiesForDictionary(string fieldName)
        //{
        //    IList<NameDto> list = 
        //    Session.CreateSQLQuery(@" select distinct(" + fieldName + ") as Name from [Users] ").
        //    AddScalar("Name", NHibernateUtil.String).
        //    SetResultTransformer(Transformers.AliasToBean(typeof (NameDto))).List<NameDto>();
        //    Iesi.Collections.Generic.ISet<IdNameDto> result = new HashedSet<IdNameDto>();
        //    int i = 0;
        //    result.Add(new IdNameDto(i++,string.Empty)); 
        //    if (list.Count <= 0)
        //        return result;
        //    foreach (NameDto name in list)
        //    {
        //        if (!string.IsNullOrEmpty(name.Name))
        //        {
        //            IdNameDto dto = new IdNameDto(i++, name.Name);
        //            result.Add(dto);
        //        }
        //    }
        //    return result;	
        //}

        //public IList<UsersListItemDto> FindByFilter(UserListFilter filter, out int count)
        //{
        //    ICriteria criteria = Session.CreateCriteria(typeof(User));
        //    ICriterion criterion = Expression.Sql("");

        //    if (!string.IsNullOrEmpty(filter.FirstName))
        //        criterion = Restrictions.And(criterion, new LikeExpression("FirstName", filter.FirstName, MatchMode.Start));
        //    if (!string.IsNullOrEmpty(filter.MiddleName))
        //        criterion = Restrictions.And(criterion, new LikeExpression("MiddleName", filter.MiddleName, MatchMode.Start));
        //    if (!string.IsNullOrEmpty(filter.LastName))
        //        criterion = Restrictions.And(criterion, new LikeExpression("LastName", filter.LastName, MatchMode.Start));
        //    //if (filter.Status != null)
        //    //    criterion = Expression.And(criterion, new EqExpression("IsActive", filter.Status));

        //    if (!string.IsNullOrEmpty(filter.Branch))
        //        criterion = Restrictions.And(criterion, Restrictions.Eq("Branch", filter.Branch));
        //    if (!string.IsNullOrEmpty(filter.Department))
        //        criterion = Restrictions.And(criterion, Restrictions.Eq("Department", filter.Department));
        //    if (!string.IsNullOrEmpty(filter.Position))
        //        criterion = Restrictions.And(criterion, Restrictions.Eq("Position", filter.Position));

        //    ICriteria criteria2 = Session.CreateCriteria(typeof(User));
        //    criteria2.Add(criterion);
        //    criteria2.SetProjection(Projections.RowCount());
        //    count = (int)criteria2.UniqueResult();
        //    if (filter.FirstResult > count) filter.FirstResult = 0;
        //    criteria.Add(criterion);
        //    if (filter.MaxResults != -1)
        //        criteria.SetMaxResults(filter.MaxResults);
        //    if (filter.FirstResult != -1)
        //        criteria.SetFirstResult(filter.FirstResult);
        //    if (!string.IsNullOrEmpty(filter.SortExpression))
        //    {
        //        criteria.AddOrder(new Order(filter.SortExpression, filter.SortAscending));
        //        if (filter.SortExpression.CompareTo(LastNameSortFieldName) == 0)
        //        {
        //            criteria.AddOrder(new Order(FirstNameSortFieldName, filter.SortAscending));
        //            criteria.AddOrder(new Order(MiddleNameSortFieldName, filter.SortAscending));
        //        }
        //    }
        //    IList<User> list = criteria.List<User>();
        //    IList<UsersListItemDto> result = new List<UsersListItemDto>();
        //    if (list.Count <= 0)
        //        return result;
        //    foreach (User user in list)
        //    {
        //        UsersListItemDto dto = new UsersListItemDto(user);
        //        result.Add(dto);
        //    }
        //    //IList<ItemUsedDto> usedList = CoreUtils.GetFKsForEntitiesList(Session, FKExistsViewName, list);
        //    //CoreUtils.SetUsedEntityFlag(list, usedList);

        //    return result;
        //}
		//public IList<User> GetUsersInRole(string roleName)
		//{
		//    IList<User> list = new List<User>();
		//    ICriterion experssion = null;
		//    switch (roleName)
		//    {
		//        case SystemRoleConstants.EditingUser:
		//            experssion = Expression.Eq("IsEditingUser", true);
		//            break;
		//        case SystemRoleConstants.UserAdministrator:
		//            experssion = Expression.Eq("IsUserAdministrator", true);
		//            break;
		//        case SystemRoleConstants.ContentAdministrator:
		//            experssion = Expression.Eq("IsContentAdministrator", true);
		//            break;
		//        case SystemRoleConstants.SuperAdministrator:
		//            experssion = Expression.Eq("IsSuperAdministrator", true);
		//            break;
		//        case SystemRoleConstants.NotApproved:
		//            experssion = Expression.Eq("IsApproved", false);
		//            break;
		//        case SystemRoleConstants.Inactive:
		//            experssion = Expression.Eq("IsActive", false);
		//            break;
		//        default:
		//            break;
		//    }
		//    if (experssion != null)
		//    {
		//        list = Session.CreateCriteria(typeof(User))
		//            .Add(experssion)
		//            .List<User>();
		//    }
		//    return list;
		//}

		//public IList<User> GetUsersInDcmRole(ContentManagementRole role, int baseCaseId)
		//{
		//    StringBuilder queryStr = new StringBuilder();
		//    IList<User> list = new List<User>();


		//    queryStr.Append("select DISTINCT pm.UserId as Id from GraphEdgePathLink as gepl ");
		//    queryStr.Append("inner join GraphPath as gp on gepl.GraphPathId = gp.Id and gp.BaseCaseId = ");
		//    queryStr.Append(baseCaseId.ToString());
		//    queryStr.Append(" left outer join Permission as pm on pm.OwnerId = gepl.SecuredEntityId ");
		//    queryStr.Append("and (pm.Role&:role) <> 0 ");
		//    queryStr.Append("where pm.UserId is not null");
		//    IQuery queryInt = Session.CreateSQLQuery(queryStr.ToString())
		//        .AddScalar("Id", NHibernateUtil.Int32);
		//    queryInt.SetParameter("role", role);
		//    IList<int> lstInt = queryInt.List<int>();
		//    if (lstInt.Count > 0)
		//    {
		//        int last = lstInt[lstInt.Count - 1];
            

		//        queryStr.Remove(0, queryStr.Length);

		//        queryStr.Append("from User u where u.Id in ( ");
		//        foreach (int i in lstInt)
		//        {
		//            queryStr.Append(i.ToString());
		//            if(i != last)
		//            {
		//                queryStr.Append(", ");
		//            }
		//        }
		//        queryStr.Append(")");

		//        IQuery query = Session.CreateQuery(queryStr.ToString());
		//        list = query.List<User>();
		//    }

		//    return list;
		//}

//        public ContentManagementRole GetUserRolesForCase(int userId, int baseCaseId)
//        {
//            StringBuilder queryStr = new StringBuilder();

//            ContentManagementRole resRole = ContentManagementRole.None;

//            queryStr.Append("select pm.Role as CMRole from GraphEdgePathLink as gepl ");
//            queryStr.Append("inner join GraphPath as gp on gepl.GraphPathId = gp.Id and gp.BaseCaseId = ");
//            queryStr.Append(baseCaseId.ToString());
//            queryStr.Append(" left outer join Permission as pm on pm.OwnerId = gepl.SecuredEntityId ");
//            queryStr.Append("and (pm.Role&:role) <> 0 ");
//            queryStr.Append("where pm.UserId = :userId");
//            IQuery queryInt = Session.CreateSQLQuery(queryStr.ToString())
//                .AddScalar("CMRole", NHibernateUtil.Int32);
//            queryInt.SetParameter("userId", userId);
//            queryInt.SetParameter("role", ContentManagementRole.All);
//            IList<int> lstInt = queryInt.List<int>();
			
//            foreach (int role in lstInt)
//            {
//                resRole = resRole | ((ContentManagementRole)role);
//            }

//            return resRole;
//        }

//        public IList<User> GetUsersInDcmRoles(ContentManagementRole roles,
//            int maxResults,int firstResult,
//            string sortExpression,bool sortAscending, out int count) // role is bit mask of DCM roles
//        {
//            ICriteria criteria = Session.CreateCriteria(typeof(User));
////            ICriterion criterion = Expression.Sql("");
//            Array values = Enum.GetValues(typeof(ContentManagementRole));
//            Junction jun = Expression.Disjunction();
//            foreach (ContentManagementRole role in values)
//            {
//                if((role != ContentManagementRole.None) && (role != ContentManagementRole.All))
//                {
//                    if ((roles & role) == role)
//                        jun.Add(Expression.Sql("( DefaultCMRole&" + ((int)role + "=" + ((int)role)+" )")));//criterion, new SqlExpression("DefaultCMRole" /*+ ((int)role)*/, role));
//                }
//            }
//            ICriteria criteria2 = Session.CreateCriteria(typeof(User));
//            criteria2.Add(jun);
//            criteria2.SetProjection(Projections.RowCount());
//            count = (int)criteria2.UniqueResult();
//            if (firstResult > count) firstResult = 0;
//            criteria.Add(jun);
//            if (maxResults != -1)
//                criteria.SetMaxResults(maxResults);
//            if (firstResult != -1)
//                criteria.SetFirstResult(firstResult);
//            if (!string.IsNullOrEmpty(sortExpression))
//                criteria.AddOrder(new Order(sortExpression, sortAscending));
//            return criteria.List<User>();
//        }

//        public string[] GetUsersLoginInRole(string roleName)
//        {
//            List<User> list = (List<User>)GetUsersInRole(roleName);
//            List<string> listLogin = list.ConvertAll<string>(delegate(User user) { return user.Login; });
//            string[] users = listLogin.ToArray();
//            return users;
//        }
//        public IList<SelectUserDto> LoadDtoListForInstitution(IList<User> list,int institutionId)
//        {
//            IList<SelectUserDto> resultList = new List<SelectUserDto>();
//            if(list.Count <= 0)
//                return resultList;
//            //ArrayList userList = new ArrayList();
//            //foreach (User user in list)
//            //{
//            //    if (!userList.Contains(user.Id))
//            //        userList.Add(user.Id);
//            //}
//            ICriteria criteria = Session.CreateCriteria(typeof(UserInstitutionLink));
//            criteria.Add(Expression.In("User.Id", CoreUtils.CreateArrayList(list)));
//            criteria.Add(Expression.Eq("Institution.Id", institutionId));
//            IList<UserInstitutionLink> linkList = criteria.List<UserInstitutionLink>();
//            if (linkList.Count > 0)
//            {
//                IList<ItemUsedDto> usedList = CoreUtils.GetFKsForEntitiesList(Session, UserInstitutionLinkDao.FKExistsViewName, linkList);
//                CoreUtils.SetUsedEntityFlag(linkList, usedList);
//            }
//            foreach (User user in list)
//            {
//                SelectUserDto dto = new SelectUserDto(user);
//                UserInstitutionLink link = FindInList(linkList, user.Id);
//                if(link != null)
//                {
//                    dto.IsSelected = true;
//                    dto.IsUsed = link.IsUsed; 
//                }
//                else
//                {
//                    dto.IsSelected = false;
//                    dto.IsUsed = false; 
//                }
//                resultList.Add(dto); 
//            }
//            return resultList; 
//        }
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
            string sqlWhere = string.Format(@" ((u.RoleId & {0}) > 0) 
            and ((u.DateRelease is null) or (u.DateRelease >= :releaseDate))", (int)UserRole.Employee);
            sqlQuery += @" where " + sqlWhere;
            sqlQuery += @" order by Name";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String);
                //AddScalar("DateAccept", NHibernateUtil.DateTime);
            query.SetDateTime("releaseDate", DateTime.Today.AddMonths(-3));
            //query.SetDateTime("endDate", endDate);
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


    }
}

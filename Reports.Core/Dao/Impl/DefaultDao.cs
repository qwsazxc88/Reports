using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using log4net;
using NHibernate;
using System.Collections;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;
using System.Linq.Expressions;
namespace Reports.Core.Dao.Impl
{
    //[Transient]
    public class DefaultDao<TEntity, TIdentifier> : IDao<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>
    {
        private ISessionManager _sessionManager;
        protected static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        

        public DefaultDao(ISessionManager sessionManager)
        {
            Validate.NotNull(sessionManager, "sessionManager");
            _sessionManager = sessionManager;
        }

        protected ISession Session
        {
            get { return _sessionManager.CurrentSession; }
        }

        protected ICriteria CreateCriteria ()
        {
            return Session.CreateCriteria(typeof(TEntity));
        }

        public virtual TEntity Load(TIdentifier id)
        {
            return Session.Load<TEntity>(id);
        }

        public virtual TEntity Get(TIdentifier id)
        {
            return Session.Get<TEntity>(id);
        }

		public IList<TEntity> Load(string queryStr, Hashtable parameters)
		{
			IQuery query = Session.CreateQuery(queryStr);
			if (parameters != null)
			{
				foreach (string param in parameters.Keys)
				{
					object paramValue = parameters[param];
				    ICollection iColParamValue = (ICollection) paramValue; 
					if (iColParamValue != null)//(paramValue is ICollection)
						query.SetParameterList(param, iColParamValue/*(ICollection)paramValue*/);
					else
						query.SetParameter(param, paramValue);
				}
			}
			return query.List<TEntity>();
		}

        public virtual void Save(TEntity entity)
        {
            Session.Save(entity);
        }
        public virtual void SaveAndFlush(TEntity entity)
        {
            Session.Save(entity);
            Session.Flush();
        }
        public virtual TEntity SaveFlush(TEntity entity)
        {
            TEntity ret = (TEntity)Session.Save(entity);
            Session.Flush();
            return ret;
        }
        public virtual TEntity MergeAndFlush(TEntity entity)
        {
            TEntity result = (TEntity)Session.Merge(entity);
            Session.Flush();
            return result;
        }
        public virtual TEntity Merge(TEntity entity)
        {
            TEntity result = (TEntity)Session.Merge(entity);
            return result;
        }

        public virtual void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }
        public virtual void DeleteAndFlush(TEntity entity)
        {
            Session.Delete(entity);
            Session.Flush();
        }
        public virtual void Delete(TIdentifier entityId)
        {
            TEntity entity = Session.Load<TEntity>(entityId);
            Session.Delete(entity);
        }
        public virtual void DeleteAndFlush(TIdentifier entityId)
        {
            Delete(entityId);
            Session.Flush();
        }
        public virtual void Flush()
        {
            Session.Flush();
        }

        public virtual void SaveOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public virtual TEntity FindById(TIdentifier id)
        {
            return Session.Get<TEntity>(id);
        }

        public virtual IList<TEntity> FindAll()
        {
            return CreateCriteria().List<TEntity>();
        }
        public virtual void RollbackTran()
        {
            ITransaction tx = Session.Transaction;
            if(tx != null && tx.IsActive)
                tx.Rollback();
        }
        public virtual void BeginTran()
        {
            Session.BeginTransaction();
            //ITransaction tx = Session.Transaction;
            //if (tx != null && tx.IsActive)
            //    tx.Rollback();
        }
        public virtual void CommitTran()
        {
             ITransaction tx = Session.Transaction;
             if (tx != null && tx.IsActive)
             {
                 try
                 {
                     tx.Commit();
                     Log.DebugFormat("Commit transaction per session {0}, thread {1} success.",
                                     Session.GetHashCode(),
                                     Thread.CurrentThread.ManagedThreadId);
                 }
                 catch (Exception ex)
                 {
                     Log.Error("Error on tx.Commit()", ex);
                     try
                     {
                         tx.Rollback();
                     }
                     catch (Exception rollEx)
                     {

                         Log.Error("Error on tx.Rollback()", rollEx);
                     }
                 }
             }
        }
    }

    public class DefaultDao<TEntity> : DefaultDao<TEntity, int>, IDao<TEntity>
        
        where TEntity : class, IEntity<int>
    {
        #region Constants
        public const string StrInvalidManagerLevel = "Неверный уровень руководителя (id {0}) {1} в базе даннных.";
        
        protected const string ObjectPropertyFormat = "{0}.{1}";
        protected const string DeleteRequestText = "Заявка отклонена";

        protected const string RequestStatusStandardSelector = @"
            case when v.DeleteDate is not null then '{0}'
                when v.SendTo1C is not null then 'Выгружено в 1с' 
                when v.PersonnelManagerDateAccept is not null 
                        and v.ManagerDateAccept is not null 
                        and v.UserDateAccept is not null 
                        then 'Согласовано кадровиком'
                when  v.PersonnelManagerDateAccept is null 
                        and v.ManagerDateAccept is not null 
                        and v.UserDateAccept is not null 
                        then 'Отправлено кадровику'    
                when  -- v.PersonnelManagerDateAccept is null and 
                        v.ManagerDateAccept is null 
                        and v.UserDateAccept is not null 
                        then 'Отправлено руководителю'    
                when  v.PersonnelManagerDateAccept is null 
                        and v.ManagerDateAccept is null 
                        and v.UserDateAccept is null 
                        then 'Черновик сотрудника'    
                else ''
            end as RequestStatus";

        protected const string sqlUManagerAccountJoin = @"left join [dbo].[Users] uManagerAccount
                                                            on (uManagerAccount.RoleId & 4) > 0
                                                                and u.Email = uManagerAccount.Email
                                                                and uManagerAccount.Login like u.Login+N'R'
                                                                and uManagerAccount.IsActive = 1";

        protected const string sqlCurrentUserJoin = @"inner join dbo.Users currentUser
                                                        on currentUser.Id = :userId";
        protected const string sqlDepartamentStandartSelector = @"
                                dep.Name as Dep7Name,
                                dep3.Name as Dep3Name,        
                                ";
        protected const string sqlPositionStandartSelector = @"
                                up.Name as Position,
                                ";
        protected const string sqlDepartamentJoin = @"
                                inner join dbo.Department dep on u.DepartmentId = dep.Id                                
                                LEFT JOIN dbo.Department dep3 ON dep.[Path] like dep3.[Path]+N'%' and dep3.ItemLevel = 3 
                                ";
        protected const string sqlPositionJoin = @"
                                left join [dbo].[Position]  up on up.Id = u.PositionId
                                ";
        protected const string sqlSelectForList =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                (
								select top(1) manager.name 
								from Users manager 
								inner JOIN Department userd on userd.Id=u.DepartmentId
								INNER JOIN Department d on manager.DepartmentId=d.Id and userd.Path like d.Path+'%'
								where manager.IsActive=1 and manager.RoleId&4>0 and u.Email!=manager.Email order by manager.Level desc, manager.IsMainManager desc 
								) as ManagerName,
                                '{3}' as Name,
                                {2} as Date,  
                                {5} as BeginDate,  
                                {6} as EndDate,  
                                v.Number as Number,
                                u.Name as UserName,
                                t.Name as RequestType," + 
                                sqlPositionStandartSelector +
                                sqlDepartamentStandartSelector+
                                RequestStatusStandardSelector +
                                @"
                                from {4} v
                                left join {1} t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                " 
                                + sqlDepartamentJoin
                                + sqlPositionJoin
                                + sqlUManagerAccountJoin + @"
                                " + sqlCurrentUserJoin;
        protected const string sqlSelectForListSicklist =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                '{3}' as Name,
                                {2} as Date,  
                                {5} as BeginDate,  
                                {6} as EndDate,  
                                v.Number as Number,
                                (
								    select top(1) manager.name 
								    from Users manager 
								    inner JOIN Department userd on userd.Id=u.DepartmentId
								    INNER JOIN Department d on manager.DepartmentId=d.Id and userd.Path like d.Path+'%'
								    where manager.IsActive=1 and manager.RoleId&4>0 and u.Email!=manager.Email order by manager.Level desc, manager.IsMainManager desc 
								) as ManagerName,
                                v.SicklistNumber,
                                u.Name as UserName,
                                t.Name as RequestType," + 
                                sqlDepartamentStandartSelector+
                                sqlPositionStandartSelector+
                                RequestStatusStandardSelector + @",
                                u.ExperienceIn1C as UserExperienceIn1C,
                                v.IsOriginalReceived as IsOriginalReceived
                                from {4} v
                                left join {1} t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                " 
                                + sqlDepartamentJoin
                                +sqlPositionJoin
                                + sqlUManagerAccountJoin + @"
                                " + sqlCurrentUserJoin;
        protected const string sqlSelectForListClearanceChecklist =
                                @"select distinct v.Id as Id,
                                u.Id as UserId,
                                '{1}' as Name,
                                {2} as Date,
                                {4} as BeginDate,  
                                {5} as EndDate,
                                v.Number as Number,
                                u.Name as UserName,
                                case when v.DeleteDate is not null then '{0}'
                                     when v.SendTo1C is not null then 'Выгружено в 1с' 
                                    else ''
                                end as RequestStatus,
                                {6} as RequestType,
                                {7} as RegistryNumber,
                                {8} as PersonalIncomeTax,
                                {9} as OKTMO
                                from {3} v
                                inner join [dbo].[Users] u on u.Id = v.UserId";
        protected const string sqlSelectForListChildVacation =
                               @"select v.Id as Id,
                                u.Id as UserId,
                                (
								select top(1) manager.name 
								from Users manager 
								inner JOIN Department userd on userd.Id=u.DepartmentId
								INNER JOIN Department d on manager.DepartmentId=d.Id and userd.Path like d.Path+'%'
								where manager.IsActive=1 and manager.RoleId&4>0 and u.Email!=manager.Email order by manager.Level desc, manager.IsMainManager desc 
								) as ManagerName,
                                N'{3}' as Name,
                                {2} as Date,  
                                {5} as BeginDate,  
                                {6} as EndDate,  
                                v.Number as Number,
                                u.Name as UserName,
                                N'Отпуск по уходу за ребенком'  as RequestType," +
                                sqlPositionStandartSelector+
                                sqlDepartamentStandartSelector+
                                RequestStatusStandardSelector + @",
                                v.IsOriginalReceived as IsOriginalReceived
                                from {4} v
                                -- left join {1} t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                " 
                                + sqlDepartamentJoin
                                + sqlPositionJoin
                                + sqlUManagerAccountJoin + @"
                                " + sqlCurrentUserJoin;
        protected const string sqlSelectForListVacation =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                (
								    select top(1) manager.name 
								    from Users manager 
								    inner JOIN Department userd on userd.Id=isnull(ur.DepartmentId, u.DepartmentId)
								    INNER JOIN Department d on manager.DepartmentId=d.Id and userd.Path like d.Path+'%'
								    where manager.IsActive=1 and manager.RoleId&4>0 and u.Email!=manager.Email 
                                          and manager.level < case when ur.id is not null and isnull(ur.IsMainManager, 0) = 1 
                                                                   then ur.level 
                                                                   when ur.id is not null and isnull(ur.IsMainManager, 0) = 0 then ur.level + 6 else 7 end
                                    order by manager.Level desc, manager.IsMainManager desc 
								) as ManagerName,
                                '{3}' as Name,
                                {2} as Date,  
                                {5} as BeginDate,  
                                {6} as EndDate,  
                                v.Number as Number,
                                u.Name as UserName,
                                t.Name as RequestType," +
                                sqlPositionStandartSelector+
                                sqlDepartamentStandartSelector+
                                RequestStatusStandardSelector + @",
                                v.IsOriginalReceived as IsOriginalReceived
                                from {4} v
                                left join {1} t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                " 
                                +sqlDepartamentJoin
                                +sqlPositionJoin
                                + sqlUManagerAccountJoin + @"
                                " + sqlCurrentUserJoin + @"
                                LEFT JOIN Users as ur ON ur.Login = u.Login + N'R'";//у сотрудников-руководителей не правильные рукоовдители в реестре (если нужно отменить, то нужно еще исправить условия в подзапросе выше)
        protected const string sqlSelectForListDismissal =
                                @"select v.Id as Id,
                                u.Id as UserId,
                                '{3}' as Name,
                                {2} as Date,  
                                {5} as BeginDate,  
                                {6} as EndDate,  
                                v.Number as Number,
                                u.Name as UserName,
                                t.Name as RequestType," + 
                                sqlPositionStandartSelector+
                                sqlDepartamentStandartSelector+
                                RequestStatusStandardSelector + @",
                                v.IsOriginalReceived as IsOriginalReceived,
                                v.IsPersonnelFileSentToArchive as IsPersonnelFileSentToArchive
                                from {4} v
                                left join {1} t on v.TypeId = t.Id
                                inner join [dbo].[Users] u on u.Id = v.UserId
                                " 
                                + sqlDepartamentJoin
                                +sqlPositionJoin
                                + sqlUManagerAccountJoin + @"
                                " + sqlCurrentUserJoin;
        #endregion
        public DefaultDao(ISessionManager sessionManager) : base(sessionManager)
        {
        }
        public IList<TEntity> Find(Func<TEntity, bool> predicate)
        {            
            var result= Session.Query<TEntity>().Where(predicate).ToList();
            return (result != null && result.Any()) ? result.ToList() : new List<TEntity>();
        }
        public IList<TEntity> QueryExpression(Expression<Func<TEntity, bool>> predicate)
        {           
            var result = Session.QueryOver<TEntity>().Where(predicate).List();
            return result;
        }
        public void Update(Func<TEntity, bool> predicate, Action<TEntity> action)
        {            
            var result = Session.Query<TEntity>().Where(predicate);
            if (result != null )
            {
                foreach (var el in result)
                {
                    action(el);
                    SaveAndFlush(el);
                }
            }            
        }
        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
        }

        protected IUserDao userDao;
        public IUserDao UserDao
        {
            get { return Validate.Dependency(userDao); }
            set { userDao = value; }
        }

        public TEntity Load(string id)
        {
            int entityId;
            if (!int.TryParse(id, NumberStyles.Integer, CultureInfo.InvariantCulture, out entityId))
                throw new ArgumentException(
                    string.Format(Resources.Culture,
                    Resources.DefaultDao_CannotLoadByInvalidStringId,
                    typeof(TEntity), id));

            return Load(entityId);
        }

        public TEntity FindById(string id)
        {
            int entityId;
            if (int.TryParse(id,NumberStyles.Integer,CultureInfo.InvariantCulture, out entityId))
                return FindById(entityId);
            return default(TEntity);
        }
        public IList<TEntity> LoadAll()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TEntity));
            //criteria.AddOrder(new Order("Name", true));
            return criteria.List<TEntity>();
        }
        public virtual IList<TEntity> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TEntity));
            criteria.AddOrder(new Order("Name", true));
            return criteria.List<TEntity>();
        }

        // Where-блок определения прав при прямой привязке руководителя к сотруднику (deprecated)
        public virtual string GetWhereForUserRole(UserRole role,int userId)
        {
            switch (role)
            {
                #region Employees
                
                case UserRole.Employee:
                    return string.Format(" u.Id = {0} ", userId);
                
                #endregion

                #region Managers
                
                case UserRole.Manager:
                    return string.Format(" u.ManagerId = {0} ", userId);
                
                #endregion

                #region PersonnelManagers
                
                case UserRole.PersonnelManager:
                    int? superPersonnelId = ConfigurationService.SuperPersonnelId;
                    if (superPersonnelId.HasValue && superPersonnelId.Value == userId)
                        return string.Empty;
                    return string.Format(" exists ( select * from UserToPersonnel up where up.PersonnelId = {0} and u.Id = up.UserId ) ", userId);
                
                #endregion

                #region Inspectors
                
                case UserRole.Inspector:
                    return string.Format(" exists ( select * from InspectorToUser iu where iu.InspectorId = {0} and u.Id = iu.UserId ) ", userId);
                
                #endregion

                #region Chiefs

                case UserRole.Chief:
                    return string.Format(" exists ( select * from ChiefToUser cu where cu.ChiefId = {0} and u.Id = cu.UserId ) ", userId);
                
                #endregion
                #region Estimator
                case UserRole.Estimator:
                    return String.Empty;
                #endregion
                #region OutsourcingManagers

                case UserRole.OutsourcingManager:
                    return string.Empty;

                #endregion

                #region ConsultantPersonnel
                case UserRole.ConsultantPersonnel:
                    return string.Empty;
                #endregion

                #region ConsultantOutsorsingManager
                /*case UserRole.ConsultantOutsorsingManager:
                    return string.Empty;*/
                #endregion

                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}",role));
            }
        }
        
        public virtual string GetWhereForUserRole(UserRole role, int userId, ref string sqlQuery)
        {
            switch (role)
            {
                #region Employees
                case UserRole.Employee:
                    // sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Format(" u.Id = {0} ", userId);
                #endregion

                #region DismissedEmployee:
                case UserRole.DismissedEmployee:
                    // sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Format(" u.Id = {0} ", userId);
                #endregion

                #region Managers
                case UserRole.Manager:
                    User currentUser = UserDao.Load(userId);
                    if (currentUser == null)
                        throw new ArgumentException(string.Format("Не могу загрузить пользователя {0} из базы даннных", userId));

                    string sqlQueryPart = string.Empty;
                    string sqlFlag = string.Empty;

                    switch (currentUser.Level)
                    {
                        case 2:                            
                        case 3:

                            sqlQueryPart += " -1";
                            sqlFlag = @"case when v.UserDateAccept is not null 
                                and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;

                        case 4:
                        case 5:
                        case 6:
                            // Выборка замов и руководителей нижележащих уровней по ветке для применения автоматических прав уровней 4-6
                            sqlQueryPart += @" 
                            select distinct managerEmployeeAccount.Id from dbo.Users managerEmployeeAccount
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

                            sqlFlag = @"case when v.UserDateAccept is not null 
                                and  v.ManagerDateAccept is null then 1 else 0 end as Flag";
                            break;
                        default:
                            throw new ArgumentException(string.Format(StrInvalidManagerLevel, userId, currentUser.Level));
                    }

                    sqlQueryPart = string.Format(sqlQueryPart, userId);
                    sqlQueryPart = string.Format(@"u.Id in ( {0} )", sqlQueryPart);

                    // Автороль должна действовать только для уровней ниже третьего
                    sqlQueryPart = string.Format(" ((u.Level>3 or u.Level IS NULL) and {0} ) ", sqlQueryPart);
                    // Ручные привязки человек-человек и человек-подразделение из ManualRoleRecord
                    sqlQueryPart += string.Format(@"
                        or u.Id in (select mrr.TargetUserId from [dbo].[ManualRoleRecord] mrr where mrr.UserId = {0} and mrr.RoleId = 2)", userId);
                    sqlQueryPart += string.Format(@"
                        or 
                        (
                            ((u.RoleId & 2) > 0 or (u.RoleId & 2097152) > 0)
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
                                        on mrr.RoleId = 2
                            )
                        )
                        ", userId);
                    sqlQueryPart = string.Format(@"({0})", sqlQueryPart);
                    sqlQuery = string.Format(sqlQuery, sqlFlag, string.Empty);
                    return sqlQueryPart;
                #endregion                

                #region PersonnelManagers

                case UserRole.PersonnelManager:
                    int? superPersonnelId = ConfigurationService.SuperPersonnelId;
                    if (superPersonnelId.HasValue && superPersonnelId.Value == userId)
                        return string.Empty;
                    return string.Format(" exists ( select * from UserToPersonnel up where up.PersonnelId = {0} and u.Id = up.UserId ) ", userId);

                #endregion

                #region Inspectors

                case UserRole.Inspector:
                    return string.Format(" exists ( select * from InspectorToUser iu where iu.InspectorId = {0} and u.Id = iu.UserId ) ", userId);

                #endregion

                #region Chiefs

                case UserRole.Chief:
                    return string.Format(" exists ( select * from ChiefToUser cu where cu.ChiefId = {0} and u.Id = cu.UserId ) ", userId);

                #endregion
                #region Estimator
                    case UserRole.Estimator:
                    //sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Empty;
                #endregion
                #region OutsourcingManagers

                case UserRole.OutsourcingManager:
                    sqlQuery = string.Format(sqlQuery, @" 0 as Flag", string.Empty);
                    return string.Empty;

                #endregion

                #region ConsultantPersonnel
                case UserRole.ConsultantPersonnel:
                    return string.Empty;
                #endregion

                case UserRole.ConsultantOutsourcing:
                    return string.Empty;

                #region ConsultantOutsorsingManager
                /*case UserRole.ConsultantOutsorsingManager:
                    return string.Empty;*/
                #endregion

                #region -Deleted (Directors, Accountants, Secretary, Findep)

                /*
                case UserRole.Director:
                    const string sqlFlagD =
                        @"case when
                          (
                            (
                              v.UserDateAccept is not null
                              and  v.ManagerDateAccept is not null
                              and  v.[ChiefDateAccept] is null
                              and  v.[NeedToAcceptByChief] = 1
                            )
                            or
                            (
                              v.UserDateAccept is not null
                              and  v.ManagerDateAccept is null
                              and v.[NeedToAcceptByChiefAsManager] = 1
                            )
                          )
                          then 1 else 0 end as Flag";
                    sqlQuery = string.Format(sqlQuery, sqlFlagD, string.Empty);
                    return @" ((v.[NeedToAcceptByChief] = 1) or (v.[NeedToAcceptByChiefAsManager] = 1) or (ao.NeedToAcceptByChief = 1))  ";
                
                // case UserRole.Accountant:
                // case UserRole.Secretary:
                // case UserRole.Findep:*/

                #endregion

                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}", role));
            }
        }

        public virtual string GetDatesWhere(string whereString, DateTime? beginDate,
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
        public virtual void AddDatesToQuery( IQuery query,DateTime? beginDate,
            DateTime? endDate,string userName)
        {
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1));
            if (!string.IsNullOrEmpty(userName))
                query.SetString("userName", "%"+userName.ToLower()+"%");
        }

        public virtual string GetPositionWhere(string whereString, int positionId)
        {
            if (positionId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format("u.[PositionId] = {0} ",positionId);
            }
            return whereString;
        }
        public virtual string GetTypeWhere(string whereString, int typeId)
        {
            if (typeId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format("v.[TypeId] = {0} ",typeId);
            }
            return whereString;
        }
        public virtual string GetDocumentNumberWhere(string whereString, string docNumber)
        {
            if (!string.IsNullOrEmpty(docNumber))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format("v.Number = {0} ", docNumber);
            }
            return whereString;
        }
        public virtual string GetUserNameWhere(string whereString, string userName)
        {
            if(!string.IsNullOrEmpty(userName))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += "LOWER(u.[Name]) like :userName";
            }
            return whereString;
        }
        public virtual string GetWhereForOnlyUser(string whereString, int userId)
        {
            if (whereString.Length > 0)
                    whereString += @" and ";
            whereString += String.Format("u.Id = {0}", userId);
            
            return whereString;
        }
        public virtual string GetNumberWhere(string whereString, string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += "cast(v.Number as nvarchar(10)) = :number";
            }
            return whereString;
        }
        public virtual string GetDismDateWhere(string whereString, string DismDate)
        {
            
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format(@"Dism.SendTo1C is null or Dism.SendTo1C >'{0}' "
                    , DismDate);
            
            return whereString;
        }
        public virtual string GetDepartmentWhere(string whereString, int departmentId)
        {
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format(@"exists 
                    (select d1.ID from dbo.Department d
                     inner join dbo.Department d1 on d1.Path like d.Path +'%'
                     and u.DepartmentID = d1.ID --and d1.ItemLevel = 7 
                     and d.Id = {0})"
                    , departmentId);
            }
            return whereString;
        }
        public virtual string GetStatusWhere(string whereString, int statusId)
        {
            if (statusId != 0)
            {
                string statusWhere;
                switch (statusId)
                {
                    case 1:
                        statusWhere =
                            @"UserDateAccept is null and ManagerDateAccept is null and PersonnelManagerDateAccept is null and SendTo1C is null";
                        break;
                    case 2:
                        statusWhere = @"UserDateAccept is not null";
                        break;
                    case 3:
                        statusWhere = @"UserDateAccept is null";
                        break;
                    case 4:
                        statusWhere = @"ManagerDateAccept is not null";
                        break;
                    case 5:
                        statusWhere = @"ManagerDateAccept is null";
                        break;
                    case 6:
                        statusWhere = @"PersonnelManagerDateAccept is not null";
                        break;
                    case 7:
                        statusWhere = @"PersonnelManagerDateAccept is null";
                        break;
                    case 8:
                        statusWhere =
                            @"UserDateAccept is not null and ManagerDateAccept is not null and PersonnelManagerDateAccept is not null";
                        break;
                    case 9:
                        statusWhere = @"SendTo1C is not null";
                        break;
                    case 10:
                        statusWhere = @"[DeleteDate] is not null";
                        break;
                    case 11:
                        statusWhere = @"UserDateAccept is not null and ManagerDateAccept is null
                            and
                            (
                                (currentUser.Level = 6 and (u.Level is null or u.Level = 7))
                                or
                                (
                                    uManagerAccount.Id > 0
                                    and
                                    (
                                        currentUser.Level = u.Level - 1
                                        or (currentUser.Level = u.Level and u.IsMainManager = 0)
                                        or (currentUser.Level = 2 and currentUser.Level = u.Level - 2 and u.IsMainManager = 1)
                                    )
                                )
                            )
                        ";
                        break;
                    default:
                        throw new ArgumentException("Неправильный статус заявки");
                }
                if (statusId != 10)
                    statusWhere += " and DeleteDate is null ";
                if (statusId != 9 && statusId != 10)
                    statusWhere += " and SendTo1C is null ";
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere;
                

                return whereString;
            }
            return whereString;
        }
        public virtual string GetSqlQueryOrdered(string sqlQuery, string whereString,
            int sortedBy,
            bool? sortDescending)
        {
            if (!string.IsNullOrEmpty(whereString))
                sqlQuery += @" where " + whereString;
            if (!sortDescending.HasValue)
                return sqlQuery;
            switch (sortedBy)
            {
                case 0:
                    return sqlQuery;
                case 1:
                    sqlQuery += @" order by Name";
                    break;
                case 2:
                    sqlQuery += @" order by UserName";
                    break;
                case 3:
                    sqlQuery += @" order by Date";
                    break;
                case 4:
                    sqlQuery += @" order by RequestType";
                    break;
                case 5:
                    sqlQuery += @" order by RequestStatus";
                    break;
                case 6:
                    sqlQuery += @" order by Number";
                    break;
                case 7:
                    sqlQuery += @" order by BeginDate";
                    break;
                case 8:
                    sqlQuery += @" order by EndDate";
                    break;
                case 10:
                    sqlQuery += @" order by IsOriginalReceived";
                    break;
                case 11:
                    sqlQuery += @" order by IsPersonnelFileSentToArchive";
                    break;
                case 12:
                    sqlQuery += @" order by SicklistNumber";
                    break;
                case 13:
                    sqlQuery += @" order by Position";
                    break;
                case 14:
                    sqlQuery += @"order by Dep3Name";
                    break;
                case 15:
                    sqlQuery += @"order by Dep7Name";
                    break;
                case 16:
                    sqlQuery += @" order by ManagerName ";
                    break;
            }
            if (sortDescending.Value)
                sqlQuery += " DESC ";
            else
                sqlQuery += " ASC ";
            //sqlQuery += @" order by Date DESC,Name ";
            return sqlQuery;
        }
        public virtual IQuery CreateQuery(string sqlQuery)
        {
            return Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("BeginDate", NHibernateUtil.DateTime).
                AddScalar("EndDate", NHibernateUtil.DateTime).
                AddScalar("Number", NHibernateUtil.Int32).
                AddScalar("UserName", NHibernateUtil.String).
                AddScalar("RequestType", NHibernateUtil.String).
                AddScalar("RequestStatus", NHibernateUtil.String).
                AddScalar("Dep7Name",NHibernateUtil.String).
                AddScalar("Dep3Name",NHibernateUtil.String).
                AddScalar("Position",NHibernateUtil.String).
                AddScalar("ManagerName", NHibernateUtil.String);
        }

        public virtual IList<VacationDto> GetDefaultDocuments(
                                int userId,
                                UserRole role,
                                int departmentId,
                                int positionId,
                                int typeId,
                                int statusId,
                                DateTime? beginDate,
                                DateTime? endDate,
                                string userName, 
                                string sqlQuery,
                                int sortedBy,
                                bool? sortDescending,
                                string Number
            )
        {
            string whereString = GetWhereForUserRole(role, userId, ref sqlQuery);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            whereString = GetUserNameWhere(whereString, userName);
            whereString = GetDocumentNumberWhere(whereString, Number);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString,sortedBy,sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            query.SetInt32("userId", userId);
            AddDatesToQuery(query, beginDate, endDate, userName);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(VacationDto))).List<VacationDto>();
        }

        protected int GetRequestsCountForTypeOneDay(DateTime beginDate, DateTime endDate, RequestTypeEnum type,
                int userId, UserRole userRole)
        {
            string sqlQuery = string.Format(@"select count(Id) ");
            switch (type)
            {
                case RequestTypeEnum.HolidayWork:
                    sqlQuery += @" from [dbo].[HolidayWork] v ";
                    break;
                case RequestTypeEnum.TimesheetCorrection:
                    sqlQuery += @" from [dbo].[TimesheetCorrection] v ";
                    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестный тип заявки {0}", type));

            }
            sqlQuery += string.Format(@" where 
                          v.{0} between :beginDate and :endDate
                          and v.DeleteDate is null ",
                          type == RequestTypeEnum.HolidayWork ? "WorkDate" : "EventDate");
            switch (userRole)
            {
                case UserRole.Employee:
                    sqlQuery += " and v.UserId = :userId ";
                    break;
                //case UserRole.Manager:
                //    sqlQuery += " and u.ManagerId = :userId ";
                //    break;
                //case UserRole.PersonnelManager:
                //    sqlQuery += " and u.PersonnelManagerId = :userId ";
                //    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            }
            IQuery query = AddDatesAndUserIdToQuery(
                Session.CreateSQLQuery(sqlQuery)
                , beginDate
                , endDate, userId);
            //IQuery query = Session.CreateSQLQuery(sqlQuery).
            //   AddScalar("BeginDate", NHibernateUtil.DateTime).
            //   AddScalar("EndDate", NHibernateUtil.DateTime).
            //   SetDateTime("beginDate", beginDate).
            //   SetDateTime("endDate", endDate);
            return query.UniqueResult<int>();
        }


        protected virtual int GetRequestsCountForDismissal(DateTime endDate,
            int userId, UserRole userRole)
        {
            string sqlQuery =
                string.Format(@"select count(Id)
                         from dbo.Dismissal v");
            sqlQuery += string.Format(@" where 
                          v.EndDate <= :endDate
                          and v.DeleteDate is null");
            switch (userRole)
            {
                case UserRole.Employee:
                    sqlQuery += " and v.UserId = :userId ";
                    break;
                //case UserRole.Manager:
                //    sqlQuery += " and u.ManagerId = :userId ";
                //    break;
                //case UserRole.PersonnelManager:
                //    sqlQuery += " and u.PersonnelManagerId = :userId ";
                //    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            }
            IQuery query = AddDatesAndUserIdToQuery(
                Session.CreateSQLQuery(sqlQuery)
                , new DateTime?()
                , endDate, userId);

            //IQuery query = Session.CreateSQLQuery(sqlQuery).
            //   AddScalar("BeginDate", NHibernateUtil.DateTime).
            //   AddScalar("EndDate", NHibernateUtil.DateTime).
            //   SetDateTime("beginDate", beginDate).
            //   SetDateTime("endDate", endDate);
            return query.UniqueResult<int>();
        }

        protected virtual int GetRequestsCountForEmployment(DateTime beginDate, DateTime endDate,
        int userId, UserRole userRole)
        {
            string sqlQuery =
                string.Format(@"select count(Id)
                         from dbo.Employment v");
            sqlQuery += string.Format(@" where 
                          v.BeginDate between :beginDate and :endDate
                          and v.DeleteDate is null ");
            switch (userRole)
            {
                case UserRole.Employee:
                    sqlQuery += " and v.UserId = :userId ";
                    break;
                //case UserRole.Manager:
                //    sqlQuery += " and u.ManagerId = :userId ";
                //    break;
                //case UserRole.PersonnelManager:
                //    sqlQuery += " and u.PersonnelManagerId = :userId ";
                //    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));

            }
            IQuery query = AddDatesAndUserIdToQuery(
                            Session.CreateSQLQuery(sqlQuery)
                            , beginDate
                            , endDate, userId);
            //IQuery query = Session.CreateSQLQuery(sqlQuery).
            //   AddScalar("BeginDate", NHibernateUtil.DateTime).
            //   AddScalar("EndDate", NHibernateUtil.DateTime).
            //   SetDateTime("beginDate", beginDate).
            //   SetDateTime("endDate", endDate);
            return query.UniqueResult<int>();
        }

        protected virtual int GetRequestsCountForType(DateTime beginDate, DateTime endDate, RequestTypeEnum type,
                int userId, UserRole userRole, int requestId)
        {
            string sqlQuery =
                @"select count(Id)";
            switch (type)
            {
                case RequestTypeEnum.Vacation:
                    sqlQuery += @" from [dbo].[Vacation] v ";
                    break;
                case RequestTypeEnum.ChildVacation:
                    sqlQuery += @" from [dbo].[ChildVacation] v ";
                    break;
                case RequestTypeEnum.Absence:
                    sqlQuery += @" from [dbo].[Absence] v ";
                    break;
                case RequestTypeEnum.Sicklist:
                    sqlQuery += @" from [dbo].[Sicklist] v ";
                    break;
                case RequestTypeEnum.Mission:
                    sqlQuery += @" from [dbo].[Mission] v ";
                    break;

                default:
                    throw new ArgumentException(string.Format("Неизвестный тип заявки {0}", type));

            }
            sqlQuery += @" where ((v.BeginDate between :beginDate and :endDate) or
                                 (v.EndDate between :beginDate and :endDate) or 
                                 (:beginDate between v.BeginDate and v.EndDate) or
                                 (:endDate between v.BeginDate and v.EndDate))
                          and v.DeleteDate is null";
            if (type == RequestTypeEnum.Vacation || type == RequestTypeEnum.ChildVacation || type == RequestTypeEnum.Sicklist)
                sqlQuery += string.Format(" and v.Id != {0} ", requestId);
            switch (userRole)
            {
                case UserRole.Employee:
                    sqlQuery += " and v.UserId = :userId ";
                    break;
                //case UserRole.Manager:
                //    sqlQuery += " and u.ManagerId = :userId ";
                //    break;
                //case UserRole.PersonnelManager:
                //    sqlQuery += " and u.PersonnelManagerId = :userId ";
                //    break;
                default:
                    throw new ArgumentException(string.Format("Неизвестная роль пользователя {0}", userRole));
            }
            IQuery query = AddDatesAndUserIdToQuery(
                    Session.CreateSQLQuery(sqlQuery)
                    ,beginDate
                    ,endDate,userId);
               //AddScalar("BeginDate", NHibernateUtil.DateTime).
               //AddScalar("EndDate", NHibernateUtil.DateTime).
               //SetDateTime("beginDate", beginDate).
               //SetDateTime("endDate", endDate);
            return query.UniqueResult<int>();
        }
        protected virtual IQuery AddDatesAndUserIdToQuery(ISQLQuery query, DateTime? beginDate,
                            DateTime endDate,int userId)
        {
                IQuery q = query.
                      SetDateTime("endDate", endDate).
                      SetInt32("userId", userId);
                return !beginDate.HasValue 
                    ? q 
                    : q.SetDateTime("beginDate", beginDate.Value);
        }


        protected IList<GradeAmountDto> GetGradeAmountForGradeAndDate(string tableName,string fieldName,
            int gradeId,DateTime date)
        {
            string sqlQuery =
                string.Format(
                    @";with res as
                            (select [GradeId],{0},max(GradeDate) as MaxDate from {1}
                            where GradeId = :gradeId and GradeDate <= :endDate
                            group by [GradeId],{0})
                            select m.GradeId,m.{0} as Id,Amount from {1} m
                            inner join res r on m.GradeId = r.[GradeId] and m.{0} = r.{0} and m.GradeDate = r.MaxDate
                            order by Id",
                    fieldName, tableName);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("GradeId", NHibernateUtil.Int32).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Amount", NHibernateUtil.Decimal).
                SetDateTime("endDate", date).
                SetInt32("gradeId", gradeId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(GradeAmountDto))).List<GradeAmountDto>();
        }
        protected IList<GradeAmountNameDto> GetGradeAmountForDate(string tableName, string fieldName, string dicTableName, 
           DateTime date)
        {
            string sqlQuery =
                string.Format(
                    @";with res as
                            (select [GradeId],{0},max(GradeDate) as MaxDate from {1}
                            where GradeDate <= :endDate
                            group by [GradeId],{0})
                            select distinct m.GradeId,m.{0} as Id,dic.Name,Amount from {1} m
                            inner join {2} dic on dic.Id =  m.{0} 
                            inner join res r on m.{0} = r.{0} and m.GradeDate = r.MaxDate
                            order by Id",
                    fieldName, tableName, dicTableName);
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("GradeId", NHibernateUtil.Int32).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Amount", NHibernateUtil.Decimal).
                SetDateTime("endDate", date);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(GradeAmountNameDto))).List<GradeAmountNameDto>();
        }

		//public bool IsSameNameEntityExists(Type type,int entityId,string name)
		//{
		//    Validate.NotNull(type,"type"); 
		//    Validate.NotNull(name,"name"); 
		//    Type interfaceType = type.GetInterface("IEntityWithName");
		//    if(interfaceType == null)
		//        throw new ApplicationException(Resources.Exception_IEntityWithNameInterfaceIsNotSupportedByParameter);
		//    ICriteria criteria = Session.CreateCriteria(type);
		//    criteria.Add(Expression.Eq("Name", name));
		//    if (entityId != 0)
		//        criteria.Add(Expression.Not(Expression.Eq("Id", entityId)));
		//    return criteria.SetProjection(Projections.Count("Id")).UniqueResult<int>() > 0;
		//}

		//#region privilege (CMR) support
		//protected static void CreateRolesQuerySetParams(IQuery query, ICollection<ContentManagementRole> roles)
		//{
		//    if (roles != null && roles.Count > 0)
		//    {
		//        IEnumerator<ContentManagementRole> caseEditStatEnum = roles.GetEnumerator();
		//        caseEditStatEnum.MoveNext();
		//        query.SetParameter("role0", caseEditStatEnum.Current);
		//        int listCount = 1;
		//        while (caseEditStatEnum.MoveNext())
		//        {
		//            query.SetParameter(string.Format("role{0}", listCount), caseEditStatEnum.Current);
		//            listCount++;
		//        }
		//    }
		//}

		//protected static void MakeHavingForAllRoles(StringBuilder queryBuilder, List<CaseEditingStatus> statuses, List<ContentManagementRole> roles, bool findAll)
		//{
		//    queryBuilder.Append("having ");
		//    queryBuilder.Append("( ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.AcrPS, statuses, roles, findAll));
		//    queryBuilder.Append("or ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.CaseContributor, statuses, roles, findAll));
		//    queryBuilder.Append("or ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.CaseEditor, statuses, roles, findAll));
		//    queryBuilder.Append("or ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.CategoryEditor, statuses, roles, findAll));
		//    queryBuilder.Append("or ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.ChiefEditor, statuses, roles, findAll));
		//    queryBuilder.Append("or ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.Reader, statuses, roles, findAll));
		//    queryBuilder.Append("or ");
		//    queryBuilder.Append(GetConditionForRole(ContentManagementRole.Reviewer, statuses, roles, findAll));
		//    queryBuilder.Append(") ");            
		//}
		//protected static void CreateStatusesQuerySetParams(IQuery query, ICollection<CaseEditingStatus> editingStatuses)
		//{
		//    if (editingStatuses != null && editingStatuses.Count > 0)
		//    {
		//        IEnumerator<CaseEditingStatus> caseEditStatEnum = editingStatuses.GetEnumerator();
		//        caseEditStatEnum.MoveNext();
		//        query.SetParameter("stat0", caseEditStatEnum.Current);
		//        int listCount = 1;
		//        while (caseEditStatEnum.MoveNext())
		//        {
		//            query.SetParameter(string.Format("stat{0}", listCount), caseEditStatEnum.Current);
		//            listCount++;
		//        }
		//    }
		//}

		//protected static string GetConditionForRole(ContentManagementRole role, ICollection<CaseEditingStatus> editingStatuses, ICollection<ContentManagementRole> roles, bool bFindAll)
		//{
		//    StringBuilder queryBuilder = new StringBuilder();
		//    ICollection<CaseEditingStatus> statuses = null;
		//    if (!bFindAll)
		//    {
		//        statuses = GetRoutingStatusesByRole(role);
		//    }

		//    queryBuilder.Append("(");
		//    queryBuilder.Append(string.Format("max(pm.Role&:role{0}) > 0", roles.Count));
		//    if (statuses != null && statuses.Count > 0)
		//    {
		//        queryBuilder.Append(" and max(bc.EditingStatus) in ");
		//        queryBuilder.Append(CreateStatusesInQueryParams(statuses, editingStatuses));
		//    }
		//    queryBuilder.Append(") ");

		//    if (!roles.Contains(role))
		//    {
		//        roles.Add(role);
		//    }

		//    return queryBuilder.ToString();
		//}

		//private static ICollection<CaseEditingStatus> GetRoutingStatusesByRole(ContentManagementRole role)
		//{
		//    List<CaseEditingStatus> statuses = new List<CaseEditingStatus>();
		//    if (((role & ContentManagementRole.CaseContributor) == ContentManagementRole.CaseContributor)
		//        || ((role & ContentManagementRole.CaseEditor) == ContentManagementRole.CaseEditor))
		//    {
		//        statuses.Add(CaseEditingStatus.ContentContributionByCaseEditor);
		//        statuses.Add(CaseEditingStatus.SentBackToCaseEditor);
		//    }
		//    if ((role & ContentManagementRole.CategoryEditor) == ContentManagementRole.CategoryEditor)
		//    {
		//        statuses.Add(CaseEditingStatus.SubmittedToSectionEditor);
		//        statuses.Add(CaseEditingStatus.ReviewBySectionEditor);
		//        statuses.Add(CaseEditingStatus.SentBackToSectionEditor);
		//    }
		//    if ((role & ContentManagementRole.AcrPS) == ContentManagementRole.AcrPS)
		//    {
		//        statuses.Add(CaseEditingStatus.SubmittedToAcr);
		//        statuses.Add(CaseEditingStatus.ReviewByAcr);
		//        statuses.Add(CaseEditingStatus.SentBackToAcr);
		//    }
		//    if ((role & ContentManagementRole.ChiefEditor) == ContentManagementRole.ChiefEditor)
		//    {
		//        statuses.Add(CaseEditingStatus.SubmittedToChiefEditor);
		//        statuses.Add(CaseEditingStatus.ReviewByChiefEditor);
		//    }
		//    return statuses;
		//}

		//private static string CreateStatusesInQueryParams(ICollection<CaseEditingStatus> editingStatuses, ICollection<CaseEditingStatus> editingStatusesAll)
		//{
		//    StringBuilder queryBuilder = new StringBuilder();
		//    if (editingStatuses != null && editingStatuses.Count > 0)
		//    {
		//        IEnumerator<CaseEditingStatus> caseEditStatEnum = editingStatuses.GetEnumerator();
		//        caseEditStatEnum.MoveNext();
		//        int countAll = editingStatusesAll.Count;
		//        if (!editingStatusesAll.Contains(caseEditStatEnum.Current))
		//        {
		//            editingStatusesAll.Add(caseEditStatEnum.Current);
		//        }
		//        queryBuilder.Append(string.Format("(:stat{0}", countAll));
		//        int listCount = 1;
		//        while (caseEditStatEnum.MoveNext())
		//        {
		//            if (!editingStatusesAll.Contains(caseEditStatEnum.Current))
		//            {
		//                editingStatusesAll.Add(caseEditStatEnum.Current);
		//            }
		//            queryBuilder.Append(string.Format(", :stat{0}", listCount + countAll));
		//            listCount++;
		//        }
		//        queryBuilder.Append(")");
		//    }
		//    return queryBuilder.ToString();
		//}

		//public ContentManagementRole GetUserRoles(User user, AbstractSecuredEntity item, Category cat)
		//{
		//    IQuery query = Session.CreateSQLQuery(item.GetUserRoles_QueryString(user, cat))
		//        .AddScalar(AbstractSecuredEntity.RolesResName, NHibernateUtil.Int32);

		//    item.GetUserRoles_QuerySet(query, user, cat);

		//    return (ContentManagementRole)query.UniqueResult<int>();
		//}
		//#endregion
    }
    //public class DefaultUsedDao<TEntity> : DefaultDao<TEntity>, IUsedDao<TEntity>
    //    where TEntity : AbstractUsedEntity
    //{
    //    public DefaultUsedDao(ISessionManager sessionManager) : base(sessionManager)
    //    {
    //    }
    //    public virtual IList<TEntity> FindAllWithUsedFlag(string fkExistsViewName)
    //    {
    //        IList<TEntity> list = CreateCriteria().List<TEntity>();
    //        if (list.Count <= 0)
    //            return list;
    //        IList<ItemUsedDto> usedList = CoreUtils.GetFKsForEntitiesList(Session, fkExistsViewName, list);
    //        CoreUtils.SetUsedEntityFlag(list, usedList);
    //        return list;
    //    }
    //    public virtual TEntity FindByIdWithUsedFlag(int id, string fkExistsViewName)
    //    {
    //        TEntity entity = FindById(id);
    //        if (entity == null)
    //            return entity;
    //        IList<TEntity> list = new List<TEntity>();
    //        list.Add(entity);
    //        IList<ItemUsedDto> usedList = CoreUtils.GetFKsForEntitiesList(Session, fkExistsViewName, list);
    //        if(usedList.Count == 1)
    //            entity.IsUsed = usedList[0].Counter > 0 ? true : false;  
    //        return entity;
    //    }

    //}

    public class DefaultDaoSorted<TEntity> : DefaultDao<TEntity>
        where TEntity : class,IEntity<int>,ISortOrder
    {
        public DefaultDaoSorted(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual List<TEntity> LoadAllSortedByOrder()
        {
            return Session.Query<TEntity>().OrderBy(a => a.SortOrder).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using log4net;
using NHibernate;
using System.Collections;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;

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
        public virtual void Delete(TIdentifier entityId)
        {
            TEntity entity = Session.Load<TEntity>(entityId);
            Session.Delete(entity);
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
    }

    public class DefaultDao<TEntity> : DefaultDao<TEntity, int>, IDao<TEntity>
        where TEntity : IEntity<int>
    {
        protected const string ObjectPropertyFormat = "{0}.{1}";
        protected const string DeleteRequestText = "������ ���������";
        public DefaultDao(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        protected IConfigurationService configurationService;
        public IConfigurationService ConfigurationService
        {
            set { configurationService = value; }
            get { return Validate.Dependency(configurationService); }
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
        public virtual string GetWhereForUserRole(UserRole role,int userId)
        {
            switch (role)
            {
                case UserRole.Employee:
                    return string.Format(" u.Id = {0} ", userId);
                case UserRole.Manager:
                    return string.Format(" u.ManagerId = {0} ", userId);
                case UserRole.PersonnelManager:
                    return string.Format(" exists ( select * from UserToPersonnel up where up.PersonnelId = {0} and u.Id = up.UserId ) ", userId);
                case UserRole.Inspector:
                    return string.Format(" exists ( select * from InspectorToUser iu where iu.InspectorId = {0} and u.Id = iu.UserId ) ", userId);
                case UserRole.Chief:
                    return string.Format(" exists ( select * from ChiefToUser cu where cu.ChiefId = {0} and u.Id = cu.UserId ) ", userId);
                default:
                    throw new ArgumentException(string.Format("Invalid user role {0}",role));
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
            DateTime? endDate)
        {
            if (beginDate.HasValue)
                query.SetDateTime("beginDate", beginDate.Value);
            if (endDate.HasValue)
                query.SetDateTime("endDate", endDate.Value.AddDays(1));
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
        public virtual string GetDepartmentWhere(string whereString, int departmentId)
        {
            if (departmentId != 0)
            {
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += string.Format(@"exists 
                    (select d1.ID from dbo.Department d
                     inner join dbo.Department d1 on d1.Path like d.Path +'%'
                     and u.DepartmentID = d1.ID and d1.ItemLevel = 7 
                     and d.Id = {0}) "
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
                    default:
                        throw new ArgumentException("������������ ������ ������");
                }
                if (whereString.Length > 0)
                    whereString += @" and ";
                whereString += @" " + statusWhere + " ";
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
                    sqlQuery += @" order by Date";
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
            return  Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("UserId", NHibernateUtil.Int32).
                AddScalar("Name", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime);
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
                                string sqlQuery,
                                int sortedBy,
                                bool? sortDescending
            )
        {
            string whereString = GetWhereForUserRole(role, userId);
            whereString = GetTypeWhere(whereString, typeId);
            whereString = GetStatusWhere(whereString, statusId);
            whereString = GetDatesWhere(whereString, beginDate, endDate);
            whereString = GetPositionWhere(whereString, positionId);
            whereString = GetDepartmentWhere(whereString, departmentId);
            sqlQuery = GetSqlQueryOrdered(sqlQuery, whereString,sortedBy,sortDescending);

            IQuery query = CreateQuery(sqlQuery);
            AddDatesToQuery(query, beginDate, endDate);
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
                    throw new ArgumentException(string.Format("����������� ��� ������ {0}", type));

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
                    throw new ArgumentException(string.Format("����������� ���� ������������ {0}", userRole));

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
                    throw new ArgumentException(string.Format("����������� ���� ������������ {0}", userRole));

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
                    throw new ArgumentException(string.Format("����������� ���� ������������ {0}", userRole));

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
                int userId, UserRole userRole,int vacationId)
        {
            string sqlQuery =
                @"select count(Id)";
            switch (type)
            {
                case RequestTypeEnum.Vacation:
                    sqlQuery += @" from [dbo].[Vacation] v ";
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
                    throw new ArgumentException(string.Format("����������� ��� ������ {0}", type));

            }
            sqlQuery += @" where ((v.BeginDate between :beginDate and :endDate) or
                                 (v.EndDate between :beginDate and :endDate) or 
                                 (:beginDate between v.BeginDate and v.EndDate) or
                                 (:endDate between v.BeginDate and v.EndDate))
                          and v.DeleteDate is null";
            if (type == RequestTypeEnum.Vacation)
                sqlQuery += string.Format(" and v.Id != {0} ",vacationId);
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
                    throw new ArgumentException(string.Format("����������� ���� ������������ {0}", userRole));
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
}
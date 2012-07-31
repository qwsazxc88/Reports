using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using log4net;
using NHibernate;
using System.Collections;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;
using Reports.Core.Utils;

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
        public DefaultDao(ISessionManager sessionManager) : base(sessionManager)
        {
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
        public IList<TEntity> LoadAllSorted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TEntity));
            criteria.AddOrder(new Order("Name", true));
            return criteria.List<TEntity>();
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
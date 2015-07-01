using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
	public class DocumentDao : DefaultDao<Document>, IDocumentDao
	{
		//private const int maxDateDiffInDaysForNotAdminUser = 7;

		public DocumentDao(ISessionManager sessionManager) : base(sessionManager)
        {
        }
        public IList<DocumentDto> GetDocumentsListForManager(int managerId,
            UserRole managerRole,bool showNew,int? ownerId,int subtypeId)
        {
            string approvedString = string.Empty;
            string whereString = string.Empty;
            string orderByString = string.Empty;
            if (ownerId.HasValue)
                whereString = @" doc.UserId = :ownerId and ";
            if(subtypeId != 0)
                whereString += @" doc.SubTypeId = :subTypeId and ";
            bool managerIdUsed = false; 
            switch (managerRole)
            {
                case UserRole.Manager:
                    approvedString = @" case when ManagerDateAccept is null then 0 else 1 end as IsApproved ";
                    whereString += @" u.ManagerId = :managerId ";
                    managerIdUsed = true;
                    if (showNew)
                    {
                        whereString += @" and doc.ManagerDateAccept is null";
                        orderByString = @" order by doc.LastModifiedDate desc ";
                    }
                    else
                    {
                        orderByString =
                            @"order by doc.LastModifiedDate desc,
                            case when ManagerDateAccept is null then 0 else 1 end ";
                    }
                    
                    break;
                case UserRole.PersonnelManager:
                    approvedString = @" case when PersonnelManagerDateAccept is null then 0 else 1 end as IsApproved ";
                    whereString += @" u.PersonnelManagerId = :managerId and ManagerDateAccept is not null ";
                    managerIdUsed = true;
                    if (showNew)
                    {
                        whereString += @" and doc.PersonnelManagerDateAccept is null";
                        orderByString = @" order by doc.LastModifiedDate desc ";
                    }
                    else
                    {
                        orderByString =
                            @"order by doc.LastModifiedDate desc,
                            case when PersonnelManagerDateAccept is null then 0 else 1 end ";
                    }

                    break;
                case UserRole.BudgetManager:
                    approvedString = @" case when BudgetManagerDateAccept is null then 0 else 1 end as IsApproved ";
                    whereString += @" PersonnelManagerDateAccept is not null ";
                    if (showNew)
                    {
                        whereString += @" and doc.BudgetManagerDateAccept is null";
                        orderByString = @" order by doc.LastModifiedDate desc ";
                    }
                    else
                    {
                        orderByString =
                            @"order by doc.LastModifiedDate desc,
                            case when BudgetManagerDateAccept is null then 0 else 1 end ";
                    }

                    break;
                case UserRole.Estimator:
                case UserRole.OutsourcingManager:
                    approvedString = @" case when OutsourcingManagerDateAccept is null then 0 else 1 end as IsApproved ";
                    whereString += @" BudgetManagerDateAccept is not null ";
                    if (showNew)
                    {
                        whereString += @" and doc.OutsourcingManagerDateAccept is null";
                        orderByString = @" order by doc.LastModifiedDate desc ";
                    }
                    else
                    {
                        orderByString =
                            @"order by doc.LastModifiedDate desc,
                            case when OutsourcingManagerDateAccept is null then 0 else 1 end ";
                    }

                    break;

            }
            string sqlQuery =
                @"select doc.id as Id 
                ,doc.UserId as OwnerId," +
                approvedString +
                @",dt.Name as Type
                            ,dst.Name as SubType
	                        ,doc.LastModifiedDate as Date
                            from document doc 
                            inner join dbo.EmployeeDocumentType dt on dt.id = doc.TypeId
                            inner join Users u on u.Id = doc.UserId
                            left join dbo.EmployeeDocumentSubType dst on dst.id = doc.SubTypeId
                            where " +
                whereString +
                orderByString;
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                AddScalar("Id", NHibernateUtil.Int32).
                AddScalar("OwnerId", NHibernateUtil.Int32).
                AddScalar("Type", NHibernateUtil.String).
                AddScalar("SubType", NHibernateUtil.String).
                AddScalar("Date", NHibernateUtil.DateTime).
                AddScalar("IsApproved", NHibernateUtil.Boolean);
            if(managerIdUsed)
                query.SetInt32("managerId", managerId);
            if(ownerId.HasValue)
                query.SetInt32("ownerId", ownerId.Value);
            if(subtypeId != 0)
                query.SetInt32("subTypeId", subtypeId);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(DocumentDto))).List<DocumentDto>();
        }
        public IList<DocumentDto> GetDocumentsForOwner(int userId)
        {
            return Session.CreateSQLQuery(@"select doc.id as Id
                            ,doc.UserId as OwnerId 
	                        ,case when ManagerDateAccept is null then 0 else 1 end as IsApproved
    	                    ,dt.Name as Type
                            ,dst.Name as SubType
	                        ,doc.LastModifiedDate as Date
                            from document doc 
                            inner join dbo.EmployeeDocumentType dt on dt.id = doc.TypeId
                            left join dbo.EmployeeDocumentSubType dst on dst.id = doc.SubTypeId
                            where UserId = :UserId
                            order by doc.LastModifiedDate desc,
                            case when ManagerDateAccept is null then 0 else 1 end ").
            AddScalar("Id", NHibernateUtil.Int32).
            AddScalar("OwnerId", NHibernateUtil.Int32).
            AddScalar("Type", NHibernateUtil.String).
            AddScalar("SubType", NHibernateUtil.String).
            AddScalar("Date", NHibernateUtil.DateTime).
            AddScalar("IsApproved", NHibernateUtil.Boolean).
            //SetInt32("TypeId", documentType).
            SetInt32("UserId", userId).
            SetResultTransformer(Transformers.AliasToBean(typeof(DocumentDto))).List<DocumentDto>();
        }
        public DocumentAndAttachmentDto GetDocumentAndAttachmentForId(int documentId)
        {
            return (DocumentAndAttachmentDto)Session.CreateSQLQuery(@"select 
                            doc.id,
                            doc.Version,
                            doc.LastModifiedDate as Date,
                            doc.TypeId as TypeId,
                            doc.SubTypeId as SubTypeId,
                            doc.Comment,
                            doc.ManagerDateAccept,
                            doc.PersonnelManagerDateAccept,
                            doc.BudgetManagerDateAccept,
                            doc.OutsourcingManagerDateAccept, 
                            doc.SendEmailToBilling, 
                            att.Id as AttachId,
                            att.FileName as AttachName from document doc 
                            left join dbo.Attachment att on doc.Id = att.DocumentId
                            where doc.id = :Id").
            AddScalar("Id", NHibernateUtil.Int32).
            AddScalar("Version", NHibernateUtil.Int32).
            AddScalar("Date", NHibernateUtil.DateTime).
            AddScalar("TypeId", NHibernateUtil.Int32).
            AddScalar("SubTypeId", NHibernateUtil.Int32).
            AddScalar("Comment", NHibernateUtil.StringClob).
            AddScalar("ManagerDateAccept", NHibernateUtil.DateTime).
            AddScalar("PersonnelManagerDateAccept", NHibernateUtil.DateTime).
            AddScalar("BudgetManagerDateAccept", NHibernateUtil.DateTime).
            AddScalar("OutsourcingManagerDateAccept", NHibernateUtil.DateTime).
            AddScalar("SendEmailToBilling", NHibernateUtil.Boolean).
            AddScalar("AttachId", NHibernateUtil.Int32).
            AddScalar("AttachName", NHibernateUtil.String).
            SetInt32("Id", documentId).
            SetResultTransformer(Transformers.AliasToBean(typeof(DocumentAndAttachmentDto)))
            .UniqueResult();
        }
        //public int MaxDateDiffInDaysForNotAdminUser
        //{
        //    get { return maxDateDiffInDaysForNotAdminUser; }
        //}

//        public IList<CalcListDto> GetDocumentByType(ReportType documentType,int userId)
//        {
//            return Session.CreateSQLQuery(@"select id,DateCreated,BeginDate,EndDate,Number 
//			from document where ReportType=:ReportType
//			and UserId=:UserId ").
//            AddScalar("id",NHibernateUtil.Int32).
//            AddScalar("DateCreated",NHibernateUtil.DateTime).
//            AddScalar("BeginDate", NHibernateUtil.DateTime).
//            AddScalar("EndDate", NHibernateUtil.DateTime).
//            AddScalar("Number",NHibernateUtil.String).
//            SetInt32("ReportType",(int)documentType).
//            SetInt32("UserId", userId).
//            SetResultTransformer(Transformers.AliasToBean(typeof(CalcListDto))).List<CalcListDto>();
//        }
//        public IList<CalcListDto> FindByFilter(CalcListFilter filter,bool isAdmin,int daysDelay, out int count)
//        {
//            ICriteria criteria = Session.CreateCriteria(typeof(Document));
//            ICriterion criterion = Expression.Sql("");

//            criterion = Restrictions.And(criterion, Restrictions.Eq("ReportType",filter.Type));
//            criterion = Restrictions.And(criterion, Restrictions.Eq("User.Id", filter.UserId));
//            if(!isAdmin)
//                criterion = Restrictions.And(criterion, Restrictions.Eq("DateCreated", DateTime.Today.AddDays(-daysDelay)));
//            ICriteria criteria2 = Session.CreateCriteria(typeof(Document));
//            criteria2.Add(criterion);
//            criteria2.SetProjection(Projections.RowCount());
//            count = (int)criteria2.UniqueResult();
//            if (filter.FirstResult > count) filter.FirstResult = 0;
//            criteria.Add(criterion);
//            if (filter.MaxResults != -1)
//                criteria.SetMaxResults(filter.MaxResults);
//            if (filter.FirstResult != -1)
//                criteria.SetFirstResult(filter.FirstResult);
//            if (!string.IsNullOrEmpty(filter.SortExpression))
//                criteria.AddOrder(new Order(filter.SortExpression, filter.SortAscending));

//            IList<Document> list = criteria.List<Document>();
//            IList<CalcListDto> result = new List<CalcListDto>();
//            if (list.Count <= 0)
//                return result;
//            foreach (Document doc in list)
//            {
//                CalcListDto dto = new CalcListDto(doc);
//                result.Add(dto);
//            }
//            //IList<ItemUsedDto> usedList = CoreUtils.GetFKsForEntitiesList(Session, FKExistsViewName, list);
//            //CoreUtils.SetUsedEntityFlag(list, usedList);

//            return result;
//        }

	}
}

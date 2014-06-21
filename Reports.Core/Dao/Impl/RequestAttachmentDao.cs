using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Enum;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class RequestAttachmentDao : DefaultDao<RequestAttachment>, IRequestAttachmentDao
    {
        public RequestAttachmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region IRequestAttachmentDao Members

        public virtual RequestAttachment FindByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type)
        {
            ICriteria criteria = Session.CreateCriteria(typeof (RequestAttachment));
            criteria.Add(Restrictions.Eq("RequestId", id));
            criteria.Add(Restrictions.Eq("RequestType", (int)type));
            return (RequestAttachment) criteria.UniqueResult();
        }
        public virtual IList<RequestAttachment> FindManyByRequestIdAndTypeId(int id, RequestAttachmentTypeEnum type)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(RequestAttachment));
            criteria.Add(Restrictions.Eq("RequestId", id));
            criteria.Add(Restrictions.Eq("RequestType", (int)type));
            return criteria.List<RequestAttachment>();
        }
        public virtual int GetAttachmentsCount(int entityId)
        {
            return (int)Session.CreateCriteria(typeof(RequestAttachment))
            .Add(Restrictions.Eq("RequestId", entityId))
            .SetProjection(Projections.RowCount()).UniqueResult();
        }
        public virtual int DeleteForEntityId(int entityId)
        {
            const string sqlQuery =
                @"delete from dbo.RequestAttachment
                where RequestId = :entityId";
            ISQLQuery query = Session.CreateSQLQuery(sqlQuery);
            query.SetInt32("entityId", entityId);
            return query.ExecuteUpdate();
        }
        public IList<IdEntityIdDto> LoadAttachmentsForEntitiesIdsList(List<int> entityIds, RequestAttachmentTypeEnum type)
        {

            if (entityIds.Count == 0)
                return new List<IdEntityIdDto>();

            const string sqlQuery = @"select Id,RequestId as EntityId from [dbo].[RequestAttachment] where 
                              [RequestType] = :typeId and RequestId in (:entitiesList)";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
              AddScalar("Id", NHibernateUtil.Int32).
              AddScalar("EntityId", NHibernateUtil.Int32).
              SetInt32("typeId", (int)type).
              SetParameterList("entitiesList", entityIds);
            return query.SetResultTransformer(Transformers.AliasToBean(typeof(IdEntityIdDto))).List<IdEntityIdDto>();
        }
        public int DeleteAttachmentsForEntitiesIdsList(List<int> entityIds, RequestAttachmentTypeEnum type)
        {
            if (entityIds.Count == 0)
                return 0;
            const string sqlQuery = @"delete from [dbo].[RequestAttachment] where 
                              [RequestType] = :typeId and RequestId in (:entitiesList)";
            IQuery query = Session.CreateSQLQuery(sqlQuery).
                            SetInt32("typeId", (int)type).
                            SetParameterList("entitiesList", entityIds);
            return query.ExecuteUpdate();
        }
        #endregion
    }
}
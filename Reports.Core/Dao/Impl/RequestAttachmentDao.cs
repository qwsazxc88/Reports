using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
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
        #endregion
    }
}
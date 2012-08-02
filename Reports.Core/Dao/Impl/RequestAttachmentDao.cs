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

        public virtual RequestAttachment FindByRequestIdAndTypeId(int id, RequestTypeEnum type)
        {
            ICriteria criteria = Session.CreateCriteria(typeof (RequestAttachment));
            criteria.Add(Restrictions.Eq("RequestId", id));
            criteria.Add(Restrictions.Eq("RequestType", (int)type));
            return (RequestAttachment) criteria.UniqueResult();
        }

        #endregion
    }
}
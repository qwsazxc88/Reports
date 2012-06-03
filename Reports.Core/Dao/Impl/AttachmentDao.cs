using NHibernate;
using NHibernate.Criterion;
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AttachmentDao : DefaultDao<Attachment>, IAttachmentDao
    {
        public AttachmentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
        public virtual Attachment FindByDocumentId(int documentId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(Attachment));
            criteria.Add(Restrictions.Eq("Document.Id", documentId));
            return (Attachment)criteria.UniqueResult();
        }
    }
}
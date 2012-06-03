using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DocumentCommentDao : DefaultDao<DocumentComment>, IDocumentCommentDao
    {
        public DocumentCommentDao(ISessionManager sessionManager) : base(sessionManager)
        {
        }
    }
}

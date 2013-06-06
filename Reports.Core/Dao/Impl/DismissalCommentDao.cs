using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class DismissalCommentDao : DefaultDao<DismissalComment>, IDismissalCommentDao
    {
        public DismissalCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
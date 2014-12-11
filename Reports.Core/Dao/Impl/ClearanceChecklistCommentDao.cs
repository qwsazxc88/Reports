using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ClearanceChecklistCommentDao : DefaultDao<ClearanceChecklistComment>, IClearanceChecklistCommentDao
    {
        public ClearanceChecklistCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
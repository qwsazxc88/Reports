using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class SicklistCommentDao : DefaultDao<SicklistComment>, ISicklistCommentDao
    {
        public SicklistCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
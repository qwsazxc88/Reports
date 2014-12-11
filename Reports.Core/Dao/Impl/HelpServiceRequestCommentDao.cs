using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class HelpServiceRequestCommentDao : DefaultDao<HelpServiceRequestComment>, IHelpServiceRequestCommentDao
    {
        public HelpServiceRequestCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
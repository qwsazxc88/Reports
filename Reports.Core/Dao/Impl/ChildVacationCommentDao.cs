using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class ChildVacationCommentDao : DefaultDao<ChildVacationComment>, IChildVacationCommentDao
    {
        public ChildVacationCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
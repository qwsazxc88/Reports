using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCommentDao : DefaultDao<EmploymentComment>, IEmploymentCommentDao
    {
        public EmploymentCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
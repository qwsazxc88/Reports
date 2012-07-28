using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class AbsenceCommentDao : DefaultDao<AbsenceComment>, IAbsenceCommentDao
    {
        public AbsenceCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
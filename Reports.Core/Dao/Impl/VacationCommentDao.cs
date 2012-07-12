using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class VacationCommentDao : DefaultDao<VacationComment>, IVacationCommentDao
    {
        public VacationCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
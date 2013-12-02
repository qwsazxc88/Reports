using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionOrderCommentDao : DefaultDao<MissionOrderComment>, IMissionOrderCommentDao
    {
        public MissionOrderCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
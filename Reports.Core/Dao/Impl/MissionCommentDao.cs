using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionCommentDao : DefaultDao<MissionComment>, IMissionCommentDao
    {
        public MissionCommentDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
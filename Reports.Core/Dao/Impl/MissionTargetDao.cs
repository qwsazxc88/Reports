using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionTargetDao : DefaultDao<MissionTarget>, IMissionTargetDao
    {
        public MissionTargetDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
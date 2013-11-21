using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionGoalDao : DefaultDao<MissionGoal>, IMissionGoalDao
    {
        public MissionGoalDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
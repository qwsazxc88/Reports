using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    internal class MissionDailyAllowanceDao : DefaultDao<MissionDailyAllowance>, IMissionDailyAllowanceDao
    {
        public MissionDailyAllowanceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
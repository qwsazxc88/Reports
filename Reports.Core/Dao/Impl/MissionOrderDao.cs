using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionOrderDao : DefaultDao<MissionOrder>, IMissionOrderDao
    {
        public MissionOrderDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
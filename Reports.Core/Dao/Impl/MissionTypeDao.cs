using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionTypeDao : DefaultDao<MissionType>, IMissionTypeDao
    {
        public MissionTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionResidenceDao : DefaultDao<MissionResidence>, IMissionResidenceDao
    {
        public MissionResidenceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
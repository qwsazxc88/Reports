using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionCountryDao : DefaultDao<MissionCountry>, IMissionCountryDao
    {
        public MissionCountryDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
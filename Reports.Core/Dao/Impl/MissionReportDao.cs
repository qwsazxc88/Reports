using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionReportDao : DefaultDao<MissionReport>, IMissionReportDao
    {
        public MissionReportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
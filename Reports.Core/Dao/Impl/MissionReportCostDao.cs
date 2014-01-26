using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionReportCostDao : DefaultDao<MissionReportCost>, IMissionReportCostDao
    {
        public MissionReportCostDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
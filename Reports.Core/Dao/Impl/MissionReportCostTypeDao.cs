using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class MissionReportCostTypeDao : DefaultDao<MissionReportCostType>, IMissionReportCostTypeDao
    {
        public MissionReportCostTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
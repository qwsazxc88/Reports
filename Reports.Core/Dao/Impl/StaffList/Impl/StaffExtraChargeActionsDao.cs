using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник действий с надбавками.
    /// </summary>
    public class StaffExtraChargeActionsDao : DefaultDao<StaffExtraChargeActions>, IStaffExtraChargeActionsDao
    {
        public StaffExtraChargeActionsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

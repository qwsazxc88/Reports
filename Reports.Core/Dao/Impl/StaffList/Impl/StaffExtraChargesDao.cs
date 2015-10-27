using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник надбавок.
    /// </summary>
    public class StaffExtraChargesDao : DefaultDao<StaffExtraCharges>, IStaffExtraChargesDao
    {
        public StaffExtraChargesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

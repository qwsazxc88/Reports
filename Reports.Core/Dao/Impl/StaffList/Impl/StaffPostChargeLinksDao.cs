using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Надбавки сотрудников.
    /// </summary>
    public class StaffPostChargeLinksDao : DefaultDao<StaffPostChargeLinks>, IStaffPostChargeLinksDao
    {
        public StaffPostChargeLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

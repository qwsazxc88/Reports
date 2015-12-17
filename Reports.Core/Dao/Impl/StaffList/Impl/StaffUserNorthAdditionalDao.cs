using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Журнал северных надбавок(%) для сотрудников.
    /// </summary>
    public class StaffUserNorthAdditionalDao : DefaultDao<StaffUserNorthAdditional>, IStaffUserNorthAdditionalDao
    {
        public StaffUserNorthAdditionalDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

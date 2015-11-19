using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник видов условий труда.
    /// </summary>
    public class StaffWorkingConditionsDao : DefaultDao<StaffWorkingConditions>, IStaffWorkingConditionsDao
    {
        public StaffWorkingConditionsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

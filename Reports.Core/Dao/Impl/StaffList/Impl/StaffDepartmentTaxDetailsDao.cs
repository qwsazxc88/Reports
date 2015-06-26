using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Налоговые реквизиты.
    /// </summary>
    public class StaffDepartmentTaxDetailsDao : DefaultDao<StaffDepartmentTaxDetails>, IStaffDepartmentTaxDetailsDao
    {
        public StaffDepartmentTaxDetailsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

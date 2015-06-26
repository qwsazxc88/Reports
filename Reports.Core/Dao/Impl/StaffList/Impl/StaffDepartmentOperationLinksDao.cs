using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Операции проводимые подразделением.
    /// </summary>
    public class StaffDepartmentOperationLinksDao : DefaultDao<StaffDepartmentOperationLinks>, IStaffDepartmentOperationLinksDao
    {
        public StaffDepartmentOperationLinksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

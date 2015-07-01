using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник операций подразделений.
    /// </summary>
    public class StaffDepartmentOperationsDao : DefaultDao<StaffDepartmentOperations>, IStaffDepartmentOperationsDao
    {
        public StaffDepartmentOperationsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

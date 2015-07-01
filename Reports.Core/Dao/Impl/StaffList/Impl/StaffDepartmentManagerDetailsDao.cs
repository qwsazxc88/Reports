using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Управленческие реквизиты.
    /// </summary>
    public class StaffDepartmentManagerDetailsDao : DefaultDao<StaffDepartmentManagerDetails>, IStaffDepartmentManagerDetailsDao
    {
        public StaffDepartmentManagerDetailsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

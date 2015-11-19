using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// ЦБ реквизиты подразделения.
    /// </summary>
    public class StaffDepartmentCBDetailsDao : DefaultDao<StaffDepartmentCBDetails>, IStaffDepartmentCBDetailsDao
    {
        public StaffDepartmentCBDetailsDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

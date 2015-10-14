using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник видов заявок для подразделений.
    /// </summary>
    public class StaffDepartmentRequestTypesDao : DefaultDao<StaffDepartmentRequestTypes>, IStaffDepartmentRequestTypesDao
    {
        public StaffDepartmentRequestTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

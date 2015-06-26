using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник типов подразделений.
    /// </summary>
    public class StaffDepartmentTypesDao : DefaultDao<StaffDepartmentTypes>, IStaffDepartmentTypesDao
    {
        public StaffDepartmentTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

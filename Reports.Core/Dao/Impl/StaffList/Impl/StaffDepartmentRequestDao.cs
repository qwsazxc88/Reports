using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Заявки на изменение структуры подразделений.
    /// </summary>
    public class StaffDepartmentRequestDao : DefaultDao<StaffDepartmentRequest>, IStaffDepartmentRequestDao
    {
        public StaffDepartmentRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Ориентиры подразделений.
    /// </summary>
    public class StaffDepartmentLandmarksDao : DefaultDao<StaffDepartmentLandmarks>, IStaffDepartmentLandmarksDao
    {
        public StaffDepartmentLandmarksDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

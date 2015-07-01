using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник видов ориентиров.
    /// </summary>
    public class StaffLandmarkTypesDao : DefaultDao<StaffLandmarkTypes>, IStaffLandmarkTypesDao
    {
        public StaffLandmarkTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

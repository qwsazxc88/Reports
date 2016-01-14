using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник видов длительных отсутствий.
    /// </summary>
    public class StaffLongAbsencesTypesDao : DefaultDao<StaffLongAbsencesTypes>, IStaffLongAbsencesTypesDao
    {
        public StaffLongAbsencesTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

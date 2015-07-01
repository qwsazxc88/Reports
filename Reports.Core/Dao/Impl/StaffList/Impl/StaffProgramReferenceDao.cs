using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник совместимых программ.
    /// </summary>
    public class StaffProgramReferenceDao : DefaultDao<StaffProgramReference>, IStaffProgramReferenceDao
    {
        public StaffProgramReferenceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

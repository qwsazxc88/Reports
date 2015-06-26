using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник кодов совместимых программ.
    /// </summary>
    public class StaffProgramCodesDao : DefaultDao<StaffProgramCodes>, IStaffProgramCodesDao
    {
        public StaffProgramCodesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

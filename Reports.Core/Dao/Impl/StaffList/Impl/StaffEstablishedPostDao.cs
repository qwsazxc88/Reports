using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник штатных единиц.
    /// </summary>
    public class StaffEstablishedPostDao : DefaultDao<StaffEstablishedPost>, IStaffEstablishedPostDao
    {
        public StaffEstablishedPostDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

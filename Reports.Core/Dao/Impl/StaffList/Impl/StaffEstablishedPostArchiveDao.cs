using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Архив справочника штатных единиц.
    /// </summary>
    public class StaffEstablishedPostArchiveDao : DefaultDao<StaffEstablishedPostArchive>, IStaffEstablishedPostArchiveDao
    {
        public StaffEstablishedPostArchiveDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

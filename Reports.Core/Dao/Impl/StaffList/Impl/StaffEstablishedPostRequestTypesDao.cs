using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Справочник видов заявок для штатных единиц.
    /// </summary>
    public class StaffEstablishedPostRequestTypesDao : DefaultDao<StaffEstablishedPostRequestTypes>, IStaffEstablishedPostRequestTypesDao
    {
        public StaffEstablishedPostRequestTypesDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

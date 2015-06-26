using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Заявки по штатным единицам.
    /// </summary>
    public class StaffEstablishedPostRequestDao : DefaultDao<StaffEstablishedPostRequest>, IStaffEstablishedPostRequestDao
    {
        public StaffEstablishedPostRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

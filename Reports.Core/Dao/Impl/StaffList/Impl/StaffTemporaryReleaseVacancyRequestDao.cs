using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    /// <summary>
    /// Заявки на временно освобождение вакансии.
    /// </summary>
    public class StaffTemporaryReleaseVacancyRequestDao : DefaultDao<StaffTemporaryReleaseVacancyRequest>, IStaffTemporaryReleaseVacancyRequestDao
    {
        public StaffTemporaryReleaseVacancyRequestDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

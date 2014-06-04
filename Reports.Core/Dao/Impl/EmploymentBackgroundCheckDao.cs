using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentBackgroundCheckDao : DefaultDao<BackgroundCheck>, IEmploymentBackgroundCheckDao
    {
        public EmploymentBackgroundCheckDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
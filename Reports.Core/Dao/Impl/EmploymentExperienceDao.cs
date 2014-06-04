using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentExperienceDao : DefaultDao<Experience>, IEmploymentExperienceDao
    {
        public EmploymentExperienceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
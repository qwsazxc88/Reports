using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentEducationDao : DefaultDao<Education>, IEmploymentEducationDao
    {
        public EmploymentEducationDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentFamilyDao : DefaultDao<Family>, IEmploymentFamilyDao
    {
        public EmploymentFamilyDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
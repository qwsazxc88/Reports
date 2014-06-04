using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentManagersDao : DefaultDao<Managers>, IEmploymentManagersDao
    {
        public EmploymentManagersDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
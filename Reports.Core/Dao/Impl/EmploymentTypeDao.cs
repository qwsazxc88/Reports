using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentTypeDao : DefaultDao<EmploymentType>, IEmploymentTypeDao
    {
        public EmploymentTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
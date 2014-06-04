using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentPassportDao : DefaultDao<Passport>, IEmploymentPassportDao
    {
        public EmploymentPassportDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
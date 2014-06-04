using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentMilitaryServiceDao : DefaultDao<MilitaryService>, IEmploymentMilitaryServiceDao
    {
        public EmploymentMilitaryServiceDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
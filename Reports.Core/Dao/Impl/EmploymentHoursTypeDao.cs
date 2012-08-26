using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentHoursTypeDao : DefaultDao<EmploymentHoursType>, IEmploymentHoursTypeDao
    {
        public EmploymentHoursTypeDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentAdditionDao : DefaultDao<EmploymentAddition>, IEmploymentAdditionDao
    {
        public EmploymentAdditionDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
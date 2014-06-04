using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentOnsiteTrainingDao : DefaultDao<OnsiteTraining>, IEmploymentOnsiteTrainingDao
    {
        public EmploymentOnsiteTrainingDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
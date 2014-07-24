using Reports.Core.Domain;
using Reports.Core.Services;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCandidateDao : DefaultDao<EmploymentCandidate>, IEmploymentCandidateDao
    {
        public EmploymentCandidateDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
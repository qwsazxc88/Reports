using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Services;
using System;
using System.Linq;

namespace Reports.Core.Dao.Impl
{
    public class EmploymentCandidateDao : DefaultDao<EmploymentCandidate>, IEmploymentCandidateDao
    {
        public EmploymentCandidateDao(ISessionManager sessionManager)
            : base(sessionManager)
        {            
        }

        public IList<EmploymentCandidate> LoadFiltered(int departmentId, int? statusId, string userName, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<EmploymentCandidate> results = LoadAll();
            if (departmentId > 0)
            {
                results = results.Where<EmploymentCandidate>(x => x.User.Department.Id == departmentId);
            }
            if (statusId.HasValue)
            {
                results = results.Where<EmploymentCandidate>(x => (int)x.Status == statusId.Value);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                results = results.Where<EmploymentCandidate>(x => x.User.Name.Contains(userName));
            }
            if (beginDate.HasValue)
            {
                results = results.Where<EmploymentCandidate>(x => x.QuestionnaireDate >= beginDate);
            }
            if (endDate.HasValue)
            {
                results = results.Where<EmploymentCandidate>(x => x.QuestionnaireDate <= endDate);
            }
            return results.ToList();
        }
    }
}
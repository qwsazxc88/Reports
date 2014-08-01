using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IEmploymentCandidateDao : IDao<EmploymentCandidate>
    {
        IList<EmploymentCandidate> LoadFiltered(int departmentId, int? statusId, string userName, DateTime? beginDate, DateTime? endDate);
    }
}
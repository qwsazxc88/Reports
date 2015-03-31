using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentCandidateDao : IDao<EmploymentCandidate>
    {
        IList<CandidateDto> GetCandidates(int userId,
                UserRole role,
                int departmentId,
                int statusId,
                DateTime? beginDate,
                DateTime? endDate,
                string userName,
                int CandidateId,
                int sortBy,
                bool? sortDescending);

        IList<EmploymentCandidate> LoadForIdsList(IList<int> ids);
        IList<CandidateStateDto> GetCandidateState(int CandidateID);
        IList<CandidatePersonnelDto> GetPersonnels();
    }
}
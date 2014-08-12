﻿using System;
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
                int sortBy,
                bool? sortDescending);
    }
}
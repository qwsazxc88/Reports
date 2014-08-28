using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;

namespace Reports.Core.Dao
{
    public interface IEmploymentCommonDao : IDao<EmploymentCandidate>
    {
        int GetDocumentId<T>(int userId);
        T GetEntityById<T>(int id);
        EmploymentCandidate GetCandidateByUserId(int userId);
        bool SaveOrUpdateDocument<T>(T entity);
    }
}
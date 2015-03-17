using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentFamilyDao : IDao<Family>
    {
        IList<FamilyStatusDto> GetFamilyStatuses();
    }
}
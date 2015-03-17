using System;
using System.Collections.Generic;
using Reports.Core.Domain;
using Reports.Core.Dto;
using Reports.Core.Dto.Employment2;

namespace Reports.Core.Dao
{
    public interface IEmploymentMilitaryServiceDao : IDao<MilitaryService>
    {
        IList<MilitaryValidityCategoryDto> GetMilitaryValidityCategoryes();
        IList<MilitaryRelationAccountDto> GetMilitaryRelationAccounts();
        IList<IdNameDto> GetMilitarySpecialityCategoryes();
        IList<MilitaryRanksDto> GetMilitaryRanks();
    }
}